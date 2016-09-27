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

        /// <summary>
        /// SignalR Base64 encodes the image. 
        /// The alternative is to HTTP request the image which will be binary
        /// 
        /// Which is better for latency/througpout? Find out empirically
        /// </summary>
        public bool TransmitImageViaSignalR { get; set; } = true;
    }
}