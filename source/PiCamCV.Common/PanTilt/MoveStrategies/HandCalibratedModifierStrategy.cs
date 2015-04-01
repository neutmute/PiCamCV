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
    public class HandCalibratedModifierStrategy : CameraBasedModifierStrategy
    {
        private readonly LinearRegressorPair _regressorPair;
        
        public HandCalibratedModifierStrategy(CaptureConfig captureConfig, Point target) : base(captureConfig, target)
        {

            _regressorPair = new LinearRegressorFactory().GetHandMeasured320x240();

            // calibration was done in 320x240. If capture settings different need to scale the calibration
            decimal xDiffScale = captureConfig.Resolution.Width / 320m;
            decimal yDiffScale = captureConfig.Resolution.Height / 240m;

            Scale = new PointD(xDiffScale, yDiffScale);
        }
        
        protected override PointD CalculatePercentDeflection(PointD pixelDeviation)
        {
            var xPercent = _regressorPair.PanAxis.Calculate(pixelDeviation.X);
            var yPercent = _regressorPair.PanAxis.Calculate(pixelDeviation.Y);
            return new PointD(xPercent, yPercent);
        }
    }
}

