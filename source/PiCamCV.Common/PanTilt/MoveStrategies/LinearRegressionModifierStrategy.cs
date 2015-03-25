using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;

namespace PiCamCV.ConsoleApp.Runners.PanTilt.MoveStrategies
{
    public class LinearRegressionModifierStrategy : IPanTiltModifierStrategy
    {
        private Point _target;

        private readonly decimal _xDiffScale, _yDiffScale;

        private readonly static ILog Log = LogManager.GetCurrentClassLogger();

        public Point Objective { get; set; }


        public LinearRegressionModifierStrategy(CaptureConfig captureConfig, Point target)
        {
            _target = target;

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
            /*
             * Linear regression calculated from observation
             * https://github.com/neutmute/PiCamCV/blob/master/docs/panTiltCalibration.xlsx
             */
            var xDeflection = Math.Abs(xDiff) > deadZone ? 0.0933m * xDiff + 0.0864m : 0;
            var yDeflection = Math.Abs(yDiff) > deadZone ? 0.0812m * yDiff + 0.2091m : 0;

            newSetting.PanPercent += xDeflection;
            newSetting.TiltPercent += yDeflection;

            //var message1 = string.Format(
            //    "Target={0}, Objective={1}"
            //    , _target
            //    , Objective
            //    );

            //var message2 = string.Format(
            //    "Moving {0} -> {1}"
            //    , currentSetting
            //    , newSetting
            //    );

            //Log.Info(message1);
            //Log.Info(message2);

            return newSetting;
        }
    }
}
