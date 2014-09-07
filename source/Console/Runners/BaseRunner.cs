using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using PiCamCV.Common;
using PiCamCV.Interfaces;

namespace PiCamCV.ConsoleApp.Runners
{
    public abstract class BaseRunner : IRunner
    {
        private readonly static ILog _log = LogManager.GetCurrentClassLogger();
        protected ILog Log { get { return _log; } }

        public abstract void Run();
    }

    public abstract class CameraConsumerRunner : BaseRunner, ICameraConsumer
    {
        private FpsTracker _fpsTracker;
        protected CameraConsumerRunner(ICaptureGrab captureGrabber)
        {
            _fpsTracker = new FpsTracker();
            _fpsTracker.ReportEveryNthFrame = 50;
            _fpsTracker.ReportFrames= s => Log.Info(m=>m(s));

            CameraCapture = captureGrabber;
            CameraCapture.ImageGrabbed += ImageGrabbedHandler;
            CameraCapture.ImageGrabbed += _fpsTracker.NotifyImageGrabbed;
        }

        public ICaptureGrab CameraCapture { get; set; }

        public abstract void ImageGrabbedHandler(object sender, EventArgs e);
    }
}
