using System;
using PiCamCV.Common.PanTilt.Controllers;
using PiCamCV.ConsoleApp.Runners.PanTilt;

namespace PiCamCV.Common.Tests.PanTilt.Controllers.multimode
{
    public class PanTiltTime
    {
        public TimeSpan TimeSpan { get; set; }

        public PanTiltSetting Setting { get; set; }

        public string ToCsv(PanTiltAxis axis)
        {
            var yValue = axis == PanTiltAxis.Horizontal ? Setting.PanPercent : Setting.TiltPercent;
            return $"{TimeSpan.TotalMilliseconds},{yValue}";
        }
    }
}