using System;
using System.Threading.Tasks;
using System.Web;
using Common.Logging;
using Microsoft.AspNet.SignalR;
using PiCam.Web.Controllers;
using PiCamCV.ConsoleApp.Runners.PanTilt;

namespace Web
{
    public interface ICameraHub
    {
        void MoveRelative(PanTiltSetting setting);
    }
    
    public interface IBrowserHub
    {
        void WriteLine(string message);

        void ImageReady(string base64encodedImage = null);
    }

    public class CameraHub : Hub<ICameraHub>
    {
        private static readonly ILog Log = LogManager.GetLogger<BrowserHub>();
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
            var ip = Context.Request.Environment["server.RemoteIpAddress"];
            Log.Info($"CameraHub connection from {Context.ConnectionId}, {ip}");
            return base.OnConnected();
        }

        public void Message(string message)
        {
            _broker.BrowserWriteLine(message); 
        }
    }
}