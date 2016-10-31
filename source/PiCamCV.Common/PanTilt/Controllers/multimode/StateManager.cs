using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PiCamCV.ConsoleApp.Runners.PanTilt;

namespace PiCamCV.Common.PanTilt.Controllers.multimode
{
    public abstract class StateManager
    {

        protected IScreen Screen { get; }

        protected StateManager(IScreen screen)
        {
            Screen = screen;
        }

    }
}
