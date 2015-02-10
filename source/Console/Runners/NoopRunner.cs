using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using PiCamCV.Interfaces;

namespace PiCamCV.ConsoleApp.Runners
{
    public class NoopRunner : CameraConsumerRunner
    {
        public NoopRunner(ICaptureGrab capture) : base(capture)
        {
        }

        public override void ImageGrabbedHandler(object sender, EventArgs e)
        {
            using (var matCaptured = new Mat())
            {
                CameraCapture.Retrieve(matCaptured);
            }
        }
    }
}
