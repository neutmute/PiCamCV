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
        public MCvScalar LowThreshold { get; set; }

        public MCvScalar HighThreshold { get; set; }

        public ColorDetectRunner(ICaptureGrab capture, MCvScalar lowThreshold, MCvScalar highThreshold)
            : base(capture)
        {
            LowThreshold = lowThreshold;
            HighThreshold = highThreshold;
        }

        public ColorDetectRunner(ICaptureGrab capture) : base(capture)
        {
            // useful defaults - red under lights
            LowThreshold = new MCvScalar(140, 57, 25);
            HighThreshold = new MCvScalar(187, 153, 82);
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
                   ,LowThreshold = LowThreshold
                   ,HighThreshold = HighThreshold
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
