using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using Common.Logging;
using Emgu.CV;
using Emgu.CV.Structure;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using PiCam.Web.Models;
using PiCamCV.Common.ExtensionMethods;
using PiCamCV.Common.PanTilt.Controllers;
using PiCamCV.ConsoleApp.Runners.PanTilt;
using Web;

namespace PiCam.Web.Controllers
{
    public class PiBroker
    {
        private readonly static Lazy<PiBroker> _instance = new Lazy<PiBroker>(() => new PiBroker(
            new CameraClient(GlobalHost.ConnectionManager.GetHubContext<CameraHub>().Clients)
            , new BrowserClient(GlobalHost.ConnectionManager.GetHubContext<BrowserHub>().Clients)));

        private bool _firstImageReceived = false;
        private Size _imageSize = Size.Empty;

        public static ImageCache ImageCache { get; set; }

        private static readonly ILog Log = LogManager.GetLogger<PiBroker>();

        private string _cameraIp;
        public SystemSettings SystemSettings { get; set; }

        private bool IsCameraConnected => !string.IsNullOrEmpty(_cameraIp);

        public static PiBroker Instance => _instance.Value;

        public ICameraClient Camera { get;set;}

        public IBrowserClient Browsers { get; set; }

        private PiBroker(ICameraClient cameraClient, IBrowserClient browserClients)
        {
            Camera = cameraClient;
            Browsers = browserClients;
            SystemSettings = new SystemSettings {JpegCompression= 90, TransmitImageEveryMilliseconds = 200};
        }

        public void BrowserConnected(string connectionId, string ip)
        {
            var camMsg = IsCameraConnected ? $"Camera is connected from {_cameraIp}" : "Waiting for a camera to connect";
            var msg = $"Hello {connectionId} from {ip}.\r\n{camMsg}";
            Browsers.Toast(msg);
            Browsers.InformSettings(SystemSettings);
        }
        

        public void CameraConnected(string ip)
        {
            _cameraIp = ip;
            var msg = $"Camera has connected from {ip}";
            Log.Info(msg);
            Browsers.Toast(msg);
            Camera.SetImageTransmitPeriod(TimeSpan.FromMilliseconds(SystemSettings.TransmitImageEveryMilliseconds));

            _firstImageReceived = false;
        }

        private Rectangle GetRegionOfInterest()
        {
            var percentSize = SystemSettings.RegionOfInterestPercent / 100m;

            var squareLength = _imageSize.Width*percentSize;

            var roiRect = new Rectangle(
                  Convert.ToInt32((_imageSize.Width - squareLength) / 2)
                , Convert.ToInt32((_imageSize.Height - squareLength) / 2)
                , Convert.ToInt32(squareLength)
                , Convert.ToInt32(squareLength));

            return roiRect;
        }

        public void ImageReceived(Image<Bgr, byte> image)
        {
            if (image == null)
            {
                Browsers.ScreenWriteLine("null image. bug.");
                return;
            }

            if (!_firstImageReceived)
            {
                _imageSize = image.Size;
                _firstImageReceived = true;
            }

            if (SystemSettings.ShowRegionOfInterest)
            {
                var roiRect = GetRegionOfInterest();
                image.Draw(roiRect, Color.Blue.ToBgr(), 2);
            }

            var jpeg = image.ToJpegData(SystemSettings.JpegCompression);

            // left this here as it was a pain to inject into the pibroker
            ImageCache.ImageJpeg = jpeg;
            ImageCache.Counter++;
            
            string signalRContent = null;
            if (SystemSettings.TransmitImageViaSignalR)
            {
                var base64Image = Convert.ToBase64String(jpeg);
                signalRContent = $"data:image/jpg;base64,{base64Image}";
            }
            Browsers.ImageReady(signalRContent);
        }

        public void ChangeSettings(SystemSettings settings)
        {
            var roiPercentChanged = settings.RegionOfInterestPercent != SystemSettings.RegionOfInterestPercent;

            SystemSettings = settings;
            Camera.SetImageTransmitPeriod(TimeSpan.FromMilliseconds(settings.TransmitImageEveryMilliseconds));
            Camera.SetRegionOfInterest(GetRegionOfInterest());
            
            Browsers.InformSettings(SystemSettings);

            if (!roiPercentChanged) // don't spam
            {
                Browsers.Toast("New settings received");
            }
        }

        public void CameraMoveRelative(PanTiltAxis axis, int units)
        {
            var setting = new PanTiltSetting();
            switch (axis)
            {
                case PanTiltAxis.Horizontal:
                    setting.PanPercent = units;
                    break;
                case PanTiltAxis.Vertical:
                    setting.TiltPercent = units;
                    break;
            }

            Camera.MoveRelative(setting);
        }

        public void StartColourTrack()
        {
            Camera.SetMode(ProcessingMode.ColourObjectSelect);   
        }
    }
}