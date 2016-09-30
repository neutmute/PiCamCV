using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PiCamCV.ConsoleApp.Runners.PanTilt;

namespace PiCamCV.Common.PanTilt
{
    public class TimeTarget
    {
        public PanTiltSetting Original { get; set; }

        public PanTiltSetting Target { get; set; }

        public TimeSpan TimeSpan { get; set; }

        private Stopwatch _stopWatch;

        public TimeTarget()
        {
            _stopWatch = Stopwatch.StartNew();
        }

        public PanTiltSetting GetNextPosition()
        {
            var nextPan = GetParabolaFunction(Original.PanPercent.Value, Target.PanPercent.Value);
            var nextTilt = GetParabolaFunction(Original.TiltPercent.Value, Target.TiltPercent.Value);
            return new PanTiltSetting {PanPercent = nextPan, TiltPercent = nextTilt};
        }

        /// <summary>
        /// https://www.wolframalpha.com/input/?i=10*(-(+x%2F6-1)%5E2%2B1)
        /// </summary>
        public decimal GetParabolaFunction(decimal originalAxis, decimal targetAxis)
        {
            var axisToTravel =  targetAxis - originalAxis;
            var xDivisorMinusOne = Convert.ToDecimal((_stopWatch.ElapsedMilliseconds / TimeSpan.TotalMilliseconds) - 1);
            var output = axisToTravel*(-xDivisorMinusOne*xDivisorMinusOne) + 1;
            return output;
        }
    }
}
