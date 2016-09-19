using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PiCamCV.ConsoleApp.Runners.PanTilt;

namespace Web.Client
{
    public class RemoteScreen : IScreen
    {
        private CameraHubProxy _cameraHubProxy;

        public RemoteScreen(CameraHubProxy cameraHubProxy)
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

        public void WriteLine(string format, params object[] args)
        {
            var message = format;
            if (args.Length > 0)
            {
                message = string.Format(format, args);
            }
            _cameraHubProxy.NotifyMessage(message);
        }
    }
}
