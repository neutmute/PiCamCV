using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using Emgu.CV;
using Emgu.CV.Structure;

namespace PiCamCV.ConsoleApp
{
    public class SimpleCv : IRunner
    {
        protected static ILog Log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// c# implementation of https://github.com/robidouille/robidouille/blob/master/raspicam_cv/RaspiCamTest.c
        /// </summary>
        public void Run()
        {
            Log.Info("Creating Window");
            CvInvoke.cvNamedWindow("RaspiCamTest"); //Create the window using the specific name

            Log.Info("Creating capture");


            var captureConfig = new CaptureConfig { Width = 640, Height = 480, Framerate = 25, Monochrome = true };
            var piConfig = PiCameraConfig.FromConfig(captureConfig);

            IntPtr capture = CvInvokeRaspiCamCV.cvCreateCameraCapture2(0, ref piConfig); // Index doesn't really matter

            do
            {
                IntPtr imagePtr = CvInvokeRaspiCamCV.cvQueryFrame(capture);
                using (var managedImage = Image<Bgr, Byte>.FromIplImagePtr(imagePtr))
                {
                    CvInvoke.cvShowImage("RaspiCamTest", managedImage);
                }

            } while (CvInvoke.cvWaitKey(100) < 0);

            CvInvoke.cvDestroyWindow("RaspiCamTest");
            CvInvokeRaspiCamCV.cvReleaseCapture(ref capture);
        }
    }
}
