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
    }
}
