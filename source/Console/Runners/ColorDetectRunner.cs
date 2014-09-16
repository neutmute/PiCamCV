using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;
using PiCamCV.Common;
using PiCamCV.Interfaces;

namespace PiCamCV.ConsoleApp.Runners
{
    class ColorDetectRunner : CameraConsumerRunner
    {
        public ColourDetectSettings Settings { get; set; }

        public ColorDetectRunner(ICaptureGrab capture, ColourDetectSettings settings)
            : base(capture)
        {
            Settings = settings;
        }

        public ColorDetectRunner(ICaptureGrab capture) : base(capture)
        {
            Settings = new ColourDetectSettings();
            // useful defaults - red under lights
            Settings.LowThreshold = new MCvScalar(140, 57, 25);
            Settings.HighThreshold = new MCvScalar(187, 153, 82);
            Settings.MinimumDetectionArea = 200;
        }


        public override void ImageGrabbedHandler(object sender, EventArgs e)
        {
            using (var matCaptured = new Mat())
            {
                CameraCapture.Retrieve(matCaptured);
                var detector = new ColourDetector();
                var input = new ColourDetectorInput
                {
                    Captured = matCaptured
                   ,Settings= Settings
                   ,SetCapturedImage = false
                };

                var result = detector.Process(input);

                if (result.IsDetected)
                {
                    Log.Info(result);
                }
            }
        }
    }
}
