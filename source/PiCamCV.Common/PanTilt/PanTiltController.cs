using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using PiCamCV.Common;
using PiCamCV.ConsoleApp.Runners.PanTilt.MoveStrategies;
using RPi.Pwm.Motors;

namespace PiCamCV.ConsoleApp.Runners.PanTilt
{
    public abstract class PanTiltController
    {
        private readonly static ILog _log = LogManager.GetCurrentClassLogger();
        protected ILog Log { get { return _log; } }

        private IPanTiltMechanism PanTiltMechanism {get;set;}

        protected IServoMotor PanServo { get { return PanTiltMechanism.PanServo; } }
        protected IServoMotor TiltServo { get { return PanTiltMechanism.TiltServo; } }

        public PanTiltSetting CurrentSetting
        {
            get
            {
                return new PanTiltSetting {PanPercent = PanServo.CurrentPercent, TiltPercent = TiltServo.CurrentPercent};
            }
        }

        protected PanTiltController(IPanTiltMechanism panTiltMech)
        {
            PanTiltMechanism = panTiltMech;
        }

        protected void MoveAbsolute(PanTiltSetting newPosition)
        {
            if (newPosition.PanPercent.HasValue)
            {
                PanServo.MoveTo(newPosition.PanPercent.Value);
            }
            if (newPosition.TiltPercent.HasValue)
            {
                TiltServo.MoveTo(newPosition.TiltPercent.Value);
            }
        }

        protected void MoveRelative(PanTiltSetting newPosition)
        {
            if (newPosition.PanPercent.HasValue)
            {
                PanServo.MoveTo(PanServo.CurrentPercent + newPosition.PanPercent.Value);
            }
            if (newPosition.TiltPercent.HasValue)
            {
                TiltServo.MoveTo(TiltServo.CurrentPercent + newPosition.TiltPercent.Value);
            }
        }
    }
}
