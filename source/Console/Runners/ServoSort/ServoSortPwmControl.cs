using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPi.Pwm;
using RPi.Pwm.Motors;
using Raspberry.IO.Components.Controllers.Pca9685;
using RPi.Pwm.Optics;

namespace PiCamCV.ConsoleApp.Runners
{
    public class ServoSortPwmControl : PwmControlBase
    {
        public ServoMotor Servo { get; private set; }
        
        public Led Led0 { get; private set; }

        public ServoSortPwmControl(IPwmDevice pwmDevice) : base(pwmDevice)
        {
            
        }

        public void Init()
        {
            Servo = new ServoMotor(PwmDevice, PwmChannel.C1, 150, 600);
            Led0 = new Led(PwmDevice, PwmChannel.C0);
        }
    }
}
