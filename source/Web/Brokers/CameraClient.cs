using Microsoft.AspNet.SignalR.Hubs;
using PiCamCV.ConsoleApp.Runners.PanTilt;

namespace Web
{
    public class CameraClient : ICameraClient
    {
        private readonly IHubConnectionContext<dynamic> _clients;

        public CameraClient(IHubConnectionContext<dynamic> clients)
        {
            _clients = clients;
        }

        public void MoveRelative(PanTiltSetting setting)
        {
            _clients.All.MoveRelative(setting);
        }
    }
}