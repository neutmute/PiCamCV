using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PiCamCV.Common.Interfaces;

namespace PiCamCV.ConsoleApp.Runners
{
    public class IpReporter : BaseRunner
    {
        private readonly ICameraToServerBus _cameraToServerBus;

        public IpReporter(ICameraToServerBus cameraToServerBus)
        {
            _cameraToServerBus = cameraToServerBus;
        }

        public override void Run()
        {
            // this will inform the server of our IP
            _cameraToServerBus.Connect();
        }
    }
}
