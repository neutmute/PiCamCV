using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;

namespace PiCamCV.Common.Interfaces
{
    public interface IImageTransmitter
    {
        Task Transmit(Image<Bgr, byte> image);
    }
}
