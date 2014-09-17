using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV.Structure;

namespace PiCamCV.Common.ExtensionMethods
{
    public static class RangeFExtensions
    {
        public static bool IsInRange(this RangeF target, double input)
        {
            return input >= target.Min && input <= target.Max;
        }

        public static string ToStringE(this RangeF range)
        {
            return string.Format(
                "[{0}=>{1}]"
                , range.Min
                , range.Max);
        }
    }
}
