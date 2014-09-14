using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Common.Logging;
using Emgu.CV;
using Kraken.Core;
using PiCamCV.ConsoleApp.Runners;
using PiCamCV.Interfaces;

namespace PiCamCV.ConsoleApp
{
    class Program
    {

        protected static ILog Log = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            var appData = ExecutionEnvironment.GetApplicationMetadata();
            Log.Info(appData);
            Log.Info(m => m("CVLibrary={0}", CvInvokeRaspiCamCV.CVLibrary));

            var options = new ConsoleOptions(args);

            if (options.ShowHelp)
            {
                Console.WriteLine("Options:");
                options.OptionSet.WriteOptionDescriptions(Console.Out);
                return;
            }

            ICaptureGrab capture = null;
            if (options.Mode != Mode.simple)
            {
                var captureDevice = CaptureDevice.Usb;
                if (Environment.OSVersion.Platform == PlatformID.Unix)
                {
                    captureDevice = CaptureDevice.Pi;
                }
                capture = CaptureFactory.GetCapture(captureDevice);
            }

            IRunner runner;
            Log.Info(options);
            switch (options.Mode)
            {
                case Mode.simple:runner = new SimpleCv(); 
                    break;

                case Mode.colourdetect:
                    var colorDetector = new ColorDetectRunner(capture);
                    if (options.HasThresholds)
                    {
                        colorDetector.LowThreshold = options.LowThreshold;
                        colorDetector.HighThreshold = options.HighThreshold;
                    }
                    runner=colorDetector;
                    break;

                case Mode.haar:
                    
                    var relativePath = string.Format(@"haarcascades{0}haarcascade_frontalface_default.xml", Path.DirectorySeparatorChar);
                    var cascadeFilename = Path.Combine(appData.ExeFolder, relativePath);
                    var cascadeContent = File.ReadAllText(cascadeFilename);
                    runner = new CascadeRunner(capture, cascadeContent);
                    break;


                case Mode.servosort:

                    runner = new ServoSorter(capture);
                    break;

                default:
                    throw KrakenException.Create("Option mode {0} needs wiring up", options.Mode);
            }

            runner.Run();
        }
    }
}
