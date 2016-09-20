using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using PiCamCV.Common.PanTilt.Controllers;
using PiCamCV.ConsoleApp.Runners.PanTilt;
using Web;

namespace PiCam.Web.Controllers
{

    

    public class MessageBus
    {
        internal IHubContext BrowserHubContext { get; set; }

        internal IHubContext CameraHubContext { get; set; }

        public MessageBus()
        {
            //BrowserHubContext = GlobalHost.ConnectionManager.GetHubContext<BrowserHub>();

       //     CameraHubContext = GlobalHost.ConnectionManager.GetHubContext<CameraHub>();

            //CameraHubContext.
        }

        public void MoveRelative(PanTiltAxis axis, int units)
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

            CameraHubContext.Clients.All.MoveRelative(setting);
        }

        public void SendToBrowser(string message)
        {
          //  BrowserHubContext.Clients.All.WriteLine(message);
            BrowserHubContext.Clients.All.WriteLine($"foo!");
        }
    }
}