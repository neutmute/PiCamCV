using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            GlobalHost.ConnectionManager.GetHubContext<CameraHub>().Clients
            , GlobalHost.ConnectionManager.GetHubContext<BrowserHub>().Clients));

        public static PiBroker Instance => _instance.Value;

        private IHubConnectionContext<dynamic> CameraClients { get;set;}

        private IHubConnectionContext<dynamic> BrowserClients { get; set; }

        private PiBroker(
            IHubConnectionContext<dynamic> cameraClients
            , IHubConnectionContext<dynamic> browserClients)
        {
            CameraClients = cameraClients;
            BrowserClients = browserClients;
        }

        public void BrowserWriteLine(string message)
        {
            BrowserClients.All.WriteLine(message);
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

            CameraClients.All.MoveRelative(setting);
        }

    }
}