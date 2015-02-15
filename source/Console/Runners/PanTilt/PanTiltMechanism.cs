using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raspberry.IO.Components.Controllers.Pca9685;
using RPi.Pwm.Motors;

namespace PiCamCV.ConsoleApp.Runners.PanTilt
{
    public interface IPanTiltMechanism
    {
        IServoMotor PanServo { get; }
        IServoMotor TiltServo { get; }
    }

    public class PanTiltMechanism : PwmControlBase, IPanTiltMechanism
    {
        public IServoMotor PanServo { get; private set; }

        public IServoMotor TiltServo { get; private set; }

        public PanTiltMechanism(IPwmDevice pwmDevice) : base(pwmDevice)
        {
            PanServo = new ServoMotor(PwmDevice, PwmChannel.C0, 150, 600);
            TiltServo = new ServoMotor(PwmDevice, PwmChannel.C1, 150, 600);
        }
    }
}
