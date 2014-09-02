using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PiCamCV.WinForms
{
    public interface ICameraConsumer
    {
        CapturePi CameraCapture { get; set; }

        void ImageGrabbedHandler(object sender, EventArgs e);
    }

}
