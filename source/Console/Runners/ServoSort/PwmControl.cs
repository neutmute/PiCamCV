using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using RPi.Pwm.Motors;
using Raspberry.IO.Components.Controllers.Pca9685;
using RPi.Pwm.Optics;

namespace PiCamCV.ConsoleApp.Runners
{
    public class PwmControl
    {
        private readonly static ILog Log = LogManager.GetCurrentClassLogger();
        IPwmDevice _pwmDevice;
        public ServoMotor Servo { get; private set; }
        
        public Led Led0 { get; private set; }

        public PwmControl(IPwmDevice pwmDevice)
        {
            _pwmDevice = pwmDevice;
        }

        public void Init()
        {
            Servo = new ServoMotor(_pwmDevice, PwmChannel.C1, 150, 600);
            Led0 = new Led(_pwmDevice, PwmChannel.C0);
        }

        
        //public void Command(PwmCommand pwmCommand)
        //{
        //    Log.DebugFormat("PwmCommand received({0})!", pwmCommand);
        //    switch (pwmCommand.Channel)
        //    {
        //        case DeviceChannel.DcMotor:
        //            var percent = pwmCommand.DutyCyclePercent;
        //            var direction = percent > 0 ? MotorDirection.Forward : MotorDirection.Reverse;
        //            DcMotor.Go(Math.Abs(percent), direction);
        //            break;
        //        case DeviceChannel.Led:
        //            Led0.On(pwmCommand.DutyCyclePercent);
        //            break;
        //        case DeviceChannel.Servo:
        //            Servo.MoveTo(pwmCommand.DutyCyclePercent);
        //            break;
        //    }
        //}
    }
}
