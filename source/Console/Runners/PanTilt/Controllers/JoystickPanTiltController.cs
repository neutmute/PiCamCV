using System;
using System.Timers;
using OpenTK.Input;

namespace PiCamCV.ConsoleApp.Runners.PanTilt
{
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

            for (int i = 0; i < 4; i++)
            {
                ScreenWriteAxis((JoystickAxis)i);
            }
            for (int i = 0; i < 1; i++)
            {
                ScreenWriteButton((JoystickButton)i);
            }

            var panAxis = ReadAxis(JoystickAxis.Axis0);
            var tiltAxis = (ReadAxis(JoystickAxis.Axis1) -0.5m) * 2; // tilt normalisation
            var throttleAxis = ReadAxis(JoystickAxis.Axis3);

            var throttleMultipler = (4*(-throttleAxis+ 1.1m)); // 1 to bias to +ve, .1 to ensure always non zero

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
            ScreenWritePanTiltSettings();
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