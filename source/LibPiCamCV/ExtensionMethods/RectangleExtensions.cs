using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PiCamCV.ExtensionMethods
{
    public static class RectangleExtensions
    {
        public static Point Center(this Rectangle rectangle)
        {
            var midHeight = rectangle.Height/2;
            var midWidth = rectangle.Width/2;

            var point = new Point(rectangle.Left + midWidth, rectangle.Height + midHeight);
            return point;
        }
    }
}
