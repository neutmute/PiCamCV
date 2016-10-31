using System;
using System.Diagnostics;
using System.Drawing;
using Kraken.Core;
using PiCamCV.ConsoleApp.Runners.PanTilt;

namespace PiCamCV.Common.PanTilt.Controllers.multimode
{
    public abstract class TimeoutStateManager<TOutput> : StateManager
        where TOutput: TrackingCameraPanTiltProcessOutput
    {
        private readonly Stopwatch _stopWatch;
        private readonly ProcessingMode _trackingMode;

        public Point LastDetection { get; set; }
        
        public virtual TimeSpan AbandonDetectionAfterMissing => TimeSpan.FromSeconds(5);

        public TimeSpan TimeSinceLastDetection => _stopWatch.Elapsed;

        public void RegisterDetection()
        {
            _stopWatch.Restart();
        }

        protected TimeoutStateManager(ProcessingMode trackingMode, IScreen screen) :base(screen)
        {
            _trackingMode = trackingMode;
            _stopWatch = new Stopwatch();
        }

        public ProcessingMode AcceptScan(TOutput output)
        {
            if (output.IsDetected)
            {
                LastDetection = output.Target;
                RegisterDetection();

                if (LastDetection == Point.Empty)
                {
                    Screen.WriteLine("Object detected");
                }
            }

            if (LastDetection != Point.Empty && TimeSinceLastDetection > AbandonDetectionAfterMissing)
            {
                Screen.WriteLine($"Object deemed lost afer {AbandonDetectionAfterMissing.ToHumanReadable()}");
                return ProcessingMode.Autonomous;
            }

            // Keep tracking
            return _trackingMode;
        }
    }
}