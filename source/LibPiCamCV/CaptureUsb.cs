using System;
using Emgu.CV;
using Emgu.CV.CvEnum;
using PiCamCV.Interfaces;

namespace PiCamCV
{
    public class CaptureUsb : CaptureEmgu
    {
        public CaptureUsb() : base(new Capture())
        {
        }
    }
    public class CaptureFile : CaptureEmgu
    {
        public CaptureFile(string filename)
            : base(new Capture(filename))
        {
        }
    }

    /// <summary>
    /// Provides a facade around Emgu capture so we can present the same interface for CapturePi
    /// </summary>
    public abstract class CaptureEmgu : ICaptureGrab
    {
        private readonly Capture _capture;

        protected CaptureEmgu(Capture capture)
        {
            _capture = capture;
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

        public double GetCaptureProperty(CapProp index)
        {
            return _capture.GetCaptureProperty(index);
        }

        public bool SetCaptureProperty(CapProp property, double value)
        {
            return _capture.SetCaptureProperty(property, value);
        }
    }
}