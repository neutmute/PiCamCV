using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Kraken.Core;
using PiCamCV.Common.Interfaces;

namespace PiCamCV.Common
{
    public interface IOutputProcessor
    {
        void Process(CameraProcessOutput output);
    }

    public  class FpsTracker
    {
        private int _frameCount;
        private Stopwatch _stopWatch;
        public  int ReportEveryNthFrame {get;set;}

        public Action<string> ReportFrames { get; set; }

        public FpsTracker()
        {
            ReportEveryNthFrame = 10;
        }

        public void NotifyImageGrabbed(object sender, EventArgs e)
        {
            if (_frameCount == ReportEveryNthFrame)
            {
                // minimise overhead by not performing division. pi needs all help it can get
                ReportFrames($"{_stopWatch.Elapsed.ToHumanReadable()}/{ReportEveryNthFrame} frames");
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
