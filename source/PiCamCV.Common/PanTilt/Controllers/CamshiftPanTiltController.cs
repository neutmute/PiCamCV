using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PiCamCV.Common.ExtensionMethods;
using PiCamCV.Common.Interfaces;
using PiCamCV.ConsoleApp.Runners.PanTilt;

namespace PiCamCV.Common.PanTilt.Controllers
{
    public class CamshiftPanTiltController : CameraBasedPanTiltController<CameraPanTiltProcessOutput>
    {
        private readonly CamshiftDetector _detector;


        public TrackingConfig TrackConfig { get; set; }

        public CamshiftPanTiltController(IPanTiltMechanism panTiltMech, CaptureConfig captureConfig) : base(panTiltMech, captureConfig)
        {
            _detector = new CamshiftDetector();
        }

        protected override CameraPanTiltProcessOutput DoProcess(CameraProcessInput input)
        {
            var trackingInput = new TrackingInput();
            trackingInput.Config = TrackConfig;
            trackingInput.Captured = input.Captured;
            trackingInput.SetCapturedImage = false;

            var camshiftOutput = _detector.Process(trackingInput);

            TrackConfig.StartNewTrack = false; // turn it off after inititalising

            CameraPanTiltProcessOutput output;

            if (camshiftOutput.HasObjectOfInterest)
            {
                var targetPoint = camshiftOutput.ObjectOfInterest.Center.ToPoint();
                output = ReactToTarget(targetPoint);
            }
            else
            {
                output = ReactToTarget(CentrePoint);
            }

            return output;
        }


        protected override void DisposeObject()
        {
            _detector.Dispose();
        }
    }
}
