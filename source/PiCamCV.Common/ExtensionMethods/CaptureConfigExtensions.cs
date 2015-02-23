using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PiCamCV.Common.ExtensionMethods
{
    public static class CaptureConfigExtensions
    {
        public static Point GetCenter(this CaptureConfig config)
        {
            return new Point(config.Width / 2, config.Height / 2);
        }
    }
}
