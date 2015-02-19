using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using PiCamCV.Common;
using PiCamCV.ConsoleApp.Runners.PanTilt.MoveStrategies;
using PiCamCV.Interfaces;

namespace PiCamCV.ConsoleApp.Runners.PanTilt
{
    public class FaceTrackingPanTiltController : CameraBasedPanTiltController
    {
        private readonly FaceDetector _faceDetector;
        public FaceTrackingPanTiltController(IPanTiltMechanism panTiltMech, ICaptureGrab captureGrabber) : base(panTiltMech, captureGrabber)
        {
            var environmentService = new EnvironmentService();
            var haarEyeFile = new FileInfo(environmentService.GetAbsolutePathFromAssemblyRelative("haarcascades/haarcascade_eye.xml"));
            var haarFaceFile = new FileInfo(environmentService.GetAbsolutePathFromAssemblyRelative("haarcascades/haarcascade_frontalface_default.xml"));

            _faceDetector = new FaceDetector(haarFaceFile.FullName, haarEyeFile.FullName);

        }

        public override void ImageGrabbedHandler(object sender, EventArgs e)
        {
            using (var capturedFrame = new Mat())
            {
                CameraCapture.Retrieve(capturedFrame);
                var input = new FaceDetectorInput();
                input.Captured = capturedFrame;
                input.DetectEyes =false;

                var result = _faceDetector.Process(input);
               // result.Faces[0].Region.

                if (result.Faces.Count > 0)
                {
                    //var 
                    var moveStrategy = new SimpleCameraModifierStrategy();
                }

                var imageBgr = result.CapturedImage;
            }
        }
    }
}
