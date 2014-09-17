using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV.Structure;

namespace PiCamCV.ExtensionMethods
{
    public static class MCvScalarExtensions
    {
        public static string ToStringE(this MCvScalar scalar)
        {
            return string.Format(
                "[{0}, {1}, {2}, {3}]"
                ,scalar.V0
                , scalar.V1
                , scalar.V2
                , scalar.V3);
        }
    }
}
