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
            CvInvoke.NamedWindow("RaspiCamTest"); //Create the window using the specific name

            Log.Info("Creating capture");
            IntPtr capture = CvInvokeRaspiCamCV.cvCreateCameraCapture(0); // Index doesn't really matter

            do
            {
                IntPtr imagePtr = CvInvokeRaspiCamCV.cvQueryFrame(capture);
                using (var managedImage = Image<Bgr, Byte>.FromIplImagePtr(imagePtr))
                {
                    CvInvoke.Imshow("RaspiCamTest", managedImage);
                }

            } while (CvInvoke.WaitKey(100) < 0);

            CvInvoke.DestroyWindow("RaspiCamTest");
            CvInvokeRaspiCamCV.cvReleaseCapture(ref capture);
        }
    }
}
