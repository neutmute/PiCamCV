using System;
using System.Threading.Tasks;
using System.Web;
using Common.Logging;
using Microsoft.AspNet.SignalR;

namespace Web
{
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
}