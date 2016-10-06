using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PiCamCV.Common.Interfaces;

namespace PiCamCV.Common
{

    public class StopwatchWrapper : IStopwatch
    {
        private Stopwatch _realWatch;

        public long ElapsedMilliseconds => _realWatch.ElapsedMilliseconds;

        public TimeSpan Elapsed => _realWatch.Elapsed;

        private StopwatchWrapper()
        {

        }

        public static IStopwatch StartNew()
        {
            var wrapper = new StopwatchWrapper();
            wrapper._realWatch = Stopwatch.StartNew();
            return wrapper;
        }

        public void Restart()
        {
            _realWatch.Restart();
        }
        
    }
}
