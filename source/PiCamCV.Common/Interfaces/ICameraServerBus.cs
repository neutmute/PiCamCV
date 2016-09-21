using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using PiCamCV.Common.PanTilt.Controllers;
using PiCamCV.ConsoleApp.Runners.PanTilt;

namespace PiCamCV.Common.Interfaces
{
    public interface IServerToCameraBus
    {
        event EventHandler<ProcessingMode> SetMode;
        event EventHandler<PanTiltSetting> MoveAbsolute;
        event EventHandler<PanTiltSetting> MoveRelative;
        event EventHandler<TimeSpan> SetImageTransmitPeriod;
    }

    public interface ICameraToServerBus
    {
        void ScreenWriteLine(string message);

        void ScreenClear();
        
    }
}
