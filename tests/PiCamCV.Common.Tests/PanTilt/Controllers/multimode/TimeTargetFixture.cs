using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kraken.Core;
using NUnit.Framework;
using PiCamCV.Common.PanTilt;
using PiCamCV.Common.PanTilt.Controllers;
using PiCamCV.ConsoleApp.Runners.PanTilt;

namespace PiCamCV.Common.Tests.PanTilt.Controllers.multimode
{
    [TestFixture]
    public class TimeTargetFixture : Fixture
    {
        private MockStopwatch _mockStopwatch;

        public TimeTargetFixture()
        {
            _mockStopwatch = new MockStopwatch();
        }

        public TimeTarget GetSystemUnderTest()
        {
            var timeTarget = new TimeTarget(_mockStopwatch);

            timeTarget.Original = new PanTiltSetting (50, 50);
            timeTarget.Target = new PanTiltSetting(80, 80);

            timeTarget.TimeSpan = TimeSpan.FromSeconds(4);
            return timeTarget;
        }

        [Test]
        public void SmoothPursuitPositiveQuadrant()
        {
            var results = new List<PanTiltTime>();
            var sut = GetSystemUnderTest();
            var resolution = 100;

            var tickEveryMs = Convert.ToInt32(sut.TimeSpan.TotalMilliseconds/resolution);
            var time = TimeSpan.FromSeconds(0);
            var tick = TimeSpan.FromMilliseconds(tickEveryMs);
            
            for (var timeMs = 0; timeMs <= sut.TimeSpan.TotalMilliseconds; timeMs += tickEveryMs)
            {
                var result = new PanTiltTime();
                time += tick;
                _mockStopwatch.Set(time);
                result.TimeSpan = time;
                result.Setting = sut.GetNextPosition();

                results.Add(result);                
            }

            var panPoints = results.ConvertAll(p => p.ToCsv(PanTiltAxis.Horizontal));
            var tiltPoints = results.ConvertAll(p => p.ToCsv(PanTiltAxis.Vertical));

            var csvPan = string.Join("\r\n", panPoints);
            var csvTilt = string.Join("\r\n", tiltPoints);

            Console.WriteLine($"Pan:\r\n{csvPan}");

            Console.WriteLine($"Tilt:\r\n{csvTilt}");
        }
    }
}
