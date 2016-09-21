using System;
using System.Threading.Tasks;
using Common.Logging;
using Microsoft.AspNet.SignalR;
using PiCam.Web.Controllers;
using PiCamCV.Common.PanTilt.Controllers;
using MessageBus = Microsoft.AspNet.SignalR.Messaging.MessageBus;

namespace Web
{
    public class BrowserHub : Hub<IBrowserHub>
    {
        private Guid _guid = Guid.NewGuid();
        private static readonly ILog Log = LogManager.GetLogger<BrowserHub>();
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
            var ip = Context.Request.Environment["server.RemoteIpAddress"];
            Log.Info($"BrowserHub connection from {Context.ConnectionId}, {ip}");
            Clients.All.WriteLine($"Hello {Context.ConnectionId}, {ip}");
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

        public void movePanTilt(PanTiltAxis axis, int units)
        {
            Log.Info("Move request");
            _broker.CameraMoveRelative(axis,units);
        }
    }
}