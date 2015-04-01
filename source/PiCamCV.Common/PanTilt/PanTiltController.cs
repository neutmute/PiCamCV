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


        /// <returns>True if moved, false if move not required</returns>
        protected bool MoveAbsolute(PanTiltSetting newPosition)
        {
            var moved = false;
            if (newPosition.PanPercent.HasValue)
            {
                moved |= PanServo.MoveTo(newPosition.PanPercent.Value);
            }
            if (newPosition.TiltPercent.HasValue)
            {
                moved |= TiltServo.MoveTo(newPosition.TiltPercent.Value);
            }
            return moved;
        }


        /// <returns>True if moved, false if move not required</returns>
        protected bool MoveRelative(PanTiltSetting newPosition)
        {
            bool moved = false;
            if (newPosition.PanPercent.HasValue)
            {
                moved |= PanServo.MoveTo(PanServo.CurrentPercent + newPosition.PanPercent.Value);
            }
            if (newPosition.TiltPercent.HasValue)
            {
                moved |= TiltServo.MoveTo(TiltServo.CurrentPercent + newPosition.TiltPercent.Value);
            }
            return moved;
        }
    }
}
