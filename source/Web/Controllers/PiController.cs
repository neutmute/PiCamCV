using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
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

        [HttpPost]
        [Route("api/pi/postJpeg")]
        public void PostJpeg(byte[] jpeg)
        {
            if (jpeg != null) // null sometimes while doing thresholding
            {
                PiBroker.Instance.JpegReceived(jpeg);
            }
        }

        [HttpPost]
        [Route("api/pi/postImage")]
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
