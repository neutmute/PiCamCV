using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using RPi.Pwm;
using RPi.Pwm.Motors;
using Raspberry.IO.Components.Controllers.Pca9685;
using RPi.Pwm.Optics;

namespace PiCamCV.ConsoleApp.Runners
{
    public class PwmControlBase
    {
        private readonly static ILog _Log = LogManager.GetCurrentClassLogger();
        //private readonly Dictionary<PwmChannel, PwmComponentBase> _components;

        protected ILog Log { get { return _Log; } }

        protected IPwmDevice PwmDevice { get; private set; }

        protected PwmControlBase(IPwmDevice pwmDevice)
        {
            PwmDevice = pwmDevice;
            // _components = new Dictionary<PwmChannel, PwmComponentBase>();
        }
    }

    public class PwmControl : PwmControlBase
    {
        public ServoMotor Servo { get; private set; }
        
        public Led Led0 { get; private set; }

        public PwmControl(IPwmDevice pwmDevice) : base(pwmDevice)
        {
            
        }

        public void Init()
        {
            Servo = new ServoMotor(PwmDevice, PwmChannel.C1, 150, 600);
            Led0 = new Led(PwmDevice, PwmChannel.C0);
        }
    }
}
