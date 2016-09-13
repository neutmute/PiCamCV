using System;
using System.Threading.Tasks;
using System.Web;
using Common.Logging;
using Microsoft.AspNet.SignalR;

namespace Web
{
    public class CameraHub : Hub
    {

        private static readonly ILog Log = LogManager.GetLogger<CameraHub>();

        public override Task OnConnected()
        {
            Log.Info($"RemoteHub connection from {Context.ConnectionId}");
            return base.OnConnected();
        }

        public void Send(string name, string message)
        {
            Clients.All.broadcastMessage(name, message);
        }
    }
}