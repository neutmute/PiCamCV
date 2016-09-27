using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Common.Logging;
using Emgu.CV;
using Emgu.CV.Structure;
using PiCamCV.Common;
using PiCamCV.Common.ExtensionMethods;
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
            else if (args.Length == 0)
            {
                Message = messageFormat;
            }
            else
            {
                Message = string.Format(messageFormat, args);
            }
        }
    }
    public class CameraConsumerUserControl : UserControl, ICameraConsumer
    {
        static ILog _Log = LogManager.GetLogger<CameraConsumerUserControl>();
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

        protected virtual void OnUnsubscribe() { }

        public void Unsubscribe()
        {
            if (CameraCapture != null)
            {
                CameraCapture.ImageGrabbed -= ImageGrabbedHandler;
            }
            OnUnsubscribe();
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
        
        protected void DrawReticle(Image<Bgr, byte> image, Point center, Color colorIn)
        {
            const int reticleRadius = 25;
            var color = colorIn.ToBgr();
            var topVert = new Point(center.X, center.Y - reticleRadius);
            var bottomVert = new Point(center.X, center.Y + reticleRadius);

            var leftHoriz = new Point(center.X - reticleRadius, center.Y);
            var rightHoriz = new Point(center.X + reticleRadius, center.Y);

            var horizontalLine = new LineSegment2D(topVert, bottomVert);
            var verticalLine = new LineSegment2D(leftHoriz, rightHoriz);

            image.Draw(horizontalLine, color, 1);
            image.Draw(verticalLine, color, 1);
        }

        protected void WriteText(Image<Bgr, byte> image, int bottom, string message)
        {
            image.Draw(message, new Point(0, bottom), Emgu.CV.CvEnum.FontFace.HersheySimplex, 0.4, new Bgr(Color.White));
        }
    }
}