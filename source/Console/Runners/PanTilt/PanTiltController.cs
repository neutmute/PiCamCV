using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Common.Logging;
using OpenTK.Input;
using RPi.Pwm.Motors;

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

        private IPanTiltMechanism PanTiltMechanism {get;set;}

        protected IServoMotor PanServo { get { return PanTiltMechanism.PanServo; } }
        protected IServoMotor TiltServo { get { return PanTiltMechanism.TiltServo; } }

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
        private JoystickState _currentState;
        private JoystickCapabilities _capabilities;

        public JoystickPanTiltController(IPanTiltMechanism panTiltMechanism): base(panTiltMechanism)
        {
            _joystickIndex = 0;
            _sampleRateMilliseconds = 100;

            _capabilities = Joystick.GetCapabilities(_joystickIndex);
            if (_capabilities.IsConnected)
            {
                Log.InfoFormat(
                    "Joystick {0} connected. Axes.Count={1}, Buttons.Count={2}"
                    , _joystickIndex
                    , _capabilities.AxisCount
                    , _capabilities.ButtonCount);
            }

            PanServo.MoveTo(50);
            TiltServo.MoveTo(50);
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
            _currentState = Joystick.GetState(_joystickIndex);

            Screen.BeginRepaint();

            for (int i = 0; i < _capabilities.AxisCount; i++)
            {
                ScreenWriteAxis((JoystickAxis)i);
            }
            for (int i = 0; i < _capabilities.ButtonCount; i++)
            {
                ScreenWriteButton((JoystickButton)i);
            }

            var panAxis = ReadAxis(JoystickAxis.Axis0);
            var tiltAxis = (ReadAxis(JoystickAxis.Axis1) -0.5m) * 2; // tilt normalisation
            var throttleAxis = ReadAxis(JoystickAxis.Axis2);

            var throttleMultipler = (5*(-throttleAxis+ 1.1m)); // 1 to bias to +ve, .1 to ensure always non zero

            if (Math.Abs(panAxis) > 0.6m)
            {
                var newPanServoPercent = (PanServo.CurrentPercent + (panAxis*throttleMultipler));
                PanServo.MoveTo(newPanServoPercent);
            }
            
            if (Math.Abs(tiltAxis) > 0.6m)
            {
                var newTiltServoPercent = (TiltServo.CurrentPercent + (tiltAxis * throttleMultipler));
                TiltServo.MoveTo(newTiltServoPercent);
            }

            Screen.WriteLine("Throttle Multiplier = {0:F}", throttleMultipler);
            Screen.WriteLine("Pan = {0:F}%, {1}pwm", PanServo.CurrentPercent, PanServo.CurrentPwm);
            Screen.WriteLine("Tilt = {0:F}%, {1}pwm", TiltServo.CurrentPercent, TiltServo.CurrentPwm);
        }

        private Decimal ReadAxis(JoystickAxis axis)
        {
            return (Decimal) _currentState.GetAxis(axis);
        }

        private void ScreenWriteAxis(JoystickAxis axis)
        {
            var axisValue = _currentState.GetAxis(axis);
            Screen.WriteLine("{0}={1}", axis, axisValue);
        }

        private void ScreenWriteButton(JoystickButton button)
        {
            var buttonValue = _currentState.GetButton(button);
            Screen.WriteLine("{0}={1}", button, buttonValue);
        }
    }
}
