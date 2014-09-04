//----------------------------------------------------------------------------
//  Copyright (C) 2004-2013 by EMGU. All rights reserved.       
//----------------------------------------------------------------------------

//#define TEST_CAPTURE
using System;
using System.Runtime.InteropServices;
using System.Threading;
using Common.Logging;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using PiCamCV.Interfaces;

namespace PiCamCV
{
    /// <summary> 
    /// Capture images from either camera or video file. 
    /// </summary>
    public class CapturePi :
        UnmanagedObject, ICaptureGrab
    {

        protected static ILog Log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// the type of flipping
        /// </summary>
        private FlipType _flipType = Emgu.CV.CvEnum.FlipType.None;

        /// <summary>
        /// The type of capture source
        /// </summary>
        public enum CaptureModuleType
        {
            /// <summary>
            /// Capture from camera
            /// </summary>
            Camera,
            /// <summary>
            /// Capture from file using HighGUI
            /// </summary>
            Highgui,
        }

        private readonly CaptureModuleType _captureModuleType;

        #region Properties
        /// <summary>
        /// Get the type of the capture module
        /// </summary>
        public CaptureModuleType CaptureSource
        {
            get
            {
                return _captureModuleType;
            }
        }

        /// <summary>
        /// Get and set the flip type
        /// </summary>
        public FlipType FlipType
        {
            get
            {
                return _flipType;
            }
            set
            {
                _flipType = value;
            }
        }

        /// <summary>
        /// Get or Set if the captured image should be flipped horizontally
        /// </summary>
        public bool FlipHorizontal
        {
            get
            {
                return (_flipType & Emgu.CV.CvEnum.FlipType.Horizontal) == Emgu.CV.CvEnum.FlipType.Horizontal;
            }
            set
            {
                if (value != FlipHorizontal)
                    _flipType ^= Emgu.CV.CvEnum.FlipType.Horizontal;
            }
        }

        /// <summary>
        /// Get or Set if the captured image should be flipped vertically
        /// </summary>
        public bool FlipVertical
        {
            get
            {
                return (_flipType & Emgu.CV.CvEnum.FlipType.Vertical) == Emgu.CV.CvEnum.FlipType.Vertical;
            }
            set
            {
                if (value != FlipVertical)
                    _flipType ^= Emgu.CV.CvEnum.FlipType.Vertical;
            }
        }

        #endregion

        #region constructors
        ///<summary> Create a capture using the default camera </summary>
        public CapturePi() : this(0)
        {
        }

        ///<summary> Create a capture using the specific camera</summary>
        ///<param name="camIndex"> The index of the camera to create capture from, starting from 0</param>
        private CapturePi(int camIndex)
        {
            CvInvoke.UseOpenCL = false;
            CvInvoke.CheckLibraryLoaded();
            _captureModuleType = CaptureModuleType.Camera;

            _ptr = CvInvokeRaspiCamCV.cvCreateCameraCapture(camIndex);
            if (_ptr == IntPtr.Zero)
            {
                throw new NullReferenceException(String.Format("Error: Unable to create capture from camera {0}", camIndex));
            }
        }
        #endregion

        #region implement UnmanagedObject
        /// <summary>
        /// Release the resource for this capture
        /// </summary>
        protected override void DisposeObject()
        {
            Stop();
            CvInvokeRaspiCamCV.cvReleaseCapture(ref _ptr);
        }
        #endregion

        /// <summary>
        /// Grab a frame
        /// </summary>
        /// <returns>True on success</returns>
        public virtual bool Grab()
        {
            if (_ptr == IntPtr.Zero)
                return false;

            CvInvokeRaspiCamCV.cvQueryFrame(_ptr);
            if (ImageGrabbed != null)
            {
                ImageGrabbed(this, new EventArgs());
            }
            return true;
        }

        #region Grab process
        /// <summary>
        /// The event to be called when an image is grabbed
        /// </summary>
        public event EventHandler ImageGrabbed;

        private enum GrabState
        {
            Stopped,
            Running,
            Pause,
            Stopping,
        }

        private volatile GrabState _grabState = GrabState.Stopped;

        private void Run()
        {
            try
            {
                while (_grabState == GrabState.Running || _grabState == GrabState.Pause)
                {
                    if (_grabState == GrabState.Pause)
                    {
                        Wait(100);
                    }
                    else if (!Grab())
                    {
                        //no more frames to grab, this is the end of the stream. We should stop.
                        _grabState = GrabState.Stopping;
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
            finally
            {
                _grabState = GrabState.Stopped;
            }
        }

        private static void Wait(int millisecond)
        {
            Thread.Sleep(millisecond);
        }

        /// <summary>
        /// Start the grab process in a sperate thread. Once started, use the ImageGrabbed event handler and RetrieveGrayFrame/RetrieveBgrFrame to obtain the images.
        /// </summary>
        public void Start()
        {
            if (_grabState != GrabState.Running)
            {
                _grabState = GrabState.Running;
                ThreadPool.QueueUserWorkItem(delegate { Run(); });
            }
        }

        /// <summary>
        /// Pause the grab process if it is running.
        /// </summary>
        public void Pause()
        {
            if (_grabState == GrabState.Running)
                _grabState = GrabState.Pause;
        }

        /// <summary>
        /// Stop the grabbing thread
        /// </summary>
        public void Stop()
        {
            if (_grabState != GrabState.Stopped)
            {
                if (_grabState != GrabState.Stopping)
                    _grabState = GrabState.Stopping;
            }
        }
        #endregion

        public virtual bool Retrieve(IOutputArray outputArray)
        {
            if (FlipType == FlipType.None)
            {
                var ptr = CvInvokeRaspiCamCV.cvQueryFrame(_ptr);
                var managedImage = Image<Bgr, Byte>.FromIplImagePtr(ptr);
                managedImage.Mat.CopyTo(outputArray);
                return true;
            }
            else
            {
                using (Mat tmp = new Mat())
                {
                    var ptr = CvInvokeRaspiCamCV.cvQueryFrame(Ptr);
                    var managedImage = Image<Bgr, Byte>.FromIplImagePtr(ptr);
                    managedImage.Mat.CopyTo(tmp);
                    CvInvoke.Flip(tmp, outputArray, FlipType);
                    return true;
                }
            }
        }

        ///// <summary> 
        ///// Retrieve a Gray image frame after Grab()
        ///// </summary>
        ///// <param name="streamIndex">Stream index. Use 0 for default.</param>
        ///// <returns> A Gray image frame</returns>
        //public virtual Image<Gray, Byte> RetrieveGrayFrame()
        //{
        //    IntPtr img = CvInvokeRaspiCamCV.cvQueryFrame(Ptr);
        //    if (img == IntPtr.Zero)
        //        return null;

        //   // var mat = new Mat(img, false);
        //    MIplImage iplImage = (MIplImage)Marshal.PtrToStructure(img, typeof(MIplImage));

        //    Image<Gray, Byte> res;
        //    if (iplImage.NChannels == 3)
        //    {  //if the image captured is Bgr, convert it to Grayscale
        //        res = new Image<Gray, Byte>(iplImage.Width, iplImage.Height);

        //        // Not sure how to give this a Mat
        //        CvInvoke.CvtColor(null, res, ColorConversion.Bgr2Gray);
        //    }
        //    else
        //    {
        //        res = new Image<Gray, byte>(iplImage.Width, iplImage.Height, iplImage.WidthStep, iplImage.ImageData);
        //    }

        //    //inplace flip the image if necessary
        //    res._Flip(FlipType);

        //    return res;
        //}

        ///// <summary> 
        ///// Retrieve a Bgr image frame after Grab()
        ///// </summary>
        ///// <param name="streamIndex">Stream index</param>
        ///// <returns> A Bgr image frame</returns>
        //public virtual Image<Bgr, Byte> RetrieveBgrFrame()
        //{
        //    IntPtr img = CvInvokeRaspiCamCV.cvQueryFrame(_ptr);

        //    if (img == IntPtr.Zero)
        //    {
        //        return null;
        //    }

        //    MIplImage iplImage = (MIplImage)Marshal.PtrToStructure(img, typeof(MIplImage));

        //    Image<Bgr, Byte> res;
        //    if (iplImage.NChannels == 1)
        //    {  //if the image captured is Grayscale, convert it to BGR
        //        throw new NotImplementedException("Not sure how to supply a Mat here");
        //        res = new Image<Bgr, Byte>(iplImage.Width, iplImage.Height);
        //        CvInvoke.CvtColor(null, res, Emgu.CV.CvEnum.ColorConversion.Gray2Bgr);
        //    }
        //    else
        //    {
        //        //res = Image<Bgr, Byte>.FromIplImagePtr(img);
        //       res = new Image<Bgr, byte>(iplImage.Width, iplImage.Height, iplImage.WidthStep, iplImage.ImageData);
        //    }

        //    //inplace flip the image if necessary
        //    res._Flip(FlipType);

        //    return res;
        //}

        ///// <summary> 
        ///// Capture a Gray image frame
        ///// </summary>
        ///// <returns> A Gray image frame</returns>
        //public virtual Image<Gray, Byte> QueryGrayFrame()
        //{
        //    return Grab() ? RetrieveGrayFrame() : null;
        //}

        #region implement ICapture
        /// <summary> 
        /// Capture a Bgr image frame
        /// </summary>
        /// <returns> A Bgr image frame</returns>
        public virtual Mat QueryFrame()
        {
            if (Grab())
            {
                var image = new Mat();
                Retrieve(image);
                return image;
            }
            else
            {
                return null;
            }
        }

        public Mat QuerySmallFrame()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
