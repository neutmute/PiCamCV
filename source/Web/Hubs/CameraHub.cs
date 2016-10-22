using System;
using System.Threading.Tasks;
using System.Web;
using Common.Logging;
using Microsoft.AspNet.SignalR;
using PiCam.Web.Controllers;

namespace Web
{
    public class CameraHub : Hub<ICameraClient>
    {
        private PiBroker _broker;

        public CameraHub(): this(PiBroker.Instance)
        {
        }

        public CameraHub(PiBroker broker)
        {
            _broker = broker;
        }

        public override Task OnConnected()
        {
            var ip = Context.Request.Environment["server.RemoteIpAddress"]?.ToString();
            _broker.CameraConnected(ip);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            _broker.CameraDisconnected();
            return base.OnDisconnected(stopCalled);
        }

        public void ScreenClear()
        {
            _broker.Browsers.ScreenClear();
        }
        
        public void ScreenWriteLine(string message)
        {
            _broker.Browsers.ScreenWriteLine(message);
        }

        public void InformIp(string ipCsv)
        {
            _broker.SetCameraLocalIp(ipCsv);
        }
    }
}