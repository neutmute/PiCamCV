using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PiCamCV.ConsoleApp.Runners.PanTilt;

namespace PiCamCV.Common.PanTilt.Controllers.multimode
{
    public abstract class StateManager
    {
        private readonly Stopwatch _stopWatch;

        public virtual TimeSpan AbandonDetectionAfterMissing => TimeSpan.FromSeconds(5);

        public TimeSpan TimeSinceLastDetection => _stopWatch.Elapsed;

        protected IScreen Screen { get; }

        protected StateManager(IScreen screen)
        {
            _stopWatch = new Stopwatch();
            Screen = screen;
        }

        public void RegisterDetection()
        {
            _stopWatch.Restart();
        }
    }
}
