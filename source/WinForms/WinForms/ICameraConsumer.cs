using System;
using PiCamCV.Interfaces;

namespace PiCamCV.WinForms
{
    public interface ICameraConsumer
    {
        ICaptureGrab CameraCapture { get; set; }

        void ImageGrabbedHandler(object sender, EventArgs e);
    }
}