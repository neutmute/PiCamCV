using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PiCamCV.ConsoleApp.Runners.PanTilt;
using RPi.Pwm;

namespace PiCamCV.ConsoleApp.Runners
{
    public class PanTiltRunner : BaseRunner
    {
        private PanTiltMechanism _panTiltMechanism;

        public PanTiltRunner(ConsoleOptions options)
        {
            var pwmDeviceFactory = new Pca9685DeviceFactory();
            var pwmDevice = pwmDeviceFactory.GetDevice(options.UseFakeDevice);
            _panTiltMechanism = new PanTiltMechanism(pwmDevice);
        }

        public override void Run()
        {
            var keyHandler = new KeyHandler();
            keyHandler.WaitForExit();
        }
    }
}
