using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using Emgu.CV;
using Kraken.Core;
using PiCamCV.ConsoleApp.Runners;

namespace PiCamCV.ConsoleApp
{
    class Program
    {

        protected static ILog Log = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            Log.Info(ExecutionEnvironment.GetApplicationMetadata());
            Log.Info(m => m("CVLibrary={0}", CvInvokeRaspiCamCV.CVLibrary));

            var options = new ConsoleOptions(args);

            if (options.ShowHelp)
            {
                Console.WriteLine("Options:");
                options.OptionSet.WriteOptionDescriptions(Console.Out);
                return;
            }

            IRunner runner;
            
            switch (options.Mode)
            {
                case Mode.simple:runner = new SimpleCv(); 
                    break;

                case Mode.colourdetect:
                    var capture = CaptureFactory.GetCapture(CaptureDevice.Usb);
                    var colorDetector = new ColorDetectRunner(capture);
                    if (options.HasThresholds)
                    {
                        colorDetector.LowThreshold = options.LowThreshold;
                        colorDetector.HighThreshold = options.HighThreshold;
                    }
                    runner=colorDetector;
                    break;

                default:
                    throw KrakenException.Create("Option mode {0} needs wiring up", options.Mode);
            }

            runner.Run();
        }
    }
}
