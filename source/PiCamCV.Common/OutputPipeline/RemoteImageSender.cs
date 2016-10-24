using System;
using System.Diagnostics;
using PiCamCV.Common.Interfaces;

namespace PiCamCV.Common
{
    public class RemoteImageSender : IOutputProcessor
    {
        private readonly IImageTransmitter _imageTransmitter;

        public TimeSpan SendEveryPeriod { get; set; }

        public bool Enabled { get; set; }

        private Stopwatch _sinceLastSend;

        public RemoteImageSender(IImageTransmitter imageTransmitter, IServerToCameraBus serverToCameraBus)
        {
            _imageTransmitter = imageTransmitter;
            SendEveryPeriod = TimeSpan.FromMilliseconds(200);
            _sinceLastSend = Stopwatch.StartNew();
            Enabled = true;
            serverToCameraBus.SettingsChanged += (s, e) => SendEveryPeriod = e.TransmitImagePeriod;
        }

        public void Process(CameraProcessOutput output)
        {
            if (!Enabled)
            {
                return;
            }
            if (_sinceLastSend.ElapsedMilliseconds >= SendEveryPeriod.TotalMilliseconds)
            {
                _imageTransmitter.Transmit(output.CapturedImage);
                _sinceLastSend = Stopwatch.StartNew();
            }
        }
    }
}