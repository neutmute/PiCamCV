using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    //public enum CommandType
    //{
    //    Unknown
    //    ,Recenter
    //    ,CommenceFaceTrack
    //    ,CommenceCamshiftDetect
    //}

    //public class PanTiltCommand
    //{
    //    public CommandType Type { get; set; }

    //    public override string ToString()
    //    {
    //        return Type.ToString();
    //    }

    //    public static PanTiltCommand Factory(CommandType type)
    //    {
    //        var command = new PanTiltCommand();
    //        command.Type = type;
    //        return command;
    //    }
    //}
    
    public class MultimodePanTiltController : CameraBasedPanTiltController<CameraPanTiltProcessOutput>
    {
        public ProcessingMode State { get; set; }

        private readonly IScreen _screen;
        private readonly FaceTrackingPanTiltController _faceTrackingController;
        private readonly CamshiftPanTiltController _camshiftTrackingController;
        private readonly IServerToCameraBus _serverToCameraBus;
        private readonly IOutputProcessor[] _outputPipelines;
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
            screen.Clear();
            StateToFaceDetect();

            _lastFaceTrack = new FaceTrackingPanTiltOutput();
            InitController();
        }
        
        private void InitController()
        {

            EventHandler<ProcessingMode> setModeHandler = (s, e) => { State = e; };
            EventHandler<PanTiltSetting> moveAbsHandler = (s, e) => { MoveAbsolute(e); _screen.WriteLine($"Move Absolute {e}"); };
            EventHandler<PanTiltSetting> moveRelHandler = (s, e) => { MoveRelative(e); _screen.WriteLine($"Move Relative {e}"); };

            _serverToCameraBus.SetMode += setModeHandler;
            _serverToCameraBus.MoveAbsolute += moveAbsHandler;
            _serverToCameraBus.MoveRelative += moveRelHandler;

            _unsubscribeBus = () =>
            {
                _serverToCameraBus.SetMode -= setModeHandler;
                _serverToCameraBus.MoveAbsolute -= moveAbsHandler;
                _serverToCameraBus.MoveRelative -= moveRelHandler;
            };
        }
        

        protected override CameraPanTiltProcessOutput DoProcess(CameraProcessInput input)
        {
            var output = new CameraPanTiltProcessOutput();
            switch (State)
            {
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

                case ProcessingMode.CamshiftSelect:
                    throw new NotImplementedException();
            }

            foreach (var outputPipeline in _outputPipelines)
            {
                outputPipeline.Process(output);
            }

            return output;
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
