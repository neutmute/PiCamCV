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
    /// sudo -s
    /// source [mono env]
    /// source picamcv- #server vars
    /// mono picamcv.con.exe -m=simple
    /// </summary>
    class Program
    {
        protected static ILog Log = LogManager.GetLogger("Console");
        private static ConsoleOptions _consoleOptions;

        static void Main(string[] args)
        {
            var appData = ExecutionEnvironment.GetApplicationMetadata();
            Log.Info(appData);

            _consoleOptions = new ConsoleOptions(args);

            if (_consoleOptions.ShowHelp)
            {
                Console.WriteLine("Options:");
                _consoleOptions.OptionSet.WriteOptionDescriptions(Console.Out);
                return;
            }
            
            CapturePi.DoMatMagic("CreateCapture");

            var noCaptureGrabs = new[] { Mode.simple, Mode.pantiltjoy };
            var i2cRequired = new[] { Mode.pantiltface, Mode.pantiltjoy ,Mode.pantiltcolour, Mode.pantiltmultimode };

            ICaptureGrab capture = null;
            if (!noCaptureGrabs.Contains(_consoleOptions.Mode))
            {
                capture = BuildCaptureGrabber();
            }

            IPanTiltMechanism panTiltMech = null;
            IScreen screen = null;
            if (i2cRequired.Contains(_consoleOptions.Mode))
            {
                var pwmDeviceFactory = new Pca9685DeviceFactory();
                var pwmDevice = pwmDeviceFactory.GetDevice(_consoleOptions.UseFakeDevice);
                panTiltMech = new PanTiltMechanism(pwmDevice);
                screen = new ConsoleScreen();
                screen.Clear();
            }
            else
            {
                Log.Info("Pan Tilt is not required");
            }

            IRunner runner;
            Log.Info(_consoleOptions);
            switch (_consoleOptions.Mode)
            {
                case Mode.noop: runner = new NoopRunner(capture);
                    break;

                case Mode.simple:runner = new SimpleCv(); 
                    break;

                case Mode.colourdetect:
                    var colorDetector = new ColorDetectRunner(capture);
                    if (_consoleOptions.HasColourSettings)
                    {
                        colorDetector.Settings = _consoleOptions.ColourSettings;
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
                    runner = new ServoSorter(capture, _consoleOptions);
                    break;

                case Mode.pantiltjoy:
                    var joyController = new JoystickPanTiltController(panTiltMech);
                    runner = new TimerRunner(joyController, screen);
                    break;

                case Mode.pantiltface:
                    var controllerF = new FaceTrackingPanTiltController(panTiltMech, capture.RequestedConfig);
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

                    var controllerMultimode = new MultimodePanTiltController(panTiltMech, capture.RequestedConfig, remoteScreen, cameraHubProxy, imageTransmitter);

                    var cameraBasedRunner = new CameraBasedPanTiltRunner(panTiltMech, capture, controllerMultimode, screen);
                    runner = cameraBasedRunner;

                    cameraHubProxy.UpdateCapture += (s, e) =>
                    {
                        remoteScreen.WriteLine($"Changing capture settings to {e}");
                        var newGrabber = BuildCaptureGrabber(e);
                        cameraBasedRunner.UpdateCaptureGrabber(newGrabber);
                    };
                    break;

                case Mode.pantiltcolour:
                    var controllerC = new ColourTrackingPanTiltController(panTiltMech, capture.RequestedConfig);
                    if (_consoleOptions.HasColourSettings)
                    {
                        controllerC.Settings = _consoleOptions.ColourSettings;
                    }
                    else
                    {
                        throw KrakenException.Create("Colour settings not found");
                    }
                    runner = new CameraBasedPanTiltRunner(panTiltMech, capture, controllerC, screen);
                    break;

                default:
                    throw KrakenException.Create("Option mode {0} needs wiring up", _consoleOptions.Mode);
            }

            runner.Run();
        }

        private static ICaptureGrab BuildCaptureGrabber(CaptureConfig config = null)
        {
            var request = new CaptureRequest {Device = CaptureDevice.Usb};
            if (EnvironmentService.IsUnix)
            {
                request.Device = CaptureDevice.Pi;
            }

            if (config == null)
            { 
                // Default capture
                request.Config = new CaptureConfig {Resolution = new Resolution(160, 120), Framerate = 25, Monochrome = false};
            }

            var capture = CaptureFactory.GetCapture(request);
            var actualConfig = capture.GetCaptureProperties();
            Log.Info($"Created capture: {actualConfig}");

            SafetyCheckRoi(_consoleOptions, actualConfig);
            return capture;
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
