using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Kraken.Core;

namespace PiCamCV.WinForms
{
    public  class FpsTracker
    {
        private int _frameCount;
        private Stopwatch _stopWatch;
        private const int ReportEveryNthFrame = 10;

        public Action<string> ReportFrames { get; set; }

        public void NotifyImageGrabbed(object sender, EventArgs e)
        {
            if (_frameCount == ReportEveryNthFrame)
            {
                // minimise overhead by not performing division. pi needs all help it can get
                ReportFrames(string.Format("{0}/{1} frames", _stopWatch.Elapsed.ToHumanReadable(), ReportEveryNthFrame));
                _frameCount = 0;
            }
            if (_frameCount == 0)
            {
                _stopWatch = Stopwatch.StartNew();
            }
            _frameCount++;
        }
    }
}
