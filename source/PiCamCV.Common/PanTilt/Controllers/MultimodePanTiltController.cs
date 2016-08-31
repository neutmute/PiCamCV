using System;
using System.Collections.Generic;
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
        ,ObjectDetect
    }

    public enum CommandType
    {
        Unknown
        ,Recenter
        ,CommenceFaceTrack
        ,CommenceCamshiftDetect
    }

    public class PanTiltCommand
    {
        public CommandType Type { get; set; }
    }

    //public class FakeServo : IServoMotor
    //{
    //    public decimal CurrentPercent { get; }
    //    public int CurrentPwm { get; }

    //    public FakeServo()
    //    {
    //        CurrentPwm
    //    }

    //    public bool MoveTo(decimal percent)
    //    {
    //        // noop
    //    }
    //}

    //public class FakePanTiltMech : IPanTiltMechanism
    //{
    //    public IServoMotor PanServo { get; }
    //    public IServoMotor TiltServo { get; }

    //    public FakePanTiltMech()
    //    {
    //        PanServo = new ServoMotor();
    //    }
    //}


    public class MultimodePanTiltController : CameraBasedPanTiltController<CameraPanTiltProcessOutput>
    {
        public ProcessingMode State { get; set; }

        public Queue<PanTiltCommand> CommandQueue { get; private set; }

        private FaceTrackingPanTiltController _faceTrackingController;
        private CamshiftPanTiltController _camshiftTrackingController;

        public MultimodePanTiltController(IPanTiltMechanism panTiltMech, CaptureConfig captureConfig) : base(panTiltMech, captureConfig)
        {
            CommandQueue = new Queue<PanTiltCommand>();
            _faceTrackingController = new FaceTrackingPanTiltController(panTiltMech, captureConfig);
            _camshiftTrackingController = new CamshiftPanTiltController(panTiltMech, captureConfig);
        }

        protected override CameraPanTiltProcessOutput DoProcess(CameraProcessInput input)
        {
            ActionCommand();

            switch (State)
            {
                case ProcessingMode.FaceDetection:
                    var faceTrackOutput = _faceTrackingController.Process(input);
                    break;
                case ProcessingMode.CamshiftTrack:
                    var camshiftOutput= _camshiftTrackingController.Process(input);
                    break;
            }

            return null;
        }

        private void ActionCommand()
        {
            var command = CommandQueue.Dequeue();
        }

        protected override void DisposeObject()
        {
            base.DisposeObject();
            _faceTrackingController.Dispose();
            _camshiftTrackingController.Dispose();
        }
    }
}
