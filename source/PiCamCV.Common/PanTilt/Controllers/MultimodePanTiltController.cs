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
        private readonly ColourDetectorInput _colourDetectorInput;
        private readonly IServerToCameraBus _serverToCameraBus;
        private readonly IOutputProcessor[] _outputPipelines;
        private Rectangle _regionOfInterest = Rectangle.Empty;

        private readonly FaceTrackStateManager _faceTrackManager;
        private readonly AutonomousTrackStateManager _autonomousManager;

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
            _colourDetectorInput.Settings.MomentArea = new RangeF(50, 10000);

            _faceTrackManager = new FaceTrackStateManager(screen);
            _autonomousManager = new AutonomousTrackStateManager(this, screen);

            screen.Clear();
            SetMode(ProcessingMode.Autonomous);
            
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
            EventHandler<ProcessingMode> setModeHandler = (s, e) => { SetMode(e); };
            EventHandler<Rectangle> setRoiHandler = (s, e) => { _regionOfInterest = e; _screen.WriteLine("ROI set"); };
            
            _serverToCameraBus.SetMode += setModeHandler;
            _serverToCameraBus.PanTiltCommand += HandleCommand;
            _serverToCameraBus.SetRegionOfInterest += setRoiHandler;

            _unsubscribeBus = () =>
            {
                _serverToCameraBus.SetMode -= setModeHandler;
                _serverToCameraBus.SetRegionOfInterest -= setRoiHandler;
                _serverToCameraBus.PanTiltCommand -= HandleCommand;
            };
        }

        private void HandleCommand(object sender, PanTiltSettingCommand command)
        {
            switch (command.Type)
            {
                case PanTiltSettingCommandType.MoveAbsolute:
                    MoveAbsolute(command);
                    SetMode(ProcessingMode.Static);
                    _screen.WriteLine($"{command}");
                    break;

                case PanTiltSettingCommandType.MoveRelative:
                    MoveRelative(command);
                    SetMode(ProcessingMode.Static);
                    _screen.WriteLine($"{command}");
                    break;

                case PanTiltSettingCommandType.SetRangePursuitLower:
                    _autonomousManager.PursuitBoundaryLower = command;
                    ReportPursuitBoundaries();
                    break;

                case PanTiltSettingCommandType.SetRangePursuitUpper:
                    _autonomousManager.PursuitBoundaryUpper = command;
                    ReportPursuitBoundaries();
                    break;
            }
        }

        private void ReportPursuitBoundaries()
        {
            var panText = $"Pan={_autonomousManager.PursuitBoundaryLower.PanPercent}-{_autonomousManager.PursuitBoundaryUpper.PanPercent}";
            var tiltText = $"Tilt={_autonomousManager.PursuitBoundaryLower.TiltPercent}-{_autonomousManager.PursuitBoundaryUpper.TiltPercent}";
            _screen.WriteLine($"Pursuit boundary: {panText}, {tiltText}");
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

                    if (Ticks % 60 == 0) // provide some feedback on moment size but don't spam
                    {
                        _screen.WriteLine(colourOutput.ToString());
                    }
                    break;

                case ProcessingMode.FaceDetection:
                    var faceTrackOutput = _faceTrackingController.Process(input);

                    nextState =  _faceTrackManager.AcceptScan(faceTrackOutput);
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
                    _screen.WriteLine($"Threshold tuning complete: {thresholdSettings}");
                    _colourDetectorInput.SetCapturedImage = true;
                    _colourDetectorInput.Settings.Accept(thresholdSettings);
                    
                    nextState = ProcessingMode.ColourObjectTrack;
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
                switch (nextState)
                {
                    case ProcessingMode.Autonomous:
                        // Reset the timers
                        _autonomousManager.Reset();
                        break;
                    case ProcessingMode.ColourObjectTrack:
                        _screen.WriteLine($"Color detector settings: {_colourDetectorInput.Settings}");
                        break;
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
            var captureConfig = new CaptureConfig();
            switch (key)
            {
                case '7':
                    captureConfig.Framerate = 40;
                    captureConfig.Resolution = new Resolution(128, 96);
                    _serverToCameraBus.InvokeUpdateCapture(captureConfig);
                    break;

                case '8':
                    captureConfig.Framerate = 40;
                    captureConfig.Resolution = new Resolution(160, 120);
                    _serverToCameraBus.InvokeUpdateCapture(captureConfig);
                    break;

                case 'a':
                    SetMode(ProcessingMode.Autonomous);
                    break;

                case 'r':
                    var command = new PanTiltSettingCommand();
                    command.Type= PanTiltSettingCommandType.MoveAbsolute;
                    command.PanPercent = 50;
                    command.TiltPercent = 50;
                    HandleCommand(this, command);
                    break;
                default:
                    handled = false;
                    break;
            }
            return handled;
        }
    }
}
