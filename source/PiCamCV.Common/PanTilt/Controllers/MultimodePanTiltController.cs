using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using PiCamCV.Common.Interfaces;
using PiCamCV.ConsoleApp.Runners.PanTilt;
using RPi.Pwm.Motors;

namespace PiCamCV.Common.PanTilt.Controllers
{
    public enum ProcessingMode
    {
        FaceDetection
        ,CamshiftTrack
        ,ColourObjectTrack
        //,MultiTrack
        ,CamshiftSelect
        ,ColourObjectSelect  
    }
    
    public class MultimodePanTiltController : CameraBasedPanTiltController<CameraPanTiltProcessOutput>
    {
        public ProcessingMode State { get; set; }

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

            screen.Clear();
            StateToFaceDetect();

            _lastFaceTrack = new FaceTrackingPanTiltOutput();
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
            EventHandler<ProcessingMode> setModeHandler = (s, e) => { State = e; };
            EventHandler<PanTiltSetting> moveAbsHandler = (s, e) => { MoveAbsolute(e); _screen.WriteLine($"Move Absolute {e}"); };
            EventHandler<PanTiltSetting> moveRelHandler = (s, e) => { MoveRelative(e); _screen.WriteLine($"Move Relative {e}"); };
            EventHandler<Rectangle> setRoiHandler = (s, e) => { _regionOfInterest = e; _screen.WriteLine("ROI set"); };

            _serverToCameraBus.SetMode += setModeHandler;
            _serverToCameraBus.MoveAbsolute += moveAbsHandler;
            _serverToCameraBus.MoveRelative += moveRelHandler;
            _serverToCameraBus.SetRegionOfInterest += setRoiHandler;

            _unsubscribeBus = () =>
            {
                _serverToCameraBus.SetMode -= setModeHandler;
                _serverToCameraBus.MoveAbsolute -= moveAbsHandler;
                _serverToCameraBus.MoveRelative -= moveRelHandler;
                _serverToCameraBus.SetRegionOfInterest -= setRoiHandler;
            };
        }
        

        protected override CameraPanTiltProcessOutput DoProcess(CameraProcessInput input)
        {
            var output = new CameraPanTiltProcessOutput();
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

                    if (!_lastFaceTrack.IsDetected && faceTrackOutput.IsDetected)
                    {
                        _screen.WriteLine("Face detected");
                    }

                    if (!faceTrackOutput.IsDetected && _lastFaceTrack.IsDetected)
                    {
                        _screen.WriteLine("Switching to Camshift");
                        // camshift on last known position
                        State = ProcessingMode.CamshiftTrack;
                        _camshiftTrackingController.TrackConfig = new TrackingConfig();
                        _camshiftTrackingController.TrackConfig.ObjectOfInterest = _lastFaceTrack.Faces.First().Region;
                        _camshiftTrackingController.TrackConfig.StartNewTrack = true;
                    }

                    _lastFaceTrack = faceTrackOutput;
                    output = faceTrackOutput;
                    break;

                case ProcessingMode.CamshiftTrack:
                    var camshiftOutput = _camshiftTrackingController.Process(input);

                    if (camshiftOutput.Target == Point.Empty)
                    {
                        StateToFaceDetect();
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

                case ProcessingMode.CamshiftSelect:
                    throw new NotImplementedException();
            }

            ProcessOutputPipeline(output);
            
            return output;
        }

        private void ProcessOutputPipeline(CameraProcessOutput output)
        {
            foreach (var outputPipeline in _outputPipelines)
            {
                outputPipeline.Process(output);
            }
        }

        private void StateToFaceDetect()
        {
            _screen.WriteLine("Switching to Face Detection");
            State = ProcessingMode.FaceDetection;
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
    }
}
