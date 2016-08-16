using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PiCamCV.Common.Interfaces;

namespace PiCamCV.Common
{
    public class TrackingDetector : CameraProcessor<CameraProcessInput, CameraProcessOutput>
    {
        protected override CameraProcessOutput DoProcess(CameraProcessInput input)
        {
            throw new NotImplementedException();
        }
    }
}
