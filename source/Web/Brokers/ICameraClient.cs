using System;
using System.Drawing;
using PiCamCV.ConsoleApp.Runners.PanTilt;

namespace Web
{
    public interface ICameraClient
    {
        void MoveRelative(PanTiltSetting setting);

        void SetImageTransmitPeriod(TimeSpan transmitPeriod);

        void SetRegionOfInterest(Rectangle roi);
    }
}