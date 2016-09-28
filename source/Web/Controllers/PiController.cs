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
        
        [System.Web.Mvc.Route("api/pi/postImage")]
        public void PostImage(Image<Bgr, byte> image)
        {
            if (image != null) // null sometimes while doing thresholding
            {
                PiBroker.Instance.ImageReceived(image);
                image.Dispose();
            }
        }
    }
}
