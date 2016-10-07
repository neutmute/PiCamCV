using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Emgu.Util;
using PiCamCV.Common;
using PiCamCV.ConsoleApp.Runners.PanTilt.MoveStrategies;
using RPi.Pwm.Motors;

namespace PiCamCV.ConsoleApp.Runners.PanTilt
{
    public interface IPanTiltController
    {
        PanTiltSetting CurrentSetting { get; }

        /// <returns>True if moved, false if move not required</returns>
        bool MoveAbsolute(PanTiltSetting newPosition);

        /// <returns>True if moved, false if move not required</returns>
        bool MoveRelative(PanTiltSetting newPosition);
    }

    public abstract class PanTiltController : DisposableObject, IPanTiltController
    {
        private readonly static ILog _log = LogManager.GetLogger< PanTiltController>();

        protected ILog Log => _log;

        private IPanTiltMechanism PanTiltMechanism {get;set;}

        protected IServoMotor PanServo => PanTiltMechanism.PanServo;

        protected IServoMotor TiltServo => PanTiltMechanism.TiltServo;

        public PanTiltSetting CurrentSetting => new PanTiltSetting {PanPercent = PanServo.CurrentPercent, TiltPercent = TiltServo.CurrentPercent};

        protected PanTiltController(IPanTiltMechanism panTiltMech)
        {
            PanTiltMechanism = panTiltMech;
        }


        /// <returns>True if moved, false if move not required</returns>
        public bool MoveAbsolute(PanTiltSetting newPosition)
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
        public bool MoveRelative(PanTiltSetting newPosition)
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

        /// <summary>
        /// abstract - must be implemented
        /// </summary>
        protected override void DisposeObject()
        {
            
        }
    }
}
