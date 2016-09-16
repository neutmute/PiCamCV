using System;
using System.Threading.Tasks;
using Common.Logging;
using Microsoft.AspNet.SignalR;
using PiCamCV.Common.PanTilt.Controllers;

namespace Web
{
    public class BrowserHub : Hub<IBrowserHub>
    {
        private Guid _guid;
        private static readonly ILog Log = LogManager.GetLogger<BrowserHub>();

        public BrowserHub()
        {
        }

        public override Task OnConnected()
        {
            Log.Info($"BrowserHub connection from {Context.ConnectionId}");
            return base.OnConnected();
        }

        public void ImageReady()
        {
            Clients.All.ImageReady();
        }

        public void MovePanTilt(PanTiltAxis axis, int units)
        {
            Log.Info("Move request");
        }
    }
}