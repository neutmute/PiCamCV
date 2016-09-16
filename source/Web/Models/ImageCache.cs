using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PiCam.Web.Models
{
    public class ImageCache
    {
        public byte[] ImageJpeg { get; set; }

        public int Counter { get; set; }

        public bool IsRetrieved { get; set; }
    }
}