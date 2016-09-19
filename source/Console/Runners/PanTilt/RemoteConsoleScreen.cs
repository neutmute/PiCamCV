using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PiCamCV.Common.Interfaces;
using PiCamCV.ConsoleApp.Runners.PanTilt;
using Web.Client;

namespace PiCamCV.ConsoleApp.Runners
{
    public class RemoveConsoleScreen : RemoteScreen
    {
        private readonly ConsoleScreen _console;

        public RemoveConsoleScreen(ICameraToServerBus cameraToServerBus) : base(cameraToServerBus)
        {
            _console = new ConsoleScreen();
        }

        public override void WriteLine(string format, params object[] args)
        {
            base.WriteLine(format, args);
            _console.WriteLine(format, args);
        }
    }
}
