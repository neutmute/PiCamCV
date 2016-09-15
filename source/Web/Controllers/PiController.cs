using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Common.Logging;
using Emgu.CV;
using Emgu.CV.Structure;

namespace PiCam.Web.Controllers
{
    public class PiController : ApiController
    {
        protected static ILog Log = LogManager.GetLogger< PiController>();

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
        }

        [Route("api/pi/postImage")]
        public void PostImage(Image<Bgr, byte> imageBytes)
        {
            Log.Info("Image bytes received");
        }
    }
}
