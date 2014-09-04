using System;
using PiCamCV.Interfaces;

namespace PiCamCV.Common
{
    public interface ICameraConsumer
    {
        ICaptureGrab CameraCapture { get; set; }

        void ImageGrabbedHandler(object sender, EventArgs e);
    }
}