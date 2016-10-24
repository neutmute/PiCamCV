using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PiCam.Web.Models
{

    public class PiSettingsModel
    {
        public int TransmitImageEveryMilliseconds { get; set; }
        public bool EnableConsoleTransmit { get; set; }
        public bool EnableImageTransmit { get; set; }
    }

    /// <summary>
    /// Between server and browser
    /// </summary>
    public class ServerSettings
    {
        public int JpegCompression { get; set; }
        
        /// <summary>
        /// SignalR Base64 encodes the image. 
        /// The alternative is to HTTP request the image which will be binary
        /// 
        /// Which is better for latency/througpout? Find out empirically
        /// </summary>
        public bool TransmitImageViaSignalR { get; set; } = true;

        public bool ShowRegionOfInterest { get; set; }

        public int RegionOfInterestPercent { get; set; } = 40;
    }
}