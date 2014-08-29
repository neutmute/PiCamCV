using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;

namespace PiCamCV.Console
{
    public class SimpleCv
    {
        /// <summary>
        /// c# implementation of https://github.com/robidouille/robidouille/blob/master/raspicam_cv/RaspiCamTest.c
        /// </summary>
        public void Run()
        {
            CvInvoke.cvNamedWindow("RaspiCamTest"); //Create the window using the specific name
            IntPtr capture = CvInvokeRaspiCamCV.cvCreateCameraCapture(0); // Index doesn't really matter

            do
            {
                IntPtr image = CvInvokeRaspiCamCV.cvQueryFrame(capture);
                CvInvoke.cvShowImage("RaspiCamTest", image);
            } while (CvInvoke.cvWaitKey(100) < 0);

            CvInvoke.cvDestroyWindow("RaspiCamTest");
            CvInvokeRaspiCamCV.cvReleaseCapture(ref capture);
        }
    }
}
