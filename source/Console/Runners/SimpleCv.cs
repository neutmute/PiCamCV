using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace PiCamCV.ConsoleApp
{
    public class SimpleCv : IRunner
    {
        /// <summary>
        /// c# implementation of https://github.com/robidouille/robidouille/blob/master/raspicam_cv/RaspiCamTest.c
        /// </summary>
        public void Run()
        {
            CvInvoke.NamedWindow("RaspiCamTest"); //Create the window using the specific name
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
