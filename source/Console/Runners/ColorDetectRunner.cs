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
    class ColorDetectRunner : BaseRunner
    {
        private readonly ICaptureGrab _capture;
        public ColorDetectRunner(ICaptureGrab capture)
        {
            _capture = capture;
        }
        public override void Run()
        {
            _capture.ImageGrabbed += _capture_ImageGrabbed;
            _capture.Start();

            do
            {
            } while (CvInvoke.cvWaitKey(100) < 0);

        }

        void _capture_ImageGrabbed(object sender, EventArgs e)
        {
            var matCaptured = new Mat();
            _capture.Retrieve(matCaptured);
            var detector = new ColourDetector();
            var input = new ColourDetectorInput
            {
               Captured = matCaptured
               ,LowThreshold =new MCvScalar(140, 57, 25)
               ,HighThreshold = new MCvScalar(187, 153, 82)
            };
            var result = detector.Process(input);

            if (result.IsDetected)
            {
                Log.Info(result);
            }
        }
    }
}
