using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Logging;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using PiCamCV.Common.PanTilt.Controllers;
using PiCamCV.ConsoleApp.Runners.PanTilt;
using Web;

namespace PiCam.Web.Controllers
{
    public class PiBroker
    {
        private readonly static Lazy<PiBroker> _instance = new Lazy<PiBroker>(() => new PiBroker(
            new CameraClient(GlobalHost.ConnectionManager.GetHubContext<CameraHub>().Clients)
            , new BrowserClient(GlobalHost.ConnectionManager.GetHubContext<BrowserHub>().Clients)));


        private static readonly ILog Log = LogManager.GetLogger<PiBroker>();

        private string _cameraIp;

        private bool IsCameraConnected => !string.IsNullOrEmpty(_cameraIp);

        public static PiBroker Instance => _instance.Value;

        public ICameraClient Camera { get;set;}

        public IBrowserClient Browsers { get; set; }

        private PiBroker(ICameraClient cameraClient, IBrowserClient browserClients)
        {
            Camera = cameraClient;
            Browsers = browserClients;
        }

        public void BrowserConnected(string connectionId, string ip)
        {
            var camMsg = IsCameraConnected ? $"Camera is connected from {_cameraIp}" : "Waiting for a camera to connect";
            var msg = $"Hello {connectionId} from {ip}.\r\n{camMsg}";
            Browsers.Toast(msg);
        }
        

        public void CameraConnected(string ip)
        {
            _cameraIp = ip;
            var msg = $"Camera has connected from {ip}";
            Log.Info(msg);
            Browsers.Toast(msg);
        }

        
        public void CameraMoveRelative(PanTiltAxis axis, int units)
        {
            var setting = new PanTiltSetting();
            switch (axis)
            {
                case PanTiltAxis.Horizontal:
                    setting.PanPercent = units;
                    break;
                case PanTiltAxis.Vertical:
                    setting.TiltPercent = units;
                    break;
            }

            Camera.MoveRelative(setting);
        }

    }
}