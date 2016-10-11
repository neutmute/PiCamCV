using System;
using System.Drawing;
using PiCamCV.Common.PanTilt.Controllers;
using PiCamCV.ConsoleApp.Runners.PanTilt;

namespace Web
{
    public interface ICameraClient
    {
        void MoveRelative(PanTiltSetting setting);

        void MoveAbsolute(PanTiltSetting setting);

        void SetImageTransmitPeriod(TimeSpan transmitPeriod);

        void SetRegionOfInterest(Rectangle roi);

        void SetMode(ProcessingMode mode);
    }
}