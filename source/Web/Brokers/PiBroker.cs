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
using PiCamCV.Common;
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

        private string _cameraExternalIp;

        public string CameraInteralIpCsv { get; set; }

        public ServerSettings ServerSettings { get; set; }

        public PiSettings PiSettings { get; set; }

        private bool IsCameraConnected => !string.IsNullOrEmpty(_cameraExternalIp);

        public static PiBroker Instance => _instance.Value;

        public ICameraClient Camera { get;set;}

        public IBrowserClient Browsers { get; set; }

        private PiBroker(ICameraClient cameraClient, IBrowserClient browserClients)
        {
            Camera = cameraClient;
            Browsers = browserClients;
            ServerSettings = new ServerSettings {JpegCompression= 90};
            PiSettings = new PiSettings {TransmitImagePeriod = TimeSpan.FromMilliseconds(200)};
        }

        public void BrowserConnected(string connectionId, string ip)
        {
            var camMsg = IsCameraConnected ? $"Camera is connected from {_cameraExternalIp}. All IPs: {CameraInteralIpCsv}" : "Waiting for a camera to connect";
            var msg = $"Hello {connectionId} from {ip}.\r\n{camMsg}";
            Browsers.Toast(msg);
            Browsers.InformSettings(ServerSettings);
        }
        
        public void CameraConnected(string ip)
        {
            _cameraExternalIp = ip;
            var msg = $"Camera has connected from {ip}";
            Log.Info(msg);
            Browsers.Toast(msg);
            Camera.UpdateSettings(PiSettings);

            _firstImageReceived = false;
        }

        public void CameraDisconnected()
        {
            _cameraExternalIp = null;
            Browsers.Toast("Camera has disconnected");
        }

        private Rectangle GetRegionOfInterest()
        {
            var percentSize = ServerSettings.RegionOfInterestPercent / 100m;

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
            if (!_firstImageReceived)
            {
                _imageSize = image.Size;
                _firstImageReceived = true;
            }

            var isFullImage = _imageSize == image.Size; // its not a full image while training the color threshold
            if (isFullImage && ServerSettings.ShowRegionOfInterest)
            {
                var roiRect = GetRegionOfInterest();
                image.Draw(roiRect, Color.Blue.ToBgr(), 2);
            }

            var jpeg = image.ToJpegData(ServerSettings.JpegCompression);

            // left this here as it was a pain to inject into the pibroker
            ImageCache.ImageJpeg = jpeg;
            ImageCache.Counter++;
            
            string signalRContent = null;
            if (ServerSettings.TransmitImageViaSignalR)
            {
                var base64Image = Convert.ToBase64String(jpeg);
                signalRContent = $"data:image/jpg;base64,{base64Image}";
            }
            Browsers.ImageReady(signalRContent);
        }

        public void UpdatePi(PiSettings settings)
        {
            PiSettings = settings;
            Camera.UpdateSettings(PiSettings);
        }

        public void UpdateServer(ServerSettings settings)
        {
            var roiPercentChanged = settings.RegionOfInterestPercent != ServerSettings.RegionOfInterestPercent;
            
            ServerSettings = settings;

            if (roiPercentChanged)
            {
                Camera.SetRegionOfInterest(GetRegionOfInterest());
            }

            Browsers.InformSettings(ServerSettings);

            if (!roiPercentChanged) // don't spam
            {
                Browsers.Toast("New settings received");
            }
        }

        public void CameraSendCommand(PanTiltSettingCommand command)
        {
            Camera.SendCommand(command);
        }
        
        public void SetMode(ProcessingMode mode)
        {
            Camera.SetMode(mode);
            Browsers.Toast($"Changing mode to {mode}");
        }

        public void SetCameraLocalIp(string ipCsv)
        {
            CameraInteralIpCsv= ipCsv;
            Browsers.Toast($"Camera local IPs are {ipCsv}");
        }
    }
}