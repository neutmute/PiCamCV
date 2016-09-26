using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using Common.Logging;
using Emgu.CV;
using Emgu.CV.Structure;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using PiCam.Web.Models;
using Web;

namespace PiCam.Web.Controllers
{
    public class PiController : ApiController
    {
        protected static ILog Log = LogManager.GetLogger< PiController>();
        private readonly ImageCache _imageCache;

        public PiController(ImageCache imageCache)
        {
            _imageCache = imageCache;
        }

        [System.Web.Mvc.Route("api/pi/postImage")]
        public void PostImage(Image<Bgr, byte> image)
        {
            var jpeg = image.ToJpegData(PiBroker.Instance.SystemSettings.JpegCompression);
            
            // left this here as it was a pain to inject into the pibroker
            _imageCache.ImageJpeg = jpeg;
            _imageCache.Counter++;

            PiBroker.Instance.ImageReceived(jpeg);

            image.Dispose();
        }
    }
}
