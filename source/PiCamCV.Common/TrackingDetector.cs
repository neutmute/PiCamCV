using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV.Tracking;
using PiCamCV.Common.Interfaces;
using PiCamCV.Common.PanTilt.Controllers;

namespace PiCamCV.Common
{
    public class TrackingOutput : CameraProcessOutput
    {
        public Rectangle ObjectOfInterest { get; set; }
    }

    public class TrackingInput : CameraProcessInput
    {
        public bool StartNewTrack { get; set; }

        public Rectangle ObjectOfInterest { get; set; }
    }

    public class TrackingDetector : CameraProcessor<TrackingInput, TrackingOutput>
    {
        private Tracker _tracker;

        public string TrackerType { get; set; }


        /// <param name="trackerType">Tracker type, The following detector types are supported: "MIL" – TrackerMIL; "BOOSTING" – TrackerBoosting</param>
        public TrackingDetector(string trackerType= "MIL")
        {
            TrackerType = trackerType;
        }

        private void Reset()
        {
            if (_tracker != null)
            {
                _tracker.Dispose();
            }
            _tracker = new Tracker(TrackerType);
        }
        

        protected override TrackingOutput DoProcess(TrackingInput input)
        {
            Rectangle boundingBox = Rectangle.Empty;

            if (input.StartNewTrack)
            {
                Reset();
                _tracker.Init(input.Captured, input.ObjectOfInterest);
                Log.InfoFormat("Starting tracking");
            }
            else if (_tracker != null)
            {
                _tracker.Update(input.Captured, out boundingBox);
            }

            var output = new TrackingOutput();
            output.ObjectOfInterest = boundingBox;
            return output;
        }

        protected override void DisposeObject()
        {
            base.DisposeObject();

            _tracker?.Dispose();
        }
    }
}
