using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PiCamCV.Common.Interfaces;
using PiCamCV.ConsoleApp.Runners.PanTilt;
using PiCamCV.ExtensionMethods;

namespace PiCamCV.Common.PanTilt.Controllers
{
    public class MotionTrackingPanTiltOutput : CameraPanTiltProcessOutput
    {
        public List<MotionSection> MotionSections { get; private set; }

        public MotionSection TargetedMotion { get;set;}

        public bool IsDetected
        {
            get { return MotionSections.Count > 0; }
        }

        public MotionTrackingPanTiltOutput()
        {
            MotionSections = new List<MotionSection>();
        }
    }

    public class MotionTrackingPanTiltController : CameraBasedPanTiltController<MotionTrackingPanTiltOutput>
    {
        private readonly MotionDetector _motionDetector;

        public MotionTrackingPanTiltController(IPanTiltMechanism panTiltMech, CaptureConfig captureConfig)
            : base(panTiltMech, captureConfig)
        {
            _motionDetector = new MotionDetector();

            SetServoSettleTime(200);
        }

        protected override MotionTrackingPanTiltOutput DoProcess(CameraProcessInput input)
        {
            var detectorInput = new MotionDetectorInput();
            detectorInput.SetCapturedImage = false;
            //detectorInput.MinimumArea = 2000;
            //detectorInput.MinimumPercentMotionInArea = 0.20m;
            detectorInput.Captured = input.Captured;

            var motionOutput = _motionDetector.Process(detectorInput);

            var targetPoint = CentrePoint;
            MotionSection biggestMotion = null;
            if (motionOutput.IsDetected)
            {
                motionOutput.MotionSections.Sort((x, y) =>  y.Area.CompareTo(x.Area));
                biggestMotion = motionOutput.MotionSections[0];
                targetPoint = biggestMotion.Region.Center();
            }

            var output = ReactToTarget(targetPoint);
            output.MotionSections.AddRange(motionOutput.MotionSections);
            output.TargetedMotion = biggestMotion;
            return output;
        }

        protected override void PreServoSettle()
        {
            _motionDetector.Reset();
        }
    }
}
