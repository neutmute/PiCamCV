using Common.Logging;
using Raspberry.IO.Components.Controllers.Pca9685;

namespace PiCamCV.ConsoleApp.Runners
{
    public class PwmControlBase
    {
        private readonly static ILog _Log = LogManager.GetCurrentClassLogger();
        //private readonly Dictionary<PwmChannel, PwmComponentBase> _components;

        protected ILog Log { get { return _Log; } }

        protected IPwmDevice PwmDevice { get; private set; }

        protected PwmControlBase(IPwmDevice pwmDevice)
        {
            PwmDevice = pwmDevice;
            // _components = new Dictionary<PwmChannel, PwmComponentBase>();
        }
    }
}