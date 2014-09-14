using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;
using PiCamCV.Common;
using PiCamCV.Interfaces;

namespace PiCamCV.ConsoleApp.Runners
{
    public class CascadeRunner : CameraConsumerRunner
    {
        private readonly CascadeDetector _detector;
        public CascadeRunner(ICaptureGrab capture, string casacdeXmlContent)
            : base(capture)
        {
            _detector = new CascadeDetector(casacdeXmlContent);
        }

        public override void ImageGrabbedHandler(object sender, EventArgs e)
        {
            using (var matCaptured = new Mat())
            {
                CameraCapture.Retrieve(matCaptured);
                var input = new CascadeDetectorInput { Captured = matCaptured };
                var result = _detector.Process(input);
                if (result.IsDetected)
                {
                    Log.Info(m => m("{0}", result));
                }
            }
        }
    }
}
