using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using PiCamCV.ConsoleApp.Runners;
using PiCamCV.Interfaces;

namespace PiCamCV.ConsoleApp.Runners
{

    public  class ServoSorter : CameraConsumerRunner
    {
        private int _waitTimeMs;

        public ServoSorter(ICaptureGrab capture) : base(capture)
        {
            _waitTimeMs = 100;
        }
        public override void ImageGrabbedHandler(object sender, EventArgs e)
        {
            Thread.Sleep(_waitTimeMs);
            //  Log.Info("Image!");
        }

        public override void HandleKey(ConsoleKeyInfo keyInfo)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.T:
                    _waitTimeMs -= 10;
                    Log.Info(m =>m("_waitTimeMs={0}", _waitTimeMs));
                    break;
            }
        }
    }
}

