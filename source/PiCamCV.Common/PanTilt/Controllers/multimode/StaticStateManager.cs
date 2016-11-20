using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PiCamCV.ConsoleApp.Runners.PanTilt;

namespace PiCamCV.Common.PanTilt.Controllers.multimode
{
    public class StaticTrackingPanTiltOutput : TrackingCameraPanTiltProcessOutput
    {
        public override bool IsDetected => false;
    }


    public class StaticStateManager : TimeoutStateManager<StaticTrackingPanTiltOutput>
    {
        protected override string ObjectName => "Static";

        public override TimeSpan AbandonDetectionAfterMissing => TimeSpan.FromSeconds(30);

        public StaticStateManager(IScreen screen) : base(ProcessingMode.Static, screen)
        {
            
        }
    }
}
