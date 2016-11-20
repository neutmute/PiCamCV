using PiCamCV.ConsoleApp.Runners.PanTilt;

namespace PiCamCV.Common.PanTilt.Controllers.multimode
{
    public class FaceTrackStateManager : TimeoutStateManager<FaceTrackingPanTiltOutput>
    {
        protected override string ObjectName => "Face";

        public FaceTrackStateManager(IScreen screen) :base(ProcessingMode.FaceDetection, screen)
        {
                
        }
    }
}