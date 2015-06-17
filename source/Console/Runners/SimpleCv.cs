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
            var windowName = "PiCamCVSimpleTest";
            CvInvoke.NamedWindow(windowName); //Create the window using the specific name

            Log.Info("Creating capture");

            EnvironmentService.DemandUnix("OpenCV 3.0 deprecated these capture methods. Only supported with PiCamCv on Pi");
            var captureConfig = new CaptureConfig { Resolution= new Resolution(640,480) , Framerate = 25, Monochrome = true };
            var piConfig = PiCameraConfig.FromConfig(captureConfig);

            IntPtr capture = CvInvokeRaspiCamCV.cvCreateCameraCapture2(0, ref piConfig); // Index doesn't really matter

            do
            {
                IntPtr imagePtr = CvInvokeRaspiCamCV.cvQueryFrame(capture);
                using (var managedImage = Image<Bgr, Byte>.FromIplImagePtr(imagePtr))
                {
                    CvInvoke.Imshow(windowName, managedImage);
                }

            } while (CvInvoke.WaitKey(100) < 0);

            CvInvoke.DestroyWindow("RaspiCamTest");
            CvInvokeRaspiCamCV.cvReleaseCapture(ref capture);
        }
    }
}
