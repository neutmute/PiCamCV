using System;
using PiCamCV.Common.Interfaces;

namespace PiCamCV.Common.Tests.PanTilt.Controllers.multimode
{
    public class MockStopwatch : IStopwatch
    {
        private TimeSpan _elapsed;

        public void Set(TimeSpan elapsed)
        {
            _elapsed = elapsed;
        }

        public long ElapsedMilliseconds => Convert.ToInt64(_elapsed.TotalMilliseconds);

        public TimeSpan Elapsed => _elapsed;
        public void Restart()
        {
            
        }
    }
}