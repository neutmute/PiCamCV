using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;

namespace PiCamCV.ConsoleApp.Runners.PanTilt.MoveStrategies
{
    public class SimpleCameraModifierStrategy : IPanTiltModifierStrategy
    {
        private Point _target;
        private Point _objective;


        private readonly static ILog _log = LogManager.GetCurrentClassLogger();

        public SimpleCameraModifierStrategy(Point objective, Point target)
        {
            _objective = objective;
            _target = target;
        }
        public PanTiltSetting CalculateNewSetting(PanTiltSetting currentSetting)
        {
            var newSetting = new PanTiltSetting();

            var xDiff = _target.X - _objective.X;
            var yDiff = _target.Y - _objective.Y;

            newSetting.PanPercent = currentSetting.PanPercent.Value + xDiff;
            newSetting.TiltPercent = currentSetting.TiltPercent.Value + yDiff;

            return currentSetting;
        }
    }
}
