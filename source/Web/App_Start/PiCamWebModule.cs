using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;
using Autofac.Integration.SignalR;
using PiCam.Web.Models;
using Module = Autofac.Module;

namespace Web.App_Start
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterHubs(Assembly.GetExecutingAssembly());
            builder.RegisterType<ImageCache>().SingleInstance();
        }
    }
}