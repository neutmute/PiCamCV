using System;
using System.Drawing;
using PiCamCV.Common;
using PiCamCV.Common.PanTilt.Controllers;
using PiCamCV.ConsoleApp.Runners.PanTilt;

namespace Web
{
    public interface ICameraClient
    {
        void UpdateSettings(PiSettings settings);

        void SendCommand(PanTiltSettingCommand setting);
        
        void SetRegionOfInterest(Rectangle roi);

        void SetMode(ProcessingMode mode);
    }
}