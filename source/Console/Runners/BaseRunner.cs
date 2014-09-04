using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;

namespace PiCamCV.ConsoleApp.Runners
{
    public abstract class BaseRunner : IRunner
    {
        private readonly static ILog _log = LogManager.GetCurrentClassLogger();
        protected ILog Log { get { return _log; } }

        public abstract void Run();
    }
}
