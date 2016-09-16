using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Common.Logging;
using Emgu.CV;
using Emgu.CV.Structure;
using Microsoft.AspNet.SignalR;
using PiCam.Web.Models;
using Web;

namespace PiCam.Web.Controllers
{
    public class PiController : ApiController
    {
        protected static ILog Log = LogManager.GetLogger< PiController>();
        private ImageCache _imageCache;

        public PiController(ImageCache imageCache)
        {
            _imageCache = imageCache;
        }

        // GET api/values
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/values/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //[Route("api/pi/postImageBytes")]
        //public void PostImageBytes(byte[] image)
        //{
        //    Log.Info("Image bytes received");
        //    _browserHub.ImageReady();
        //}
        

        [System.Web.Mvc.Route("api/pi/postImage")]
        public void PostImage(Image<Bgr, byte> image)
        {
            Log.Info("Image bytes received");
            var browserHub = GlobalHost.ConnectionManager.GetHubContext<BrowserHub>();
            var jpeg = image.ToJpegData();

            _imageCache.ImageJpeg = jpeg;
            _imageCache.Counter++;

            browserHub.Clients.All.ImageReady();

            image.Dispose();
        }
    }
}
