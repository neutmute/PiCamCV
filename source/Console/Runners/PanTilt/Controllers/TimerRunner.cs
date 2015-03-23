using System;
using System.Timers;
using Common.Logging;
using OpenTK.Input;
using PiCamCV.Common;
using PiCamCV.ConsoleApp.Runners.PanTilt.MoveStrategies;

namespace PiCamCV.ConsoleApp.Runners.PanTilt
{
    /// <summary>
    /// mono picamcv.con.exe -m=pantiltjoy -nopwm
    /// </summary>
    public class TimerRunner : IRunner
    {
        private readonly static ILog _log = LogManager.GetCurrentClassLogger();
        protected ILog Log { get { return _log; } }

        private readonly int _sampleRateMilliseconds;
        private readonly JoystickPanTiltController _controller;
        private IScreen _screen;

        public TimerRunner(JoystickPanTiltController controller, IScreen screen)
        {
            _controller = controller;
            _sampleRateMilliseconds = 100;
            _screen = screen;
        }

        public void Run()
        {
            Log.InfoFormat("Starting timer with a sample rate of {0} ms", _sampleRateMilliseconds);
            var timer = new Timer();
            timer.AutoReset = true;
            timer.Interval = _sampleRateMilliseconds;
            timer.Elapsed += (s, e) => Tick();
            timer.Start();

            var keyHandler = new KeyHandler();
            keyHandler.WaitForExit();
        }

        public void Tick()
        {
            _screen.BeginRepaint();
            _screen.WriteLine(_controller.Tick());
        }

    }
}