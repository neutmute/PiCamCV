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

        public bool ReportFramesPerSecond { get; set; }

        public FpsTracker()
        {
            ReportEveryNthFrame = 10;
        }

        public void NotifyImageGrabbed(object sender, EventArgs e)
        {
            if (_frameCount == ReportEveryNthFrame)
            {
                var m = $"{_stopWatch.Elapsed.ToHumanReadable()}/{ReportEveryNthFrame} frames";
                if (ReportFramesPerSecond)
                {
                    var fps = ReportEveryNthFrame / _stopWatch.Elapsed.TotalSeconds;
                    m += $", {fps:0.00} fps";
                }
                ReportFrames(m);
                _frameCount = 0;
            }
            if (_frameCount == 0) // needed for first image grab
            {
                _stopWatch = Stopwatch.StartNew();
            }
            _frameCount++;
        }
    }
}
