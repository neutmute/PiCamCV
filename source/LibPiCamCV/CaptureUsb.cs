using System;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.Util;
using PiCamCV.Common;
using PiCamCV.Interfaces;

namespace PiCamCV
{
    public class CaptureUsb : CaptureEmgu
    {
        public CaptureUsb(CaptureConfig config) : this(0, config)
        {
        }

        public CaptureUsb(int index, CaptureConfig config)
            : base(new VideoCapture(index))
        {
            RequestedConfig = config;
            SetCaptureProperty(CapProp.FrameHeight,config.Resolution.Height);
            SetCaptureProperty(CapProp.FrameWidth, config.Resolution.Width);
            SetCaptureProperty(CapProp.Monochrome, config.Monochrome ? 1 : 0);
        }
    }

    public class CaptureFile : CaptureEmgu
    {
        public CaptureFile(string filename)
            : base(new VideoCapture(filename))
        {
        }
    }

    /// <summary>
    /// Provides a facade around Emgu capture so we can present the same interface for CapturePi
    /// </summary>
    public abstract class CaptureEmgu : DisposableObject, ICaptureGrab
    {
        private readonly VideoCapture _capture;

        protected CaptureEmgu(VideoCapture capture)
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


        public CaptureConfig RequestedConfig { get; internal set; }

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

        protected override void DisposeObject()
        {
            _capture.Dispose();
        } 
    }
}