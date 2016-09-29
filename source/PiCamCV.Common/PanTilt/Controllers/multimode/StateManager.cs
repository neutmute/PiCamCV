using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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

    public class FaceTrackStateManager : StateManager
    {
        public Point LastDetection { get; set; }

        public FaceTrackStateManager(IScreen screen) :base(screen)
        {
                
        }

        public ProcessingMode AcceptScan(FaceTrackingPanTiltOutput output)
        {
            if (output.IsDetected)
            {
                LastDetection = output.Target;
                RegisterDetection();
                
                if (LastDetection == Point.Empty)
                {
                    Screen.WriteLine("Face detected");
                }
            }
            
            if (LastDetection != Point.Empty && TimeSinceLastDetection > AbandonDetectionAfterMissing)
            {
                // Give up on face track if we saw a face but lost it
                return ProcessingMode.Autonomous;
            }

            // Keep tracking
            return ProcessingMode.FaceDetection;
        }
    }

    //public class ColourTrackStateManager : StateManager
    //{

    //}
}
