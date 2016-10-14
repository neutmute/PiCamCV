using System;
using System.Collections.Generic;
using System.Drawing;
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
        //event EventHandler<PanTiltSetting> MoveAbsolute;
        //event EventHandler<PanTiltSetting> MoveRelative;
        event EventHandler<TimeSpan> SetImageTransmitPeriod;
        event EventHandler<Rectangle> SetRegionOfInterest;
        //event EventHandler<PanTiltSetting> SetPursuitBoundaryUpper;
        //event EventHandler<PanTiltSetting> SetPursuitBoundaryLower;

        event EventHandler<PanTiltSettingCommand> PanTiltCommand;
    }

    public interface ICameraToServerBus
    {
        void ScreenWriteLine(string message);

        void ScreenClear();
        
    }
}
