using System;
using System.Diagnostics;
using System.Drawing;
using Emgu.CV.Structure;
using PiCamCV.Common;
using PiCamCV.Common.ExtensionMethods;
using PiCamCV.Common.Interfaces;
using PiCamCV.ConsoleApp.Runners.PanTilt.MoveStrategies;
using PiCamCV.Interfaces;

namespace PiCamCV.ConsoleApp.Runners.PanTilt
{
    public interface IController
    {
        
    }

    public abstract class CameraBasedPanTiltController : 
        PanTiltController
        , IController
        , ICameraProcessor<CameraProcessInput, CameraProcessOutput>
    {
        protected CaptureConfig CaptureConfig { get; private set; }

        protected Point CentrePoint { get; private set; }

        protected int Ticks { get;set;}

        protected CameraBasedPanTiltController(IPanTiltMechanism panTiltMech, CaptureConfig captureConfig, IScreen screen)
            : base(panTiltMech, screen)
        {
            CaptureConfig = captureConfig;
            CentrePoint = CaptureConfig.GetCenter();

            Log.InfoFormat("Centre = {0}", CentrePoint);
            Ticks = 0;

            MoveTo(new PanTiltSetting(50, 50));
        }
        

        protected void ReactToTarget(Point targetPoint)
        {
            var moveStrategy = new CameraModifierStrategy(CaptureConfig, Screen, targetPoint, CentrePoint);
            var newPosition = moveStrategy.CalculateNewSetting(CurrentSetting);

            MoveTo(newPosition);
            
            Screen.WriteLine("Capture Config {0}", CaptureConfig);
            Screen.WriteLine("Target {0}", targetPoint);
            ScreenWritePanTiltSettings();
        }


        public CameraProcessOutput Process(CameraProcessInput input)
        {
            var stopWatch = Stopwatch.StartNew();
            var result = DoProcess(input);

            if (result.CapturedImage == null && input.SetCapturedImage)
            {
                result.CapturedImage = input.Captured.ToImage<Bgr, byte>();
            }

            result.Elapsed = stopWatch.Elapsed;
            return result;
        }
        protected abstract CameraProcessOutput DoProcess(CameraProcessInput input);
    }
}