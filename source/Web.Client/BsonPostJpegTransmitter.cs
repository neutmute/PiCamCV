using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Emgu.CV;
using Emgu.CV.Structure;
using PiCamCV.Common.Interfaces;

namespace Web.Client
{
    public class BsonPostJpegTransmitter : BsonPostTransmitter
    {
        public int Quality { get; set; } = 20;

        protected override string UrlRoute => "api/pi/PostJpeg";
        protected override object GetPostBody(Image<Bgr, byte> image)
        {
            return image.ToJpegData(Quality);
        }
    }
}
