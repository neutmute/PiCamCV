using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.CvEnum;

namespace PiCamCV.Interfaces
{
    public interface ICaptureGrab : ICapture
    {
        event EventHandler ImageGrabbed;

        bool FlipHorizontal { get; set; }
        bool FlipVertical { get; set; }

        void Start();
        void Stop();
        void Pause();
        bool Retrieve(IOutputArray image);
    }
}
