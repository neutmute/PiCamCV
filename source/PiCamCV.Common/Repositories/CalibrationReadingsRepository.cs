using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kraken.Core;
using PiCamCV.Common.PanTilt.Controllers;

namespace PiCamCV.Common
{
    public class CalibrationReadingsRepository : FileBasedRepository<PanTiltCalibrationReadings>
    {
        protected override string Filename
        {
            get { return "calibration_readings.settings.xml"; }
        }

        public void ToCsv(PanTiltCalibrationReadings calibrationReadings)
        {
            foreach (var resolution in calibrationReadings.Keys)
            {
                var sb = new StringBuilder();
                var readings = calibrationReadings[resolution];
                var resolutionCsv = string.Empty;
                var axes = new[] {PanTiltAxis.Horizontal, PanTiltAxis.Vertical};
                foreach (var axis in axes)
                {
                    var axisReading = readings[axis];
                    sb.AppendFormat("{0} Pixels,{0} Percent,SampleCount,IsInterpolated\r\n", axis);
                    foreach (var pixelDeviation in axisReading.Keys)
                    {
                        var readingSet = axisReading[pixelDeviation];
                        sb.AppendFormat(
                            "{0},{1},{2},{3}\r\n"
                            , pixelDeviation
                            , readingSet.Accepted
                            , readingSet.AllReadings.Count
                            , readingSet.IsInterpolated);
                    }

                    resolutionCsv = sb.ToString();
                }
                var filename = "res_" + resolution + ".csv";
                File.WriteAllText(filename, resolutionCsv);
            }
        }
    }
}
