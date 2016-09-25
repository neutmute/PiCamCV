using Microsoft.AspNet.SignalR.Hubs;
using PiCam.Web.Models;

namespace Web
{
    public class BrowserClient : IBrowserClient
    {
        private readonly IHubConnectionContext<dynamic> _clients;

        public BrowserClient(IHubConnectionContext<dynamic> clients)
        {
            _clients = clients;
        }
        
        public void ScreenWriteLine(string message)
        {
            _clients.All.ScreenWriteLine(message);
        }

        public void ScreenClear()
        {
            _clients.All.ScreenClear();
        }

        public void Toast(string message)
        {
            _clients.All.toast(message);
        }

        public void ImageReady(string base64encodedImage = null)
        {
            _clients.All.ImageReady($"data:image/jpg;base64,{base64encodedImage}");
        }

        public void InformSettings(SystemSettings settings)
        {
            var json = new { jpegCompression = settings.JpegCompression, transmitImageEveryMilliseconds = settings.TransmitImageEveryMilliseconds};
            _clients.All.informSettings(json);
        }
    }
}