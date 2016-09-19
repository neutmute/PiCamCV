using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PiCamCV.Common.PanTilt.Controllers;
using PiCamCV.ConsoleApp.Runners.PanTilt;

namespace PiCamCV.Common.Interfaces
{
    public interface IServerToCameraBus
    {
        event EventHandler<ProcessingMode> SetMode;
        event EventHandler<PanTiltSetting> MoveAbsolute;
        event EventHandler<PanTiltSetting> MoveRelative;
        //void NotifyMessage(string message);
    }

    public interface ICameraToServerBus
    {
        void Message(string message);
    }
}
