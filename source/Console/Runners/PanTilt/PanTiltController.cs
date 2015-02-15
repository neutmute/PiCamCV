using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Common.Logging;
using OpenTK.Input;

namespace PiCamCV.ConsoleApp.Runners.PanTilt
{
    public class Screen
    {
        private int _lineNumber;

        public Screen()
        {
            Clear();
        }

        public void Clear()
        {
            Console.Clear();
            
        }

        public void BeginRepaint()
        {
            _lineNumber = 1;
        }


        public void WriteLine(string format, params object[] args)
        {
            if (_lineNumber < Console.BufferHeight)
            {
                Console.SetCursorPosition(0, _lineNumber);
                var message = string.Format(format, args);
                Console.WriteLine(message.PadRight(Console.BufferWidth, ' '));
            }
            _lineNumber++;
        }
    }

    public abstract class PanTiltController
    {
        private readonly static ILog _log = LogManager.GetCurrentClassLogger();
        private int _consoleLineNumber;
        protected Screen Screen {get; private set; }

        protected ILog Log { get { return _log; } }

        protected IPanTiltMechanism PanTiltMechanism {get;set;}

        protected PanTiltController(IPanTiltMechanism panTiltMech)
        {
            Screen = new Screen();
            PanTiltMechanism = panTiltMech;
        }


        //public void Tick()
        //{
            
        //}
    }

    public class CameraBasedPanTiltController //: PanTiltController
    {
        
    }

    /// <summary>
    /// mono picamcv.con.exe -m=pantiltjoy -nopwm
    /// </summary>
    public class JoystickPanTiltController : PanTiltController, IRunner
    {
        private readonly int _joystickIndex;
        private readonly int _sampleRateMilliseconds;

        public JoystickPanTiltController(IPanTiltMechanism panTiltMechanism): base(panTiltMechanism)
        {
            _joystickIndex = 0;
            _sampleRateMilliseconds = 100;
            
            var caps = Joystick.GetCapabilities(_joystickIndex);
            if (caps.IsConnected)
            {
                Log.InfoFormat(
                    "Joystick {0} connected. Axes.Count={1}, Buttons.Count={2}"
                    , _joystickIndex
                    , caps.AxisCount
                    , caps.ButtonCount);
            }
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
            var joystickState = Joystick.GetState(_joystickIndex);
            float x = joystickState.GetAxis(JoystickAxis.Axis0);
            float y = joystickState.GetAxis(JoystickAxis.Axis1);
            var button0 = joystickState.GetButton(JoystickButton.Button0);

            Screen.BeginRepaint();
            Screen.WriteLine("Axis 0: {0}", x);
            Screen.WriteLine("Axis 1: {0}", y);
            Screen.WriteLine("Button 0: {0}", button0);

            var panServo = PanTiltMechanism.PanServo;
            var tiltServo = PanTiltMechanism.TiltServo;
            //panServo.MoveTo(panServo.CurrentPercent * )
        }
    }
}
