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
        private readonly IController<CameraPanTiltProcessOutput> _controller;

        private bool Started { get; set; }
        private bool Stopping { get; set; }

        protected FpsTracker FpsTracker {get;private set;}

        private ICaptureGrab CameraCapture { get; set; }

        IScreen Screen { get; set; }
        
        private readonly IKeyHandler _keyHandler;
        
        public CameraBasedPanTiltRunner(
            IPanTiltMechanism panTiltMech
            , ICaptureGrab captureGrabber
            , IController<CameraPanTiltProcessOutput> controller
            , IScreen screen)
            : base(panTiltMech)
        {
            _controller = controller;

            Screen = screen;

            FpsTracker = new FpsTracker();
            FpsTracker.ReportEveryNthFrame = 2;
            FpsTracker.ReportFrames = s => Screen.WriteLine(s);

            UpdateCaptureGrabber(captureGrabber);
            
            _keyHandler = controller as IKeyHandler;
        }
        
        /// <summary>
        /// So we can replace with a new framerate
        /// </summary>
        public void UpdateCaptureGrabber(ICaptureGrab captureGrabber)
        {
            if (CameraCapture != null)
            {
                CameraCapture.ImageGrabbed -= InternalImageGrabbedHandler;
                CameraCapture.Stop();
            }

            CameraCapture = captureGrabber;
            CameraCapture.ImageGrabbed += InternalImageGrabbedHandler;

            if (Started)
            {
                CameraCapture.Start();
            }
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
                var output = _controller.Process(input);

                Screen.BeginRepaint();
                Screen.WriteLine("Processing time: {0:N0}ms", output.Elapsed.TotalMilliseconds);
                Screen.WriteLine("Servo Settle Time: {0:N0}ms (Key 1=up, 2=down)", _controller.ServoSettleTime.TotalMilliseconds);
                Screen.WriteLine("Pan Tilt Before: {0}", output.PanTiltPrior);
                Screen.WriteLine("Pan Tilt After : {0}", output.PanTiltNow);
                Screen.WriteLine("Target: {0}", output.Target);
            }
        }
        
        public virtual void HandleKey(ConsoleKeyInfo keyInfo)
        {
            var servoIncrement = TimeSpan.FromMilliseconds(5);
            switch (keyInfo.KeyChar)
            {
                case '1':
                    _controller.ServoSettleTime = _controller.ServoSettleTime.Add(servoIncrement);
                    break;
                case '2':
                    if (_controller.ServoSettleTime > servoIncrement)
                    {
                        _controller.ServoSettleTime = _controller.ServoSettleTime.Add(-servoIncrement);
                    }
                    break;
                default:
                    _keyHandler?.HandleKeyPress(keyInfo.KeyChar);
                    break;
            }
        }

        public void Run()
        {
            CameraCapture.Start();
            Started = true;

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