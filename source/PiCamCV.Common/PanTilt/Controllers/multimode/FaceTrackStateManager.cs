using PiCamCV.ConsoleApp.Runners.PanTilt;

namespace PiCamCV.Common.PanTilt.Controllers.multimode
{
    public class FaceTrackStateManager : TimeoutStateManager<FaceTrackingPanTiltOutput>
    {
        public FaceTrackStateManager(IScreen screen) :base(ProcessingMode.FaceDetection, screen)
        {
                
        }
    }
}