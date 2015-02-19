using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using RPi.Pwm.Motors;

namespace PiCamCV.ConsoleApp.Runners.PanTilt
{
    public abstract class PanTiltController
    {
        private readonly static ILog _log = LogManager.GetCurrentClassLogger();
        protected Screen Screen {get; private set; }

        protected ILog Log { get { return _log; } }

        private IPanTiltMechanism PanTiltMechanism {get;set;}

        protected IServoMotor PanServo { get { return PanTiltMechanism.PanServo; } }
        protected IServoMotor TiltServo { get { return PanTiltMechanism.TiltServo; } }

        protected PanTiltController(IPanTiltMechanism panTiltMech)
        {
            Screen = new Screen();
            PanTiltMechanism = panTiltMech;
        }
        protected void ScreenWritePanTiltSettings()
        {
            Screen.WriteLine("Pan  = {0:F}%, {1}pwm", PanServo.CurrentPercent, PanServo.CurrentPwm);
            Screen.WriteLine("Tilt = {0:F}%, {1}pwm", TiltServo.CurrentPercent, TiltServo.CurrentPwm);
        }

    }
}
