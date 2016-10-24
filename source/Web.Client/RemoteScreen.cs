using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PiCamCV.Common.Interfaces;
using PiCamCV.ConsoleApp.Runners.PanTilt;

namespace Web.Client
{
    public class RemoteScreen : IScreen
    {
        private readonly ICameraToServerBus _cameraHubProxy;

        public bool Enabled { get; set; }

        public RemoteScreen(ICameraToServerBus cameraHubProxy)
        {
            _cameraHubProxy = cameraHubProxy;
            Enabled = true;
        }

        public void Clear()
        {
            _cameraHubProxy.ScreenClear();
        }

        public void BeginRepaint()
        {
            WriteLine("--REPAINT--");
        }

        public virtual void WriteLine(string format, params object[] args)
        {
            if (!Enabled)
            {
                return;
            }
            var message = format;
            if (args.Length > 0)
            {
                message = string.Format(format, args);
            }
            _cameraHubProxy.ScreenWriteLine(message);
        }
    }
}
