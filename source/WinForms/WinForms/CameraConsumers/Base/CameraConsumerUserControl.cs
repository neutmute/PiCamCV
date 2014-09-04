using System;
using System.Windows.Forms;
using Emgu.CV;
using PiCamCV.Common;
using PiCamCV.Interfaces;

namespace PiCamCV.WinForms
{
    public class StatusEventArgs : EventArgs
    {
        public string Message { get; set; }
        public StatusEventArgs(string messageFormat, params object[] args)
        {
            if (messageFormat == null)
            {
                Message = null;
            }
            else
            {
                Message = string.Format(messageFormat, args);
            }
        }
    }
    public class CameraConsumerUserControl : UserControl, ICameraConsumer
    {
        public event EventHandler<StatusEventArgs> StatusUpdated;
        public ICaptureGrab CameraCapture { get; set; }

        public virtual void ImageGrabbedHandler(object sender, EventArgs e){}

        public void Subscribe()
        {
            CameraCapture.ImageGrabbed += ImageGrabbedHandler;
        }

        public void Unsubscribe()
        {
            CameraCapture.ImageGrabbed -= ImageGrabbedHandler;
        }

        public void NotifyStatus(string messageFormat, params object[] args)
        {
            if (StatusUpdated != null)
            {
                StatusUpdated(this, new StatusEventArgs(messageFormat, args));
            }
        }
    }
}