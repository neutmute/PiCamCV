using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PiCamCV.Common;
using PiCamCV.Common.ExtensionMethods;
using PiCamCV.Common.Interfaces;

namespace PiCamCV.ConsoleApp.Runners.PanTilt
{
    public class ColourTrackingPanTiltController : CameraBasedPanTiltController<ColourTrackingPanTiltOutput>
    {
        private readonly ColourDetector _colourDetector;
        public ColourDetectSettings Settings { get; set; }

        public ColourTrackingPanTiltController(IPanTiltMechanism panTiltMech, CaptureConfig captureConfig, IScreen screen)
            : base(panTiltMech, captureConfig, screen)
        {
            _colourDetector = new ColourDetector();
        }
        protected override ColourTrackingPanTiltOutput DoProcess(CameraProcessInput input)
        {
            var colourDetectorInput = new ColourDetectorInput();
            colourDetectorInput.Captured = input.Captured;
            colourDetectorInput.SetCapturedImage = input.SetCapturedImage;
            colourDetectorInput.Settings = Settings;

            var colourDetectorOutput = _colourDetector.Process(colourDetectorInput);

            var targetPoint = CentrePoint;
            if (colourDetectorOutput.IsDetected)
            {
                targetPoint = colourDetectorOutput.CentralPoint.ToPoint();
            }
            var output = ReactToTarget(targetPoint);
            output.MomentArea = colourDetectorOutput.MomentArea;
            return output;
        }
    }
}
