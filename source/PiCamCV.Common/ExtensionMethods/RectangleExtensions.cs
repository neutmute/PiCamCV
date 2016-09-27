using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiCamCV.Common.ExtensionMethods
{
    public static class RectangleExtensions
    {
        public static int Area(this Rectangle target)
        {
            return target.Width*target.Height;
        }

        public static Point Center(this Rectangle rectangle)
        {
            var midHeight = rectangle.Height / 2;
            var midWidth = rectangle.Width / 2;

            var point = new Point(rectangle.Left + midWidth, rectangle.Top + midHeight);
            return point;
        }
    }
}
