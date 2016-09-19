using System;
using System.Threading.Tasks;
using System.Web;
using Common.Logging;
using Microsoft.AspNet.SignalR;
using PiCam.Web.Controllers;

namespace Web
{
    public interface ICameraHub
    {
    }
    
    public interface IBrowserHub
    {
        void ImageReady(string message = null);
    }

    public class CameraHub : Hub<IBrowserHub>
    {
        private static readonly ILog Log = LogManager.GetLogger<BrowserHub>();
        private MessageBus _messageBus;

        public CameraHub(MessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public override Task OnConnected()
        {
            Log.Info($"CameraHub connection from {Context.ConnectionId}");
            return base.OnConnected();
        }

        public void Message(string message)
        {
            _messageBus.SendToBrowser(message);
        }
    }
}