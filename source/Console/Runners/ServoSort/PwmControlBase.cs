using Common.Logging;
using Raspberry.IO.Components.Controllers.Pca9685;

namespace PiCamCV.ConsoleApp.Runners
{
    public class PwmControlBase
    {
        private readonly static ILog _Log = LogManager.GetLogger< PwmControlBase>();
        

        protected ILog Log { get { return _Log; } }

        protected IPwmDevice PwmDevice { get; private set; }

        protected PwmControlBase(IPwmDevice pwmDevice)
        {
            PwmDevice = pwmDevice;
        }
    }
}