using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiCamCV.ConsoleApp.Runners.PanTilt.MoveStrategies
{
    public class SimpleCameraModifierStrategy : IPanTiltModifierStrategy
    {
        private Point _target;
        private Point _objective;

        public SimpleCameraModifierStrategy(Point objective, Point target)
        {
            _objective = objective;
            _target = target;
        }
        public PanTiltSetting CalculateNewSetting(PanTiltSetting currentSetting)
        {
            var newSetting = new PanTiltSetting();
            return newSetting;
        }
    }
}
