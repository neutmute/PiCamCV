using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using PiCamCV.Interfaces;

namespace PiCamCV.Common
{
    public abstract class CameraConsumerBase : ICameraConsumer
    {
        private readonly static ILog _Log = LogManager.GetCurrentClassLogger();

        protected ILog Log { get { return _Log; } }
        
        public ICaptureGrab CameraCapture { get; set; }

        public abstract void ImageGrabbedHandler(object sender, EventArgs e);
    }
}
