using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PiCamCV.Common.PanTilt.Controllers;
using PiCamCV.ConsoleApp.Runners.PanTilt;
using PiCamCV.ConsoleApp.Runners.PanTilt.MoveStrategies;

namespace PiCamCV.Common.PanTilt.MoveStrategies
{
    public class AutoCalibratedModifierStrategy : CameraBasedModifierStrategy
    {
        private readonly AxesCalibrationReadings _calibratedReadings;
        
        public AutoCalibratedModifierStrategy(CaptureConfig captureConfig, Point target) :base(captureConfig, target)
        {
            var readingsRepository = new CalibrationReadingsRepository();
            var readings = readingsRepository.Read();
            if (!readings.ContainsKey(captureConfig.Resolution))
            {
                readings.Add(captureConfig.Resolution, new AxesCalibrationReadings());
            }
            _calibratedReadings = readings[captureConfig.Resolution];
        }


        protected override PointD CalculatePercentDeflection(PointD pixelDeviation)
        {
            var xDeviation = (int)pixelDeviation.X;
            var yDeviation = (int) pixelDeviation.Y;
            var xDictionary = _calibratedReadings[PanTiltAxis.Horizontal];
            var yDictionary = _calibratedReadings[PanTiltAxis.Vertical];
            var xPercent = Decimal.Zero;
            var yPercent = Decimal.Zero;
            
            if (xDictionary.ContainsKey(xDeviation))
            {
                xPercent = xDictionary[xDeviation].Accepted;
            }
            
            if (yDictionary.ContainsKey(yDeviation))
            {
                yPercent = yDictionary[yDeviation].Accepted;
            }

            return new PointD(xPercent, yPercent);
        }

    }
}
