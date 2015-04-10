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
        }

        protected override MotionTrackingPanTiltOutput DoProcess(CameraProcessInput input)
        {
            var detectorInput = new MotionDetectorInput();
            detectorInput.SetCapturedImage = false;
            detectorInput.MinimumArea = 2000;
            detectorInput.MinimumPercentMotionInArea = 0.20m;

            var motionOutput = _motionDetector.Process(detectorInput);

            var targetPoint = CentrePoint;
            if (motionOutput.IsDetected)
            {
                motionOutput.MotionSections.Sort((x, y) =>  x.Area.CompareTo(y.Area));
                var biggestMotion = motionOutput.MotionSections[0];
                targetPoint = biggestMotion.Region.Center();
            }

            var output = ReactToTarget(targetPoint);
            output.MotionSections.AddRange(motionOutput.MotionSections);
            return output;
        }
    }
}
