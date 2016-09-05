using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using PiCamCV.Common.PanTilt.Controllers;
using PiCamCV.ConsoleApp.Runners.PanTilt;
using PiCamCV.ConsoleApp.Runners.PanTilt.MoveStrategies;

namespace PiCamCV.Common.PanTilt.MoveStrategies
{
    public class AutoCalibratedModifierStrategy : CameraBasedModifierStrategy
    {
        private readonly AxesCalibrationReadings _calibratedReadings;
        private ILog Log = LogManager.GetLogger<AutoCalibratedModifierStrategy>();
        
        public AutoCalibratedModifierStrategy(CaptureConfig captureConfig, Point target) :base(captureConfig, target)
        {
            var readingsRepository = new CalibrationReadingsRepository();
            var readings = readingsRepository.Read();

            if (readings == null)
            {
                Log.WarnFormat("No calibration readings found for pan tilt");
                readings = new PanTiltCalibrationReadings();
            }

            if (!readings.ContainsKey(captureConfig.Resolution))
            {
                readings.Add(captureConfig.Resolution, new AxesCalibrationReadings());
                Log.WarnFormat("Failed to load any calibration readings for {0}", captureConfig.Resolution);
            }
            _calibratedReadings = readings[captureConfig.Resolution];
            Log.InfoFormat(
                "Using {0} horizontal, {1} vertial calibration readings for {2}"
                , _calibratedReadings.Horizontal.Count
                , _calibratedReadings.Vertical.Count
                , captureConfig.Resolution);
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
