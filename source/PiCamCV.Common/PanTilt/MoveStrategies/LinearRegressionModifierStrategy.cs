using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using PiCamCV.Common.PanTilt.Controllers;
using PiCamCV.Common.PanTilt.MoveStrategies;

namespace PiCamCV.ConsoleApp.Runners.PanTilt.MoveStrategies
{
    public class LinearRegressionModifierStrategy : IPanTiltModifierStrategy
    {
        private Point _target;

        private readonly decimal _xDiffScale, _yDiffScale;

        private LinearRegressorPair _regressorPair;

        public Point Objective { get; set; }




        public LinearRegressionModifierStrategy(CaptureConfig captureConfig, Point target)
        {
            _target = target;

            _regressorPair = new LinearRegressorFactory().GetHandMeasured320x240();

            // calibration was done in 320x240. If capture settings different need to scale the calibration
            _xDiffScale = captureConfig.Resolution.Width /320m;
            _yDiffScale = captureConfig.Resolution.Height / 240m;
        }
        public PanTiltSetting CalculateNewSetting(PanTiltSetting currentSetting)
        {
            var newSetting = currentSetting.Clone();

            var xDiff = (_target.X - Objective.X) * _xDiffScale;
            var yDiff = (_target.Y - Objective.Y) * _yDiffScale;

            const int deadZone = 10;
            var xDeflection = Math.Abs(xDiff) > deadZone ?_regressorPair[PanTiltAxis.Horizontal].Calculate((int) xDiff) : 0;
            var yDeflection = Math.Abs(yDiff) > deadZone ? _regressorPair[PanTiltAxis.Vertical].Calculate((int)yDiff) : 0;

            newSetting.PanPercent += xDeflection;
            newSetting.TiltPercent += yDeflection;

            return newSetting;
        }
    }
}
