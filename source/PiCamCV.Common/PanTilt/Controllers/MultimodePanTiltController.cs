using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using PiCamCV.Common.Audio;
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
        ,ColourTrackFromFileSettings
        //Manual input only
        , Static
        ,Autonomous
    }
    
    public class MultimodePanTiltController : CameraBasedPanTiltController<CameraPanTiltProcessOutput>, IKeyHandler
    {
        public ProcessingMode State { get; private set; }
        private ProcessingMode _forcedNextState;

        private readonly IScreen _screen;
        private readonly FaceTrackingPanTiltController _faceTrackingController;
        private readonly CamshiftPanTiltController _camshiftTrackingController;
        private readonly ColourTrackingPanTiltController _colourTrackingController;
        private readonly ThresholdSelector _thresholdSelector;
        private readonly ColourDetectorInput _colourDetectorInput;
        private readonly IServerToCameraBus _serverToCameraBus;
        private readonly IOutputProcessor[] _outputPipelines;
        private Rectangle _regionOfInterest = Rectangle.Empty;
        private readonly IColourSettingsRepository _colourSettingsRepository;

        private readonly FaceTrackStateManager _faceTrackManager;
        private readonly ColourTrackStateManager _colourTrackManager;
        private readonly AutonomousTrackStateManager _autonomousManager;
        private Action _unsubscribeBus;
        ColourDetector _colourDetector;

        public ISoundService SoundService { get; set; }

        public ClassifierParameters ClassifierParams => _faceTrackingController.ClassifierParams;

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

            _colourSettingsRepository = new ColourSettingsRepository();

            _faceTrackingController = new FaceTrackingPanTiltController(panTiltMech, captureConfig);
            _camshiftTrackingController = new CamshiftPanTiltController(panTiltMech, captureConfig);
            _colourTrackingController = new ColourTrackingPanTiltController(panTiltMech, captureConfig);

            _thresholdSelector = new ThresholdSelector();
            _thresholdSelector.ColourCheckTick += thresholdSelector_ColourCheckTick;
            
            _colourDetectorInput = new ColourDetectorInput();
            _colourDetectorInput.SetCapturedImage = true;
            var repoSettings = _colourSettingsRepository.Read();

            if (repoSettings != null)
            {
                _colourDetectorInput.Settings = repoSettings;
            }

            _faceTrackManager = new FaceTrackStateManager(screen);
            _colourTrackManager = new ColourTrackStateManager(screen);
            _autonomousManager = new AutonomousTrackStateManager(this, screen);

            SoundService = new SoundService();


            screen.Clear();
            ChangeMode(ProcessingMode.Autonomous);
            
            _autonomousManager.IsFaceFound = i => _faceTrackingController.Process(i).IsDetected;
            _autonomousManager.IsColourFullFrame =  IsColourFullFrame;

            _faceTrackingController.ClassifierParams.MinSize = new Size(20, 20);
            _faceTrackingController.ClassifierParams.MaxSize = new Size(50, 50);

            screen.WriteLine(_faceTrackingController.ClassifierParams.ToString());

            InitController();

            ServoSettleTimeChanged += (s, e) =>
            {
                _faceTrackingController.ServoSettleTime = ServoSettleTime;
                _colourTrackingController.ServoSettleTime = ServoSettleTime;
            };

            _colourDetector = new ColourDetector();
        }
        
        private bool IsColourFullFrame(CameraProcessInput input)
        {
            var isFullFrameColour = false;
            // detect all black
            using (new TemporaryThresholdSettings(_colourDetectorInput, ThresholdSettings.Get(0, 0, 0, 180, 255, 40)))
            {
                _colourDetectorInput.Captured = input.Captured;
                var colourOutput = _colourDetector.Process(_colourDetectorInput);
                const int fullFrameMinimumPercent = 70;

                var fullFramePixelCount = colourOutput.CapturedImage.Width*colourOutput.CapturedImage.Height;
                var mimimumColourPixelCount = fullFramePixelCount*fullFrameMinimumPercent/100;
                isFullFrameColour = colourOutput.MomentArea > mimimumColourPixelCount;
            }
            return isFullFrameColour;
        }

        private void thresholdSelector_ColourCheckTick(object sender, AutoThresholdResult e)
        {
            // Inform the server of what we are doing
            if (e.DimensionValue % 5 == 0)
            {
                e.RoiOutput.CapturedImage = GetBgr(e.RoiOutput?.ThresholdImage);
                ProcessOutputPipeline(e.RoiOutput);
            }
        }

        private Image<Bgr, byte> GetBgr(Image<Gray, byte>  input)
        {
            if (input == null)
            {
                return null;
            }
            return new Image<Bgr, byte>(new Image<Gray, byte>[] {input, input, input});
        }

        private void InitController()
        {
            EventHandler<ProcessingMode> setModeHandler = (s, e) => { ForceMode(e); };
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
                    ForceMode(ProcessingMode.Static);
                    _screen.WriteLine($"{command}");
                    break;

                case PanTiltSettingCommandType.MoveRelative:
                    MoveRelative(command);
                    ForceMode(ProcessingMode.Static);
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
                case ProcessingMode.ColourTrackFromFileSettings:
                    _colourDetectorInput.Settings = _colourSettingsRepository.Read();
                    _screen.WriteLine($"Read colour settings {_colourDetectorInput.Settings}");
                    nextState = ProcessingMode.ColourObjectTrack;
                    break;

                case ProcessingMode.ColourObjectTrack:
                    var colourOutput = ProcessColour(input);
                    output = colourOutput;

                    if (Ticks % (90*3) == 0) // provide some feedback on moment size but don't spam
                    {
                        _screen.WriteLine("colTrack:" + colourOutput);
                    }

                    nextState = _colourTrackManager.AcceptOutput(colourOutput);
                    break;

                case ProcessingMode.FaceDetection:
                    var faceTrackOutput = _faceTrackingController.Process(input);

                    nextState =  _faceTrackManager.AcceptOutput(faceTrackOutput);
                    output = faceTrackOutput;
                    break;

                //case ProcessingMode.CamshiftTrack:
                //    var camshiftOutput = _camshiftTrackingController.Process(input);

                //    if (camshiftOutput.Target == Point.Empty)
                //    {
                //        SetMode(ProcessingMode.Autonomous);
                //    }

                //    output = camshiftOutput;
                //    break;

                case ProcessingMode.ColourObjectSelect:
                    _screen.WriteLine($"Threshold training for {_thresholdSelector.RequiredMomentAreaInRoiPercent}% ROI coverage");
                    var thresholdSettings = _thresholdSelector.Select(input.Captured, _regionOfInterest);
                    _screen.WriteLine($"Threshold tuning complete: {thresholdSettings}");
                    _colourDetectorInput.SetCapturedImage = true;
                    _colourDetectorInput.Settings.MomentArea = new RangeF(50, 10000);
                    _colourDetectorInput.Settings.Accept(thresholdSettings);
                    //_isColourTrained = true;
                    nextState = ProcessingMode.ColourObjectTrack;
                    break;
                
                case ProcessingMode.Autonomous:
                    nextState = _autonomousManager.AcceptInput(input);
                    if (nextState == ProcessingMode.ColourObjectTrack)
                    {
                        
                    }
                    break;

                case ProcessingMode.CamshiftSelect:
                    throw new NotImplementedException();
            }

            if (output.CapturedImage == null)
            {
                output.CapturedImage = input.Captured.ToImage<Bgr, byte>();
            }

            ProcessOutputPipeline(output);

            if (_forcedNextState != ProcessingMode.Unknown)
            {
                nextState = _forcedNextState;
                _forcedNextState = ProcessingMode.Unknown;
            }

            if (nextState != State)
            {
                _screen.WriteLine($"Changing {State} to {nextState}");
                switch (nextState)
                {
                    case ProcessingMode.Autonomous:
                        if (State == ProcessingMode.FaceDetection) // coming out of face detection
                        {
                            SoundService.PlayAsync("cant-see-you.wav");
                        }
                        MoveAbsolute(50, 60);
                        _autonomousManager.Reset();     // Reset the timers
                        break;
                    case ProcessingMode.ColourObjectTrack:
                        _colourTrackManager.Reset();
                        _screen.WriteLine($"Color detector settings: {_colourDetectorInput.Settings}");
                        SoundService.PlayAsync("color-tracking.wav");
                        break;
                    case ProcessingMode.FaceDetection:
                        _faceTrackManager.Reset();
                        SoundService.PlayAsync("face-tracking.wav");
                        _screen.WriteLine(ClassifierParams.ToString());
                        break;
                }
                
                State = nextState;
            }

            return output;
        }

        private ColourTrackingPanTiltOutput ProcessColour(CameraProcessInput input)
        {
            _colourDetectorInput.Captured = input.Captured;
            ColourTrackingPanTiltOutput colourOutput;
            
            _colourTrackingController.Settings = _colourDetectorInput.Settings;
            colourOutput = _colourTrackingController.Process(_colourDetectorInput);
            
            colourOutput.CapturedImage = GetBgr(colourOutput.ThresholdImage);
            return colourOutput;
        }

        private void ChangeMode(ProcessingMode mode)
        {
            if (State != mode)
            {
                _screen.WriteLine($"Mode {State}=>{mode}");
                State = mode;
            }
        }

        public void ForceMode(ProcessingMode mode)
        {
            _forcedNextState = mode;
            _screen.WriteLine($"Forcing {mode} (Currently {State})");
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
                    captureConfig.Framerate = 60;
                    captureConfig.Resolution = new Resolution(160, 120);
                    _serverToCameraBus.InvokeUpdateCapture(captureConfig);
                    break;

                case 'a':
                    ForceMode(ProcessingMode.Autonomous);
                    break;

                case 'f':
                    ForceMode(ProcessingMode.FaceDetection);
                    break;

                case 'c':
                    ForceMode(ProcessingMode.ColourTrackFromFileSettings);
                    break;

                case 'q':
                    _screen.WriteLine($"P/T={CurrentSetting},M={State}");
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
