using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using Kraken.Core;
using PiCamCV.Common.PanTilt.Controllers;
using PiCamCV.ConsoleApp.Runners.PanTilt;

namespace PiCamCV.Common
{
    public class PiSettings
    {
        public TimeSpan TransmitImagePeriod { get; set; }

        public bool EnableImageTransmit { get; set; }

        public bool EnableConsoleTransmit { get; set; }

        public override string ToString()
        {
            return $"TransmitImagePeriod={TransmitImagePeriod.ToHumanReadable()}, EnableImageTransmit={EnableImageTransmit}, EnableConsoleTransmit={EnableConsoleTransmit}";
        }
    }
}

namespace PiCamCV.Common.Interfaces
{


    public interface IServerToCameraBus
    {
        event EventHandler<ProcessingMode> SetMode;
        event EventHandler<PiSettings> SettingsChanged;
        event EventHandler<Rectangle> SetRegionOfInterest;
        event EventHandler<PanTiltSettingCommand> PanTiltCommand;
        event EventHandler<CaptureConfig> UpdateCapture;
    }

    public interface ICameraToServerBus
    {
        void ScreenWriteLine(string message);

        void ScreenClear();
        
    }
}
