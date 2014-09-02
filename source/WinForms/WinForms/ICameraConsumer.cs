using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PiCamCV.WinForms
{
    public interface ICameraConsumer
    {
        CapturePi CameraCapture { get; set; }

        void ImageGrabbedHandler(object sender, EventArgs e);
    }

    public class StatusEventArgs :EventArgs
    {
        public string Message{get;set;}
        public StatusEventArgs(string messageFormat, params object[] args)
        {
            Message = string.Format(messageFormat, args);
        }
    }

    public class CameraConsumerUserControl : UserControl, ICameraConsumer
    {
        public event EventHandler<StatusEventArgs> StatusUpdated;
        public CapturePi CameraCapture {get;set;}

        public virtual void ImageGrabbedHandler(object sender, EventArgs e){}

        public void Subscribe()
        {
            CameraCapture.ImageGrabbed += ImageGrabbedHandler;
        }

        public void Unsubscribe()
        {
            CameraCapture.ImageGrabbed -= ImageGrabbedHandler;
            NotifyStatus(string.Empty);
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
