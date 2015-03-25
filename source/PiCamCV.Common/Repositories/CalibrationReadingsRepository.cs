using System;
using System.Collections.Generic;
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
    }
}
