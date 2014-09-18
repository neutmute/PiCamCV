using System;
using System.Collections.Generic;
using System.Drawing;
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
                var captureProperties = capture.GetCaptureProperties();
                Log.Info(m => m("Capture properties: {0}", captureProperties));

                SafetyCheckRoi(options, captureProperties);
            }

            IRunner runner;
            Log.Info(options);
            switch (options.Mode)
            {
                case Mode.simple:runner = new SimpleCv(); 
                    break;

                case Mode.colourdetect:
                    var colorDetector = new ColorDetectRunner(capture);
                    if (options.HasColourSettings)
                    {
                        colorDetector.Settings = options.ColourSettings;
                    }
                    runner = colorDetector;
                    break;

                case Mode.haar:
                    
                    var relativePath = string.Format(@"haarcascades{0}haarcascade_frontalface_default.xml", Path.DirectorySeparatorChar);
                    var cascadeFilename = Path.Combine(appData.ExeFolder, relativePath);
                    var cascadeContent = File.ReadAllText(cascadeFilename);
                    runner = new CascadeRunner(capture, cascadeContent);
                    break;


                case Mode.servosort:
                    runner = new ServoSorter(capture, options);
                    break;

                default:
                    throw KrakenException.Create("Option mode {0} needs wiring up", options.Mode);
            }

            runner.Run();
        }

        private static void SafetyCheckRoi(ConsoleOptions options, CaptureProperties captureProperties)
        {
            if (captureProperties.FrameWidth != 0 && captureProperties.FrameHeight != 0)
            {
                var roiWidthTooBig = options.ColourSettings.Roi.Width > captureProperties.FrameWidth;
                var roiHeightTooBig = options.ColourSettings.Roi.Height > captureProperties.FrameHeight;
                if (roiWidthTooBig || roiHeightTooBig)
                {
                    Log.Warn("ROI is too big! Ignoring");
                    options.ColourSettings.Roi = Rectangle.Empty;
                }
            }
        }
    }
}
