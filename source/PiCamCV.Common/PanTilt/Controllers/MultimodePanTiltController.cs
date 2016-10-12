using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using PiCamCV.Common.Interfaces;
using PiCamCV.Common.PanTilt.Controllers.multimode;
using PiCamCV.ConsoleApp.Runners.PanTilt;
using RPi.Pwm.Motors;

namespace PiCamCV.Common.PanTilt.Controllers
{
    public enum ProcessingMode
    {
        Unknown = 0
        ,FaceDetection
        ,CamshiftTrack
        ,ColourObjectTrack
        ,CamshiftSelect
        ,ColourObjectSelect

        //Manual input only
        ,Static
        ,Autonomous
    }
    
    public class MultimodePanTiltController : CameraBasedPanTiltController<CameraPanTiltProcessOutput>, IKeyHandler
    {
        public ProcessingMode State { get; private set; }

        private readonly IScreen _screen;
        private readonly FaceTrackingPanTiltController _faceTrackingController;
        private readonly CamshiftPanTiltController _camshiftTrackingController;
        private readonly ColourTrackingPanTiltController _colourTrackingController;
        private readonly ThresholdSelector _thresholdSelector;
        private ColourDetectorInput _colourDetectorInput;
        private readonly IServerToCameraBus _serverToCameraBus;
        private readonly IOutputProcessor[] _outputPipelines;
        private Rectangle _regionOfInterest = Rectangle.Empty;
        private FaceTrackingPanTiltOutput _lastFaceTrack;

        private FaceTrackStateManager _faceTrackManager;
        private AutonomousTrackStateManager _autonomousManager;
        private EventHandler<PanTiltSetting> _moveAbsHandler;
        private EventHandler<PanTiltSetting> _moveRelHandler;

        private Action _unsubscribeBus;

        /// <summary>
        /// sudo mono picamcv.con.exe -m=pantiltmultimode
        /// </summary>
        public MultimodePanTiltController(
            IPanTiltMechanism panTiltMech
            , CaptureConfig captureConfig
            , IScreen screen
            , IServerToCameraBus serverToCameraBus
            , params IOutputProcessor[] outputPipelines) : base(panTiltMech, captureConfig)
        {
            _screen = screen;
            _serverToCameraBus = serverToCameraBus;
            _outputPipelines = outputPipelines;

            _faceTrackingController = new FaceTrackingPanTiltController(panTiltMech, captureConfig);
            _camshiftTrackingController = new CamshiftPanTiltController(panTiltMech, captureConfig);
            _colourTrackingController = new ColourTrackingPanTiltController(panTiltMech, captureConfig);

            _thresholdSelector = new ThresholdSelector();
            _thresholdSelector.ColourCheckTick += thresholdSelector_ColourCheckTick;
            
            _colourDetectorInput = new ColourDetectorInput();
            _colourDetectorInput.SetCapturedImage = true;
            _colourDetectorInput.Settings.MomentArea = new RangeF(200, 10000);

            _faceTrackManager = new FaceTrackStateManager(screen);
            _autonomousManager = new AutonomousTrackStateManager(this, screen);

            screen.Clear();
            SetMode(ProcessingMode.Autonomous);

            _lastFaceTrack = new FaceTrackingPanTiltOutput();

            _autonomousManager.IsFaceFound = i => _faceTrackingController.Process(i).IsDetected;

            InitController();
        }

        private void thresholdSelector_ColourCheckTick(object sender, AutoThresholdResult e)
        {
            // Inform the server of what we are doing
            if (e.DimensionValue % 5 == 0)
            {
                e.RoiOutput.CapturedImage = GetBgr(e.RoiOutput.ThresholdImage);
                ProcessOutputPipeline(e.RoiOutput);
            }
        }

        private Image<Bgr, byte> GetBgr(Image<Gray, byte>  input)
        {
            return new Image<Bgr, byte>(new Image<Gray, byte>[] {input, input, input});
        }

        private void InitController()
        {
            _moveAbsHandler = (s, e) => { MoveAbsolute(e); SetMode(ProcessingMode.Static); _screen.WriteLine($"Move Absolute {e}"); };
            _moveRelHandler = (s, e) => { MoveRelative(e); SetMode(ProcessingMode.Static); _screen.WriteLine($"Move Relative {e} to {CurrentSetting}"); };
            EventHandler<ProcessingMode> setModeHandler = (s, e) => { State = e; };
            EventHandler<Rectangle> setRoiHandler = (s, e) => { _regionOfInterest = e; _screen.WriteLine("ROI set"); };

            _serverToCameraBus.SetMode += setModeHandler;
            _serverToCameraBus.MoveAbsolute += _moveAbsHandler;
            _serverToCameraBus.MoveRelative += _moveRelHandler;
            _serverToCameraBus.SetRegionOfInterest += setRoiHandler;

            _unsubscribeBus = () =>
            {
                _serverToCameraBus.SetMode -= setModeHandler;
                _serverToCameraBus.MoveAbsolute -= _moveAbsHandler;
                _serverToCameraBus.MoveRelative -= _moveRelHandler;
                _serverToCameraBus.SetRegionOfInterest -= setRoiHandler;
            };
        }
        

        protected override CameraPanTiltProcessOutput DoProcess(CameraProcessInput input)
        {
            var output = new CameraPanTiltProcessOutput();
            ProcessingMode nextState = State;
            switch (State)
            {
                case ProcessingMode.ColourObjectTrack:
                    _colourDetectorInput.Captured = input.Captured;
                    _colourTrackingController.Settings = _colourDetectorInput.Settings;
                    var colourOutput = _colourTrackingController.Process(_colourDetectorInput);
                    colourOutput.CapturedImage = GetBgr(colourOutput.ThresholdImage);
                    output = colourOutput;
                    break;

                case ProcessingMode.FaceDetection:
                    var faceTrackOutput = _faceTrackingController.Process(input);

                    nextState =  _faceTrackManager.AcceptScan(faceTrackOutput);
                    //if (!faceTrackOutput.IsDetected && _lastFaceTrack.IsDetected)
                    //{
                    //    _screen.WriteLine("Switching to Camshift");
                    //    // camshift on last known position
                    //    State = ProcessingMode.CamshiftTrack;
                    //    _camshiftTrackingController.TrackConfig = new TrackingConfig();
                    //    _camshiftTrackingController.TrackConfig.ObjectOfInterest = _lastFaceTrack.Faces.First().Region;
                    //    _camshiftTrackingController.TrackConfig.StartNewTrack = true;
                    //}
                    //_lastFaceTrack = faceTrackOutput;
                    output = faceTrackOutput;
                    break;

                case ProcessingMode.CamshiftTrack:
                    var camshiftOutput = _camshiftTrackingController.Process(input);

                    if (camshiftOutput.Target == Point.Empty)
                    {
                        SetMode(ProcessingMode.Autonomous);
                    }

                    output = camshiftOutput;
                    break;

                case ProcessingMode.ColourObjectSelect:
                    _screen.WriteLine($"Threshold training for {_thresholdSelector.RequiredMomentAreaInRoiPercent}% ROI coverage");
                    var thresholdSettings = _thresholdSelector.Select(input.Captured, _regionOfInterest);
                    _screen.WriteLine($"Threshold settings: {thresholdSettings}");
                    _colourDetectorInput.SetCapturedImage = true;
                    _colourDetectorInput.Settings.Accept(thresholdSettings);

                    // Change state
                    State = ProcessingMode.ColourObjectTrack;
                    break;
                
                case ProcessingMode.Autonomous:
                    nextState = _autonomousManager.AcceptInput(input);
                    break;

                case ProcessingMode.CamshiftSelect:
                    throw new NotImplementedException();
            }

            if (output.CapturedImage == null)
            {
                output.CapturedImage = input.Captured.ToImage<Bgr, byte>();
            }

            ProcessOutputPipeline(output);

            if (nextState != State)
            {
                _screen.WriteLine($"Changing to {nextState}");
                State = nextState;
                if (nextState == ProcessingMode.Autonomous)
                {
                    // Reset the timers
                    _autonomousManager.Reset();
                }
            }

            return output;
        }

        public void SetMode(ProcessingMode mode)
        {
            if (State != mode)
            {
                State = mode;
                _screen.WriteLine($"Mode changed to {mode}");
            }
        }

        private void ProcessOutputPipeline(CameraProcessOutput output)
        {
            foreach (var outputPipeline in _outputPipelines)
            {
                outputPipeline.Process(output);
            }
        }
        
        protected override void DisposeObject()
        {
            base.DisposeObject();
            _faceTrackingController.Dispose();
            _camshiftTrackingController.Dispose();
            _unsubscribeBus();
        }

        public void Unsubscribe()
        {
            _unsubscribeBus();
        }
        
        public bool HandleKeyPress(char key)
        {
            var handled = true;
            switch (key)
            {
                case 'a':
                    SetMode(ProcessingMode.Autonomous);
                    break;
                case 'r':
                    _moveAbsHandler(this, new PanTiltSetting(50, 50));
                    break;
                default:
                    handled = false;
                    break;
            }
            return handled;
        }
    }
}
