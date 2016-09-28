using System;
using System.Threading.Tasks;
using Common.Logging;
using Microsoft.AspNet.SignalR;
using PiCam.Web.Controllers;
using PiCam.Web.Models;
using PiCamCV.Common.PanTilt.Controllers;
using MessageBus = Microsoft.AspNet.SignalR.Messaging.MessageBus;

namespace Web
{
    public class BrowserHub : Hub<IBrowserClient>
    {
        private PiBroker _broker;

        public BrowserHub() : this(PiBroker.Instance)
        {
            
        }

        public BrowserHub(PiBroker broker)
        {
            _broker = broker;
        }

        public override Task OnConnected()
        {
            var ip = Context.Request.Environment["server.RemoteIpAddress"]?.ToString();
            _broker.BrowserConnected(Context.ConnectionId, ip);
            return base.OnConnected();
        }

        //public void ImageReady(string message = null)
        //{
        //    Clients.All.ImageReady(message);
        //}

        //public void hello(string message)
        //{
        //    Log.Info("Hello!");
        //}

        public void MovePanTilt(PanTiltAxis axis, int units)
        {
            _broker.CameraMoveRelative(axis,units);
        }

        public void ChangeSettings(SystemSettings settings)
        {
            _broker.ChangeSettings(settings);
        }

        public void SetMode(ProcessingMode mode)
        {
            _broker.SetMode(mode);
        }
    }
}