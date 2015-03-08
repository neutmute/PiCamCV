using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiCamCV.ConsoleApp.Runners.PanTilt.MoveStrategies
{
    public class JoystickModifierStrategy : IPanTiltModifierStrategy
    {
        private readonly decimal _panAxis;
        private readonly decimal _tiltAxis;
        private readonly decimal _throttleAxis;

        public decimal ThrottleMultipler { get; private set; }

        public JoystickModifierStrategy(decimal panAxis, decimal tiltAxis, decimal throttleAxis)
        {
            _panAxis = panAxis;
            _tiltAxis = tiltAxis;
            _throttleAxis = throttleAxis;
        }

        public PanTiltSetting CalculateNewSetting(PanTiltSetting currentSetting)
        {
            var newSetting = new PanTiltSetting();
            ThrottleMultipler = (4 * (-_throttleAxis + 1.1m)); // 1 to bias to +ve, .1 to ensure always non zero

            const decimal deadZone = 0.6m;

            if (Math.Abs(_panAxis) > deadZone)
            {
                newSetting.PanPercent = (currentSetting.PanPercent + (_panAxis * ThrottleMultipler));  
            }

            if (Math.Abs(_tiltAxis) > deadZone)
            {
                newSetting.TiltPercent = (currentSetting.TiltPercent + (_tiltAxis * ThrottleMultipler));
            }

            return newSetting;
        }
    }
}