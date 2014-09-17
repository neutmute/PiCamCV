using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PiCamCV.ConsoleApp.Runners;
using PiCamCV.Interfaces;
using Raspberry.IO.Components.Controllers.Pca9685;
using RPi.Pwm;
using Emgu.CV;
using Emgu.CV.Structure;
using PiCamCV.Common;

namespace PiCamCV.ConsoleApp.Runners
{

    public  class ServoSorter : CameraConsumerRunner
    {
        private int _waitTimeMs;
        PwmControl _pwmControl;
        bool _objectCurrentlyDetected;

        public ColourDetectSettings Settings { get; set; }
        
        public ServoSorter(
            ICaptureGrab capture
            ,ConsoleOptions options) : base(capture)
        {
            _waitTimeMs = 100;


            Settings = options.ColourSettings;

            var deviceFactory = new Pca9685DeviceFactory();
            var device = deviceFactory.GetDevice(options.UseFakeDevice);
            SetLogLevel(device);

            _pwmControl = new PwmControl(device);
            _pwmControl.Init();
        }

        private void SetLogLevel(IPwmDevice device)
        {
            var stubPwmDevice = device as PwmDeviceStub;
            if (stubPwmDevice != null)
            {
                // Remove sign of life which is too chatty
                stubPwmDevice.LogChannels.Remove(PwmChannel.C0);
            }
        }

        public override void Run()
        {
            StartLedSignOfLifeAsync();
            base.Run();
        }
        public override void ImageGrabbedHandler(object sender, EventArgs e)
        {
            using (var matCaptured = new Mat())
            {
                CameraCapture.Retrieve(matCaptured);
                var detector = new ColourDetector();
                var input = new ColourDetectorInput
                {
                    Captured = matCaptured
                   ,Settings = Settings
                   ,SetCapturedImage = false
                };

                var result = detector.Process(input);

                if (result.IsDetected)
                {
                    _objectCurrentlyDetected = true;
                    SweeperArmToFull();
                    Log.Info(result);
                }
                else
                {
                    if (_objectCurrentlyDetected)
                    {
                        SweeperArmReset();
                        _objectCurrentlyDetected = false;
                    }
                }
            }
        }

        private void SweeperArmToFull()
        {
            _pwmControl.Servo.MoveTo(100);
        }

        private void SweeperArmReset()
        {
            _pwmControl.Servo.MoveTo(0);
        }

        private async Task StartLedSignOfLifeAsync()
        {
            int i = 0;
            Log.Info("Sign of life: on");
            while (!Stopping && i < int.MaxValue)
            {
                i++;
                var percentage = (int)(50 * (1 + Math.Sin(i / 20f)));
                _pwmControl.Led0.On(percentage);
                await Task.Delay(50).ConfigureAwait(false);
            }
            Log.Info("Sign of life: off");
        }

        public override void HandleKey(ConsoleKeyInfo keyInfo)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.T:
                    _waitTimeMs -= 10;
                    Log.Info(m =>m("_waitTimeMs={0}", _waitTimeMs));
                    break;
            }
        }
    }
}

