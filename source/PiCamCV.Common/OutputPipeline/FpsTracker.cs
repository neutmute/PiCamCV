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

    public class RemoteImageSender : IOutputProcessor
    {
        private readonly IImageTransmitter _imageTransmitter;

        public TimeSpan SendEveryPeriod { get; set; }

        private Stopwatch _sinceLastSend;

        public RemoteImageSender(IImageTransmitter imageTransmitter, IServerToCameraBus serverToCameraBus)
        {
            _imageTransmitter = imageTransmitter;
            SendEveryPeriod = TimeSpan.FromMilliseconds(200);
            _sinceLastSend = Stopwatch.StartNew();

            serverToCameraBus.SettingsChanged += (s, e) => SendEveryPeriod = e.TransmitImagePeriod;
        }

        public void Process(CameraProcessOutput output)
        {
            if (_sinceLastSend.ElapsedMilliseconds >= SendEveryPeriod.TotalMilliseconds)
            {
                _imageTransmitter.Transmit(output.CapturedImage);
                _sinceLastSend = Stopwatch.StartNew();
            }
        }
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
