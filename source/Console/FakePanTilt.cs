using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raspberry.IO.Components.Controllers.Pca9685;
using UnitsNet;

namespace PiCamCV.ConsoleApp
{
    /// <summary>
    /// TypeLoadException workaround
    /// </summary>
    public class FakePanTilt : IPwmDevice
    {
        public void SetPwmUpdateRate(int frequency) { }

        public void SetPwmUpdateRate(Frequency frequency)
        {
        }

        public void SetPwm(PwmChannel channel, int @on, int off)
        {
        }

        public void SetFull(PwmChannel channel, bool fullOn)
        {
        }
    }
}
