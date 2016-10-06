using System.Drawing;
using Kraken.Core;
using PiCamCV.ConsoleApp.Runners.PanTilt;

namespace PiCamCV.Common.PanTilt.Controllers.multimode
{
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
                Screen.WriteLine($"Face deemed lost afer {AbandonDetectionAfterMissing.ToHumanReadable()}");
                return ProcessingMode.Autonomous;
            }

            // Keep tracking
            return ProcessingMode.FaceDetection;
        }
    }
}