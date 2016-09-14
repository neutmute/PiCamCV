using System;
using System.Threading.Tasks;
using System.Web;
using Common.Logging;
using Microsoft.AspNet.SignalR;

namespace Web
{
    public enum PanTiltDirection
    {
        Unknown = 0,
        Pan,
        Tilt
    }

    public interface ICameraHub
    {
    }


    public interface IBrowserHub
    {
        
        void ImageReady();
    }

    public class CameraHub : Hub<IBrowserHub>
    {
        private static readonly ILog Log = LogManager.GetLogger<BrowserHub>();

        public override Task OnConnected()
        {
            Log.Info($"CameraHub connection from {Context.ConnectionId}");
            return base.OnConnected();
        }
    }

    public class BrowserHub : Hub<IBrowserHub>
    {

        private static readonly ILog Log = LogManager.GetLogger<BrowserHub>();

        public override Task OnConnected()
        {
            Log.Info($"BrowserHub connection from {Context.ConnectionId}");
            return base.OnConnected();
        }

        public void ImageReady()
        {
            Clients.All.ImageReady();
        }

        public void MovePanTilt(PanTiltDirection direction, int units)
        {
            Log.Info("Move request");
        }
    }
}