using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Common.Logging;
using Emgu.CV;
using Kraken.Core;
using PiCamCV.Common;
using PiCamCV.Common.PanTilt.Controllers;
using PiCamCV.ConsoleApp.Runners;
using PiCamCV.ConsoleApp.Runners.PanTilt;
using PiCamCV.Interfaces;
using RPi.Pwm;
using Web.Client;

namespace PiCamCV.ConsoleApp
{
    /// <summary>
    /// WINDOWS
    /// picamcv.con.exe -m=pantiltmultimode
    /// 
    /// LINUX
    /// 
    /// mono picamcv.con.exe -m=simple
    /// 
    /// 
    /// </summary>
    class Program
    {
        protected static ILog Log = LogManager.GetLogger("Console");

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
            
            CapturePi.DoMatMagic("CreateCapture");

            var noCaptureGrabs = new[] { Mode.simple, Mode.pantiltjoy };
            var i2cRequired = new[] { Mode.pantiltface, Mode.pantiltjoy ,Mode.pantiltcolour, Mode.pantiltmultimode };
            CaptureConfig captureConfig = null;
            if (!noCaptureGrabs.Contains(options.Mode))
            {
                var request = new CaptureRequest { Device = CaptureDevice.Usb };
                if (EnvironmentService.IsUnix)
                {
                    request.Device = CaptureDevice.Pi;
                }

                request.Config = new CaptureConfig { Resolution = new Resolution(160, 120), Framerate = 25, Monochrome = false };

                capture = CaptureFactory.GetCapture(request);
                captureConfig = capture.GetCaptureProperties();
                Log.Info($"Capture properties read: {captureConfig}");

                SafetyCheckRoi(options, captureConfig);
            }

            IPanTiltMechanism panTiltMech = null;
            IScreen screen = null;
            if (i2cRequired.Contains(options.Mode))
            {
                var pwmDeviceFactory = new Pca9685DeviceFactory();
                var pwmDevice = pwmDeviceFactory.GetDevice(options.UseFakeDevice);
                panTiltMech = new PanTiltMechanism(pwmDevice);
                screen = new ConsoleScreen();
                screen.Clear();
            }
            else
            {
                Log.Info("Pan Tilt is not required");
            }

            IRunner runner;
            Log.Info(options);
            switch (options.Mode)
            {
                case Mode.noop: runner = new NoopRunner(capture);
                    break;

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
                    var relativePath = $@"haarcascades{Path.DirectorySeparatorChar}haarcascade_frontalface_default.xml";
                    var cascadeFilename = Path.Combine(appData.ExeFolder, relativePath);
                    var cascadeContent = File.ReadAllText(cascadeFilename);
                    runner = new CascadeRunner(capture, cascadeContent);
                    break;

                case Mode.servosort:
                    runner = new ServoSorter(capture, options);
                    break;

                case Mode.pantiltjoy:
                    var joyController = new JoystickPanTiltController(panTiltMech);
                    runner = new TimerRunner(joyController, screen);
                    break;

                case Mode.pantiltface:
                    var controllerF = new FaceTrackingPanTiltController(panTiltMech, captureConfig);
                    runner = new CameraBasedPanTiltRunner(panTiltMech, capture, controllerF, screen);
                    break;

                case Mode.pantiltmultimode:
                    var cameraHubProxy = new CameraHubProxy();
                    cameraHubProxy.Connect();
                    var remoteScreen = new RemoteConsoleScreen(cameraHubProxy);
                    var piServerClient = new BsonPostImageTransmitter();
                    var imageTransmitter = new RemoteImageSender(piServerClient, cameraHubProxy);

                    cameraHubProxy.SettingsChanged += (sender, s) =>
                    {
                        remoteScreen.Enabled = s.EnableConsoleTransmit;
                        imageTransmitter.Enabled = s.EnableImageTransmit;
                    };

                    var controllerMultimode = new MultimodePanTiltController(panTiltMech, captureConfig, remoteScreen, cameraHubProxy, imageTransmitter);
                    runner = new CameraBasedPanTiltRunner(panTiltMech, capture, controllerMultimode, screen);
                    break;

                case Mode.pantiltcolour:
                    var controllerC = new ColourTrackingPanTiltController(panTiltMech, captureConfig);
                    if (options.HasColourSettings)
                    {
                        controllerC.Settings = options.ColourSettings;
                    }
                    else
                    {
                        throw KrakenException.Create("Colour settings not found");
                    }
                    runner = new CameraBasedPanTiltRunner(panTiltMech, capture, controllerC, screen);
                    break;

                default:
                    throw KrakenException.Create("Option mode {0} needs wiring up", options.Mode);
            }

            runner.Run();
        }

        private static void SafetyCheckRoi(ConsoleOptions options, CaptureConfig captureProperties)
        {
            if (captureProperties.Resolution.IsValid && options.ColourSettings != null)
            {
                var roiWidthTooBig = options.ColourSettings.Roi.Width >     captureProperties.Resolution.Width;
                var roiHeightTooBig = options.ColourSettings.Roi.Height >   captureProperties.Resolution.Height;
                if (roiWidthTooBig || roiHeightTooBig)
                {
                    Log.Warn("ROI is too big! Ignoring");
                    options.ColourSettings.Roi = Rectangle.Empty;
                }
            }
        }
    }
}
