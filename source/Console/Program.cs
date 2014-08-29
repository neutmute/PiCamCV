using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using Emgu.CV;
using Kraken.Core;

namespace PiCamCV.Console
{
    class Program
    {

        protected static ILog Log = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            Log.Info(ExecutionEnvironment.GetApplicationMetadata());
            Log.Info(m => m("OpencvRaspiCamCVLibrary={0}", CvInvokeRaspiCamCV.OpencvRaspiCamCVLibrary));
            var runner = new SimpleCv();
            runner.Run();
        }
    }
}
