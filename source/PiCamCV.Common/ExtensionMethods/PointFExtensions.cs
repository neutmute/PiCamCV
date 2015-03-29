using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PiCamCV.Common.ExtensionMethods
{
    public static class PointFExtensions
    {
        public static Point ToPoint(this PointF input)
        {
            if (float.IsNaN(input.X) || float.IsNaN(input.Y))
            {
                return new Point(0,0);
            }
            return new Point(Convert.ToInt32(input.X), Convert.ToInt32(input.Y));
        }
    }
}
