using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace PiCamCV
{
    /// <summary>
    /// Wrapper for https://github.com/robidouille/robidouille/tree/master/raspicam_cv
    /// Follow's EmguCV conventions
    /// </summary>
    public class CvInvokeRaspiCamCV
    {

        /// <summary>
        /// Opencv's calling convention
        /// </summary>
        public const CallingConvention CvCallingConvention = CallingConvention.Cdecl;

#if (UNIX)
        public const string OpencvRaspiCamCVLibrary = "raspicamcv";
        public const string EntryPointCapture = "raspiCamCvCreateCameraCapture";
        public const string EntryPointQuery = "raspiCamCvQueryFrame";
        public const string EntryPointRelease = "raspiCamCvReleaseCapture";
#else
        public const string OpencvRaspiCamCVLibrary = "opencv_videoio300";
        public const string EntryPointCapture = "cvCreateCameraCapture";
        public const string EntryPointQuery = "cvQueryFrame";
        public const string EntryPointRelease = "cvReleaseCapture";
#endif


        /// <summary>
        /// Allocates and initialized the CvCapture structure for reading a video stream from the camera. Currently two camera interfaces can be used on Windows: Video for Windows (VFW) and Matrox Imaging Library (MIL); and two on Linux: V4L and FireWire (IEEE1394). 
        /// </summary>
        /// <param name="index">Index of the camera to be used. If there is only one camera or it does not matter what camera to use -1 may be passed</param>
        /// <returns>Pointer to the capture structure</returns>
        [DllImport(OpencvRaspiCamCVLibrary, EntryPoint=EntryPointCapture, CallingConvention = CvCallingConvention)]
        public static extern IntPtr cvCreateCameraCapture(int index);

        
        /// <summary>
        /// Grabs a frame from camera or video file, decompresses and returns it. This function is just a combination of cvGrabFrame and cvRetrieveFrame in one call. 
        /// </summary>
        /// <param name="capture">Video capturing structure</param>
        /// <returns>Pointer to the queryed frame</returns>
        /// <remarks>The returned image should not be released or modified by user. </remarks>
        [DllImport(OpencvRaspiCamCVLibrary, EntryPoint=EntryPointQuery, CallingConvention = CvCallingConvention)]
        public static extern IntPtr cvQueryFrame(IntPtr capture);

        
        /// <summary>
        /// The function cvReleaseCapture releases the CvCapture structure allocated by cvCreateFileCapture or cvCreateCameraCapture
        /// </summary>
        /// <param name="capture">pointer to video capturing structure.</param>
        [DllImport(OpencvRaspiCamCVLibrary, EntryPoint=EntryPointRelease, CallingConvention = CvCallingConvention)]
        public static extern void cvReleaseCapture(ref IntPtr capture);

    }
}
