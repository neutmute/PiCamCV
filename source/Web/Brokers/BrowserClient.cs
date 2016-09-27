using System;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
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
            var now = DateTimeOffset.UtcNow.AddHours(11); //AEST hack
            _clients.All.ScreenWriteLine($"{now.ToString("HH:mm:ss.ff")} {message}");
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
            _clients.All.ImageReady(base64encodedImage);
        }

        public void InformSettings(SystemSettings settings)
        {
            _clients.All.informSettings(settings);
        }
    }
}