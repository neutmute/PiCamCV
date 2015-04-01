using System;
using System.Drawing;
using PiCamCV.Common.PanTilt.Controllers;

namespace PiCamCV.ConsoleApp.Runners.PanTilt.MoveStrategies
{
    public abstract class CameraBasedModifierStrategy : IPanTiltModifierStrategy
    {
        protected struct PointD
        {
            public decimal X { get; set; }
            public decimal Y { get; set; }

            public PointD(decimal x, decimal y) : this()
            {
                X = x;
                Y = y;
            }
        }

        protected PointD Scale { get; set; }

        protected const int DeadZone = 10;

        protected Point Target;

        protected CaptureConfig CaptureConfig {get;set;}

        public Point Objective { get; set; }

        protected CameraBasedModifierStrategy(CaptureConfig captureConfig, Point target)
        {
            Target = target;
            CaptureConfig = captureConfig;
            Scale = new PointD(decimal.One, decimal.One);
        }

        public PanTiltSetting CalculateNewSetting(PanTiltSetting currentSetting)
        {
            var newSetting = currentSetting.Clone();

            var deviation = GetDeviationFromTarget();

            var deflection = CalculatePercentDeflection(deviation);

            var xDeflection = Math.Abs(deviation.X) > DeadZone ? deflection.X : 0;
            var yDeflection = Math.Abs(deviation.Y) > DeadZone ? deflection.Y : 0;

            newSetting.PanPercent += xDeflection;
            newSetting.TiltPercent += yDeflection;

            return newSetting;
        }

        protected abstract PointD CalculatePercentDeflection(PointD pixelDeviation);

        protected PointD GetDeviationFromTarget()
        {
            var xDiff = (Target.X - Objective.X) * Scale.X;
            var yDiff = (Target.Y - Objective.Y) * Scale.Y;
            return new PointD(xDiff, yDiff);
        }
    }
}