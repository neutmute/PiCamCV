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
        private MessageBus _messageBus;

        //public BrowserHub(MessageBus messageBus)
        public BrowserHub(TestService messageBus)
        // public BrowserHub()
        {
        //    _messageBus = messageBus;
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

        //public void movePanTilt(PanTiltAxis axis, int units)
        //{
        //    Log.Info("Move request");
        //}
    }
}