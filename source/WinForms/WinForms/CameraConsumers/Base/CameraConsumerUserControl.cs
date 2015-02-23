using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Common.Logging;
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
        static ILog _Log = LogManager.GetCurrentClassLogger();
        protected ILog Log { get { return _Log; } }

        public event EventHandler<StatusEventArgs> StatusUpdated;
        public ICaptureGrab CameraCapture { get; set; }

        public virtual void ImageGrabbedHandler(object sender, EventArgs e){}

        public void Subscribe()
        {

            if (CameraCapture != null)
            {
                CameraCapture.ImageGrabbed += ImageGrabbedHandler;
            }
            OnSubscribe();
            
        }

        protected virtual void OnSubscribe() { }

        public void Unsubscribe()
        {
            if (CameraCapture != null)
            {
                CameraCapture.ImageGrabbed -= ImageGrabbedHandler;
            }
        }
        
        protected void InvokeUI(Action action)
        {
            Invoke((MethodInvoker) (() => action()));
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