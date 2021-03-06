using PiCamCV.ConsoleApp.Runners.PanTilt;

namespace PiCamCV.Common.PanTilt.Controllers.multimode
{
    public class ColourTrackStateManager : TimeoutStateManager<ColourTrackingPanTiltOutput>
    {
        protected override string ObjectName => "Colour";

        public ColourTrackStateManager(IScreen screen) : base(ProcessingMode.ColourObjectTrack, screen)
        {

        }
    }
}