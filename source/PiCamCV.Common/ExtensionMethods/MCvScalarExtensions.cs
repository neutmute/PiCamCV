using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV.Structure;

namespace PiCamCV.Common.ExtensionMethods
{
    public static class MCvScalarExtensions
    {
        public static MCvScalar WithV0(this MCvScalar target, double v0)
        {
            return new MCvScalar(v0, target.V1, target.V2, target.V3);
        }

        public static MCvScalar WithV1(this MCvScalar target, double v1)
        {
            return new MCvScalar(target.V0, v1, target.V2, target.V3);
        }

        public static MCvScalar WithV2(this MCvScalar target, double v2)
        {
            return new MCvScalar(target.V0, target.V1, v2, target.V3);
        }
    }
}
