using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using Emgu.CV;
using PiCamCV.Common;
using PiCamCV.Interfaces;

namespace PiCamCV.ConsoleApp.Runners
{
    public abstract class BaseRunner : IRunner
    {
        private readonly static ILog _log = LogManager.GetLogger<BaseRunner>();
        protected ILog Log { get { return _log; } }

        public abstract void Run();
    }

    public abstract class CameraConsumerRunner : BaseRunner, ICameraConsumer
    {
        protected bool Stopping { get; set; }
        private FpsTracker _fpsTracker;

        public bool ReportFramesPerSecond
        {
            get { return _fpsTracker.ReportFramesPerSecond; }
            set { _fpsTracker.ReportFramesPerSecond = value;}
        }

        protected CameraConsumerRunner(ICaptureGrab captureGrabber)
        {
            _fpsTracker = new FpsTracker();
            _fpsTracker.ReportEveryNthFrame = 50;
            _fpsTracker.ReportFrames = s => Log.Info(s);

            CameraCapture = captureGrabber;
            CameraCapture.ImageGrabbed += ImageGrabbedHandler;
            CameraCapture.ImageGrabbed += _fpsTracker.NotifyImageGrabbed;
        }

        public ICaptureGrab CameraCapture { get; set; }

        public abstract void ImageGrabbedHandler(object sender, EventArgs e);

        public virtual void HandleKey(ConsoleKeyInfo keyInfo)
        {
            Log.Info(m => m("Ignoring key {0}", keyInfo.Key));
        }

        public override void Run()
        {
            CameraCapture.Start();

            var keyHandler = new KeyHandler();
            keyHandler.KeyEvent += keyHandler_KeyEvent;
            keyHandler.WaitForExit();

            Stop();
        }

        protected virtual void Stop()
        {
            Log.Info("Stopping");
            Stopping = true;
        }

        private void keyHandler_KeyEvent(object sender, ConsoleKeyEventArgs e)
        {
            HandleKey(e.KeyInfo);
        }
    }
}
