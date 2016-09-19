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
        private ICameraToServerBus _cameraHubProxy;

        public RemoteScreen(ICameraToServerBus cameraHubProxy)
        {
            _cameraHubProxy = cameraHubProxy;
        }

        public void Clear()
        {
            WriteLine("--CLEAR--");
        }

        public void BeginRepaint()
        {
            WriteLine("--REPAINT--");
        }

        public virtual void WriteLine(string format, params object[] args)
        {
            var message = format;
            if (args.Length > 0)
            {
                message = string.Format(format, args);
            }
            _cameraHubProxy.Message(message);
        }
    }
}
