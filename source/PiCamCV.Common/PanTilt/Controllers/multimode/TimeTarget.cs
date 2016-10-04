using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PiCamCV.Common.Interfaces;
using PiCamCV.ConsoleApp.Runners.PanTilt;

namespace PiCamCV.Common.PanTilt
{
    public class TimeTarget
    {
        public PanTiltSetting Original { get; set; }

        public PanTiltSetting Target { get; set; }

        /// <summary>
        /// How long to take to reach the target
        /// </summary>
        public TimeSpan TimeSpan { get; set; }

        public bool TimeTargetReached => _stopWatch.Elapsed >= TimeSpan;

        private readonly IStopwatch _stopWatch;

        public TimeTarget(IStopwatch stopwatch)
        {
            _stopWatch = stopwatch;
        }

        public TimeTarget()
        {
            _stopWatch = StopwatchWrapper.StartNew();
        }

        public PanTiltSetting GetNextPosition()
        {
            var nextPan = Original.PanPercent.Value + GetParabolaFunction(Original.PanPercent.Value, Target.PanPercent.Value);
            var nextTilt = Original.TiltPercent.Value + GetParabolaFunction(Original.TiltPercent.Value, Target.TiltPercent.Value);
            return new PanTiltSetting {PanPercent = nextPan, TiltPercent = nextTilt};
        }

        /// <summary>
        /// https://www.wolframalpha.com/input/?i=10*(-(+x%2F6-1)%5E2%2B1)
        /// </summary>
        public decimal GetParabolaFunction(decimal originalAxis, decimal targetAxis)
        {
            var axisToTravel =  targetAxis - originalAxis;
            if (TimeTargetReached)
            {
                return axisToTravel;
            }
            else
            {
                var xDivisorMinusOne = Convert.ToDecimal((_stopWatch.ElapsedMilliseconds/TimeSpan.TotalMilliseconds) - 1);
                var output = axisToTravel*((-xDivisorMinusOne*xDivisorMinusOne) + 1);
                return output;
            }
        }
    }
}
