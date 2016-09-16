using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Emgu.CV;
using Emgu.CV.Structure;
using Web.App_Start;

namespace PiCam.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            TestEmguCVLoad();


            var builder = new ContainerBuilder();

            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;
            
            builder.RegisterControllers(typeof(WebApiApplication).Assembly);

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);
            builder.RegisterModule(new WebModule());

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
          //config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            // Set the dependency resolver for Web API.
            var webApiResolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = webApiResolver;

            // Set the dependency resolver for MVC.
            var mvcResolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(mvcResolver);
        }

        /// <summary>
        /// Throws an exception if it isn't going to load
        /// </summary>
        private static void TestEmguCVLoad()
        {
            using (var test = new Mat())
            {
                var f = test.ToImage<Bgr, byte>();
                f.Dispose();
            }
        }
    }
}
