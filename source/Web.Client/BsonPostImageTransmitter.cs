using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Web.Client
{
    public class BsonPostImageTransmitter : BsonPostTransmitter
    {
        protected override string UrlRoute => "api/pi/PostImage";
        protected override object GetPostBody(Image<Bgr, byte> image)
        {
            return image;
        }
    }
}
