using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using PiCamCV.Common;
using PiCamCV.Common.ExtensionMethods;
using PiCamCV.Common.Interfaces;

namespace PiCamCV.ConsoleApp.Runners.PanTilt
{
    public class ColourTrackingPanTiltOutput : TrackingCameraPanTiltProcessOutput
    {
        private bool _isDetected;

        public double MomentArea { get; set; }

        public override bool IsDetected => _isDetected;

        public Image<Gray, byte> ThresholdImage { get; set; }

        public override string ToString()
        {
            return $"MomentArea={MomentArea}, IsDetected={IsDetected}, {base.ToString()}";
        }

        public void Absorb(ColourDetectorOutput output)
        {
            _isDetected = output.IsDetected;
            MomentArea = output.MomentArea;
            ThresholdImage = output.ThresholdImage;
        }
    }

    public class ColourTrackingPanTiltController : CameraBasedPanTiltController<ColourTrackingPanTiltOutput>
    {
        private readonly ColourDetector _colourDetector;

        public ColourDetectSettings Settings { get; set; }

        public ColourTrackingPanTiltController(IPanTiltMechanism panTiltMech, CaptureConfig captureConfig)
            : base(panTiltMech, captureConfig)
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
            output.Absorb(colourDetectorOutput);
            return output;
        }
    }
}
