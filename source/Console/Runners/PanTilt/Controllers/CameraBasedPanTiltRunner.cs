using System;
using System.Drawing;
using Emgu.CV;
using PiCamCV.Common;
using PiCamCV.Common.ExtensionMethods;
using PiCamCV.Common.Interfaces;
using PiCamCV.ConsoleApp.Runners.PanTilt.MoveStrategies;
using PiCamCV.Interfaces;

namespace PiCamCV.ConsoleApp.Runners.PanTilt
{
    public class CameraBasedPanTiltRunner : PanTiltController, IRunner
    {
        private readonly CameraBasedPanTiltController _controller;
        protected bool Stopping { get; set; }
        protected FpsTracker FpsTracker {get;private set;}
        public ICaptureGrab CameraCapture { get; set; }

        protected CaptureConfig CaptureConfig { get; private set; }
        
        public CameraBasedPanTiltRunner(
            IPanTiltMechanism panTiltMech
            , ICaptureGrab captureGrabber
            , CameraBasedPanTiltController controller
            , IScreen screen)
            : base(panTiltMech, screen)
        {
            _controller = controller;

            FpsTracker = new FpsTracker();
            FpsTracker.ReportEveryNthFrame = 2;
            FpsTracker.ReportFrames = s => Screen.WriteLine(s);

            CameraCapture = captureGrabber;
            CameraCapture.ImageGrabbed += InternalImageGrabbedHandler;

            CaptureConfig = captureGrabber.GetCaptureProperties();
        }

        private void InternalImageGrabbedHandler(object sender, EventArgs e)
        {
            FpsTracker.NotifyImageGrabbed(sender, e);

            using (var matCaptured = new Mat())
            {
                CameraCapture.Retrieve(matCaptured);
                var input = new CameraProcessInput();
                input.Captured = matCaptured;
                input.SetCapturedImage = false;
                _controller.Process(input);
            }
        }
        
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