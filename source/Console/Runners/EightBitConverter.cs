using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PiCamCV.ConsoleApp.Runners
{
    /// <summary>
    /// Calculate the 8 bit color for the opencv_createsamples bgcolor parameter
    /// </summary>
    public class EightBitConverter : BaseRunner
    {
        public override void Run()
        {
            var rgb = Color.FromArgb(242, 242, 242);

            var red = (byte)(rgb.R * 8 / 256);
            var green = (byte)(rgb.G * 8 / 256);
            var blue = (byte)(rgb.B * 4 / 256);
            var eightBit = (red << 5) | (green << 2) | blue;

            Log.Info(m => m("8 bit color is {0}", eightBit));
        }
    }
}
