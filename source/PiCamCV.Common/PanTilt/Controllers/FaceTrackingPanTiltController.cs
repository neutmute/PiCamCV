using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using PiCamCV.Common;
using PiCamCV.Common.Interfaces;
using PiCamCV.ConsoleApp.Runners.PanTilt.MoveStrategies;
using PiCamCV.ExtensionMethods;
using PiCamCV.Interfaces;

namespace PiCamCV.ConsoleApp.Runners.PanTilt
{
    /// <summary>
    /// sudo mono picamcv.con.exe -m=pantiltface
    /// </summary>
    public class FaceTrackingPanTiltController : CameraBasedPanTiltController
    {
        private readonly FaceDetector _faceDetector;
        public FaceTrackingPanTiltController(IPanTiltMechanism panTiltMech, CaptureConfig captureConfig, IScreen screen)
            : base(panTiltMech, captureConfig, screen)
        {
            var environmentService = new EnvironmentService();
            var haarEyeFile = new FileInfo(environmentService.GetAbsolutePathFromAssemblyRelative("haarcascades/haarcascade_eye.xml"));
            var haarFaceFile = new FileInfo(environmentService.GetAbsolutePathFromAssemblyRelative("haarcascades/haarcascade_frontalface_default.xml"));

            _faceDetector = new FaceDetector(haarFaceFile.FullName, haarEyeFile.FullName);
        }

        protected override CameraPanTiltProcessOutput DoProcess(CameraProcessInput baseInput)
        {
            var input = new FaceDetectorInput();
            input.Captured = baseInput.Captured;
            input.DetectEyes = false;

            var result = _faceDetector.Process(input);

            var targetPoint = CentrePoint;

            if (result.Faces.Count > 0)
            {
                Face faceTarget = result.Faces[0];
                targetPoint = faceTarget.Region.Center();
            }

            Screen.BeginRepaint();
            var outerResult = ReactToTarget(targetPoint);

            if (input.SetCapturedImage)
            {
                outerResult.CapturedImage = input.Captured.ToImage<Bgr, byte>();
            }

            Screen.WriteLine("Detected Face Count={0}", result.Faces.Count);
            
            return outerResult;
        }
    }
}
