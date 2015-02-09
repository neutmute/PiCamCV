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
        public const string CVLibrary               = "raspicamcv";
        public const string EntryPointCapture       = "raspiCamCvCreateCameraCapture";
        public const string EntryPointCapture2      = "raspiCamCvCreateCameraCapture2";
        public const string EntryPointQuery         = "raspiCamCvQueryFrame";
        public const string EntryPointRelease       = "raspiCamCvReleaseCapture";
        public const string EntryPointGetProperty   = "raspiCamCvGetCaptureProperty";
        public const string EntryPointSetProperty   = "raspiCamCvSetCaptureProperty";
#else
       // Use this for Pi USB mode public const string CVLibrary           = "opencv_videoio";
        public const string CVLibrary = "opencv_videoio300";
        public const string EntryPointCapture = "cvCreateCameraCapture";
        public const string EntryPointCapture2 = "NOT SUPPORTED";
        public const string EntryPointQuery     = "cvQueryFrame";
        public const string EntryPointRelease   = "cvReleaseCapture";
        public const string EntryPointGetProperty = "cvGetCaptureProperty";
        public const string EntryPointSetProperty = "cvSetCaptureProperty";
#endif


        /// <summary>
        /// Allocates and initialized the CvCapture structure for reading a video stream from the camera. Currently two camera interfaces can be used on Windows: Video for Windows (VFW) and Matrox Imaging Library (MIL); and two on Linux: V4L and FireWire (IEEE1394). 
        /// </summary>
        /// <param name="index">Index of the camera to be used. If there is only one camera or it does not matter what camera to use -1 may be passed</param>
        /// <returns>Pointer to the capture structure</returns>
        [DllImport(CVLibrary, EntryPoint=EntryPointCapture, CallingConvention = CvCallingConvention)]
        public static extern IntPtr cvCreateCameraCapture(int index);

        [DllImport(CVLibrary, EntryPoint = EntryPointCapture2, CallingConvention = CvCallingConvention)]
        public static extern IntPtr cvCreateCameraCapture2(int index, ref PiCameraConfig config);

        
        /// <summary>
        /// Grabs a frame from camera or video file, decompresses and returns it. This function is just a combination of cvGrabFrame and cvRetrieveFrame in one call. 
        /// </summary>
        /// <param name="capture">Video capturing structure</param>
        /// <returns>Pointer to the queryed frame</returns>
        /// <remarks>The returned image should not be released or modified by user. </remarks>
        [DllImport(CVLibrary, EntryPoint=EntryPointQuery, CallingConvention = CvCallingConvention)]
        public static extern IntPtr cvQueryFrame(IntPtr capture);

        
        /// <summary>
        /// The function cvReleaseCapture releases the CvCapture structure allocated by cvCreateFileCapture or cvCreateCameraCapture
        /// </summary>
        /// <param name="capture">pointer to video capturing structure.</param>
        [DllImport(CVLibrary, EntryPoint=EntryPointRelease, CallingConvention = CvCallingConvention)]
        public static extern void cvReleaseCapture(ref IntPtr capture);


        [DllImport(CVLibrary, EntryPoint = EntryPointGetProperty, CallingConvention = CvCallingConvention)]
        public static extern double cvGetCaptureProperty(IntPtr capture, int property);


        [DllImport(CVLibrary, EntryPoint = EntryPointSetProperty, CallingConvention = CvCallingConvention)]
        public static extern int cvSetCaptureProperty(IntPtr capture, int property, double value);
    }
}
