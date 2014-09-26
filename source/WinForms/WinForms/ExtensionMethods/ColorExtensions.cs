using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Emgu.CV.Structure;

namespace PiCamCV.WinForms.ExtensionMethods
{
    public static class ColorExtensions
    {
        public static Bgr ToBgr(this Color color)
        {
            return new Bgr(color);
        }

        public static Bgra ToBgra(this Color color)
        {
            return new Bgra(color.B, color.G, color.R, 1);
        }
    }
}
