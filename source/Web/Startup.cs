using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Integration.SignalR;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(PiCam.Web.Startup))]

namespace PiCam.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.MapSignalR(new HubConfiguration{EnableDetailedErrors=false, Resolver=null});
            //var config = new HubConfiguration();
            //config.Resolver =
            app.MapSignalR();
        }
    }
}
