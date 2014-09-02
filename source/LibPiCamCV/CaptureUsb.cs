using System;
using Emgu.CV;
using PiCamCV.Interfaces;

namespace PiCamCV
{
    /// <summary>
    /// Provides a facade around Emgu capture
    /// Help compare CapturePi against Emgu Capture
    /// </summary>
    public class CaptureUsb : ICaptureGrab
    {
        private readonly Capture _capture;

        public CaptureUsb()
        {
            _capture = new Capture();
        }

        public event EventHandler ImageGrabbed
        {
            add { _capture.ImageGrabbed += value; }
            remove { _capture.ImageGrabbed -= value; }
        }

        public Mat QueryFrame()
        {
            return _capture.QueryFrame();
        }

        public Mat QuerySmallFrame()
        {
            return _capture.QuerySmallFrame();
        }


        public bool FlipHorizontal
        {
            get { return _capture.FlipHorizontal; }
            set { _capture.FlipHorizontal = value; }
        }
        
        public bool FlipVertical
        {
            get { return _capture.FlipVertical; }
            set { _capture.FlipVertical = value; }
        }


        public void Start()
        {
            _capture.Start();
        }

        public void Stop()
        {
            _capture.Stop();
        }
        public void Pause()
        {
            _capture.Pause();
        }

        public bool Retrieve(IOutputArray image)
        {
            return _capture.Retrieve(image);
        }
    }
}