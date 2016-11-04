using PiCamCV.ConsoleApp.Runners.PanTilt;

namespace PiCamCV.Common.PanTilt.Controllers.multimode
{
    public class ColourTrackStateManager : TimeoutStateManager<ColourTrackingPanTiltOutput>
    {
        public ColourTrackStateManager(IScreen screen) : base(ProcessingMode.ColourObjectTrack, screen)
        {

        }
    }
}