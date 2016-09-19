using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Web;

namespace PiCam.Web.Controllers
{

    

    public class MessageBus
    {
        internal IHubContext BrowserHubContext { get; set; }
        internal IHubContext CameraHubContext { get; set; }

        public MessageBus()
        {
            BrowserHubContext = GlobalHost.ConnectionManager.GetHubContext<BrowserHub>();

            CameraHubContext = GlobalHost.ConnectionManager.GetHubContext<CameraHub>();

            //CameraHubContext.
        }

        public void SendToBrowser(string message)
        {
            BrowserHubContext.Clients.All.SendStatus(message);
        }
    }
}