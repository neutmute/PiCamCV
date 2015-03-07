using System;
using System.Drawing;
using PiCamCV.Common;
using PiCamCV.Common.ExtensionMethods;
using PiCamCV.ConsoleApp.Runners.PanTilt.MoveStrategies;
using PiCamCV.Interfaces;

namespace PiCamCV.ConsoleApp.Runners.PanTilt
{
    public abstract class CameraBasedPanTiltController : PanTiltController, IRunner, ICameraConsumer
    {
        protected bool Stopping { get; set; }
        protected FpsTracker FpsTracker {get;private set;}
        public ICaptureGrab CameraCapture { get; set; }

        protected CaptureConfig CaptureConfig { get; private set; }

        protected Point CentrePoint { get; private set; }

        protected int Ticks { get;set;}

        protected CameraBasedPanTiltController(IPanTiltMechanism panTiltMech, ICaptureGrab captureGrabber)
            : base(panTiltMech)
        {
            FpsTracker = new FpsTracker();
            FpsTracker.ReportEveryNthFrame = 2;
            FpsTracker.ReportFrames = s => Screen.WriteLine(s);

            CameraCapture = captureGrabber;
            CameraCapture.ImageGrabbed += InternalImageGrabbedHandler;

            CaptureConfig = captureGrabber.GetCaptureProperties();
            CentrePoint = CaptureConfig.GetCenter();

            Log.InfoFormat("Centre = {0}", CentrePoint);
            Ticks = 0;
        }

        private void InternalImageGrabbedHandler(object sender, EventArgs e)
        {
            FpsTracker.NotifyImageGrabbed(sender, e);
            Screen.BeginRepaint();
            Screen.WriteLine("Frame count={0}", Ticks++);
            ImageGrabbedHandler(sender, e);
        }
        public abstract void ImageGrabbedHandler(object sender, EventArgs e);
        
        public virtual void HandleKey(ConsoleKeyInfo keyInfo)
        {
            Log.Info(m => m("Ignoring key {0}", keyInfo.Key));
        }

        public void Run()
        {
            CameraCapture.Start();

            MoveTo(new PanTiltSetting(50, 50));

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


        protected void ReactToTarget(Point targetPoint)
        {
            var moveStrategy = new CameraModifierStrategy(CaptureConfig, Screen, targetPoint, CentrePoint);
            var newPosition = moveStrategy.CalculateNewSetting(CurrentSetting);

            MoveTo(newPosition);

            //var imageBgr = result.CapturedImage;

            Screen.WriteLine("Capture Config {0}", CaptureConfig);
            Screen.WriteLine("Target {0}", targetPoint);
            ScreenWritePanTiltSettings();
        }
    }
}