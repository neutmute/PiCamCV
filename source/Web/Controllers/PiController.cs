using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Common.Logging;
using Emgu.CV;
using Emgu.CV.Structure;
using Web;

namespace PiCam.Web.Controllers
{
    public class PiController : ApiController
    {
        protected static ILog Log = LogManager.GetLogger< PiController>();
        private BrowserHub _browserHub;

        public PiController(BrowserHub browserHub)
        {
            _browserHub = browserHub;
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

        // POST api/values
        [HttpPost]
        [Route("api/pi/postImageBytes")]
        public void PostImageBytes(byte[] imageBytes)
        {
            Log.Info("Image bytes received");
            _browserHub.ImageReady();
        }

        [Route("api/pi/postImage")]
        public void PostImage(Image<Bgr, byte> imageBytes)
        {
            Log.Info("Image bytes received");
            _browserHub.ImageReady();
        }
    }
}
