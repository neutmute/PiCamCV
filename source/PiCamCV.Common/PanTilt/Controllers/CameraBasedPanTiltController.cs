using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Timers;
using Emgu.CV.Structure;
using PiCamCV.Common;
using PiCamCV.Common.ExtensionMethods;
using PiCamCV.Common.Interfaces;
using PiCamCV.Common.PanTilt.MoveStrategies;
using PiCamCV.ConsoleApp.Runners.PanTilt.MoveStrategies;
using PiCamCV.Interfaces;

namespace PiCamCV.ConsoleApp.Runners.PanTilt
{
    public class CameraPanTiltProcessOutput : CameraProcessOutput
    {
        public PanTiltSetting PanTiltPrior { get; set; }

        public PanTiltSetting PanTiltNow { get; set; }

        public Point Target { get; set; }

        public bool IsServoInMotion { get; set; }

        public override string ToString()
        {
            return $"PositionPrior={PanTiltPrior}, Target={Target}, PositionNow={PanTiltNow}";
        }
    }

    public interface IController<out TOutput> where TOutput : CameraPanTiltProcessOutput, new()
    {
        TOutput Process(CameraProcessInput input);

        TimeSpan ServoSettleTime { get; set; }
    }

    public abstract class CameraBasedPanTiltController<TOutput> : 
        PanTiltController
        , IController<TOutput>
        , ICameraProcessor<CameraProcessInput, TOutput>

        where TOutput : CameraPanTiltProcessOutput, new()
    {
        private readonly CameraBasedModifierStrategy _panTiltModifier;

        protected event EventHandler ServoSettleTimeChanged;

        protected CaptureConfig CaptureConfig { get; private set; }

        protected Point CentrePoint { get; private set; }

        protected int Ticks { get;set;}

        private readonly Timer _timerUntilServoSettled;

        protected bool IsServoInMotion { get; set; }

        public TimeSpan ServoSettleTime
        {
            get { return TimeSpan.FromMilliseconds(_timerUntilServoSettled.Interval); }
            set
            {
                _timerUntilServoSettled.Interval = value.TotalMilliseconds;
                ServoSettleTimeChanged?.Invoke(this, new EventArgs());
            }
        }

        protected CameraBasedPanTiltController(IPanTiltMechanism panTiltMech, CaptureConfig captureConfig)
            : base(panTiltMech)
        {
            CaptureConfig = captureConfig;
            CentrePoint = CaptureConfig.Resolution.GetCenter();
            
            _panTiltModifier = new AutoCalibratedModifierStrategy(CaptureConfig, CentrePoint);

            Log.InfoFormat("Centre = {0}", CentrePoint);
            Ticks = 0;

            MoveAbsolute(new PanTiltSetting(50, 50));
            
            _timerUntilServoSettled = new Timer(150);
            _timerUntilServoSettled.AutoReset = false;
            _timerUntilServoSettled.Elapsed += (o, a) =>
            {
                PreServoSettle();
                IsServoInMotion = false;
                PostServoSettle();
            };
        }

        protected virtual void PreServoSettle(){}

        protected virtual void PostServoSettle() { }
        
        protected TOutput ReactToTarget(Point objectOfInterest)
        {
            var output = new TOutput();
            
            _panTiltModifier.Objective = objectOfInterest;
            var newPosition = _panTiltModifier.CalculateNewSetting(CurrentSetting);

            output.Target = objectOfInterest;
            output.PanTiltPrior = CurrentSetting.Clone();
            output.PanTiltNow = newPosition.Clone();

            if (!objectOfInterest.Equals(CentrePoint))
            {
                var moved = MoveAbsolute(newPosition);
                if (moved)
                {
                    _timerUntilServoSettled.Start();
                    IsServoInMotion = true;
                }
            }
            
            return output;
        }
        
        public TOutput Process(CameraProcessInput input)
        {
            if (IsServoInMotion)
            {
                var output = new TOutput();
                output.IsServoInMotion = true;
                return output;
            }
            var stopWatch = Stopwatch.StartNew();
            var result = DoProcess(input);

            if (result.CapturedImage == null && input.SetCapturedImage)
            {
                result.CapturedImage = input.Captured.ToImage<Bgr, byte>();
            }

            result.Elapsed = stopWatch.Elapsed;
            return result;
        }
        protected abstract TOutput DoProcess(CameraProcessInput input);
    }
}