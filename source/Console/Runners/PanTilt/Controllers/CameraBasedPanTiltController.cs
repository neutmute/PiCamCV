using System;
using System.Drawing;
using PiCamCV.Common;
using PiCamCV.Interfaces;

namespace PiCamCV.ConsoleApp.Runners.PanTilt
{
    public abstract class CameraBasedPanTiltController : PanTiltController, IRunner, ICameraConsumer
    {
        protected bool Stopping { get; set; }
        private FpsTracker _fpsTracker;
        public ICaptureGrab CameraCapture { get; set; }

        protected CaptureConfig CaptureConfig { get; private set; }

        protected Point CentrePoint { get; private set; }

        protected CameraBasedPanTiltController(IPanTiltMechanism panTiltMech, ICaptureGrab captureGrabber)
            : base(panTiltMech)
        {
            _fpsTracker = new FpsTracker();
            _fpsTracker.ReportEveryNthFrame = 50;
            _fpsTracker.ReportFrames = s => Log.Info(m => m(s));

            CameraCapture = captureGrabber;
            CameraCapture.ImageGrabbed += ImageGrabbedHandler;
            CameraCapture.ImageGrabbed += _fpsTracker.NotifyImageGrabbed;

            CaptureConfig = captureGrabber.GetCaptureProperties();
            CentrePoint = new Point(CaptureConfig.Width/2, CaptureConfig.Height/2);

            Log.InfoFormat("Centre = {0}", CentrePoint);
        }


        public abstract void ImageGrabbedHandler(object sender, EventArgs e);


        public virtual void HandleKey(ConsoleKeyInfo keyInfo)
        {
            Log.Info(m => m("Ignoring key {0}", keyInfo.Key));
        }

        public void Run()
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