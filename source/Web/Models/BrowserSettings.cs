using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PiCam.Web.Models
{
    public class SystemSettings
    {
        public int JpegCompression { get; set; }

        public int TransmitImageEveryMilliseconds { get; set; }
    }
}