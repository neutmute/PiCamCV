using System;
using System.Text;
using System.Timers;
using OpenTK.Input;
using PiCamCV.Common;
using PiCamCV.ConsoleApp.Runners.PanTilt.MoveStrategies;

namespace PiCamCV.ConsoleApp.Runners.PanTilt
{
    /// <summary>
    /// mono picamcv.con.exe -m=pantiltjoy -nopwm
    /// </summary>
    public class JoystickPanTiltController : PanTiltController
    {
        private readonly int _joystickIndex;
        private JoystickState _currentState;
        private JoystickCapabilities _capabilities;

        public JoystickPanTiltController(IPanTiltMechanism panTiltMechanism): base(panTiltMechanism)
        {
            _joystickIndex = 0;

            _capabilities = Joystick.GetCapabilities(_joystickIndex);
            if (_capabilities.IsConnected)
            {
                Log.InfoFormat(
                    "Joystick {0} connected. Axes.Count={1}, Buttons.Count={2}"
                    , _joystickIndex
                    , _capabilities.AxisCount
                    , _capabilities.ButtonCount);
            }

            MoveAbsolute(new PanTiltSetting {PanPercent = 50, TiltPercent = 50});
        }


        public string Tick()
        {
            _currentState = Joystick.GetState(_joystickIndex);

            var sb = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                sb.AppendLine(GetAxisText((JoystickAxis)i));
            }
            for (int i = 0; i < 1; i++)
            {
                sb.AppendLine(GetButtonText((JoystickButton)i));
            }

            var panAxis = ReadAxis(JoystickAxis.Axis0);
            var tiltAxis = (ReadAxis(JoystickAxis.Axis1) -0.5m) * 2; // tilt normalisation
            var throttleAxis = ReadAxis(JoystickAxis.Axis3);

            var moveStrategy = new JoystickModifierStrategy(panAxis, tiltAxis, throttleAxis);
            var newPosition = moveStrategy.CalculateNewSetting(CurrentSetting);
            
            MoveAbsolute(newPosition);

            sb.AppendFormat("Pan Tilt = {0}\r\n", newPosition);

            return sb.ToString();
        }

        private string GetAxisText(JoystickAxis axis)
        {
            var axisValue = _currentState.GetAxis(axis);
            return string.Format("{0}={1}", axis, axisValue);
        }

        private string GetButtonText(JoystickButton button)
        {
            var buttonValue = _currentState.GetButton(button);
            return string.Format("{0}={1}", button, buttonValue);
        }

        private Decimal ReadAxis(JoystickAxis axis)
        {
            return (Decimal) _currentState.GetAxis(axis);
        }
    }
}