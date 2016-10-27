using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using Microsoft.AspNet.SignalR.Client;
using PiCamCV.Common;
using PiCamCV.Common.ExtensionMethods;
using PiCamCV.Common.Interfaces;
using PiCamCV.Common.PanTilt.Controllers;
using PiCamCV.ConsoleApp.Runners.PanTilt;

namespace Web.Client
{

    /// <summary>
    /// Pi side settings
    /// </summary>
    public class CameraHubProxy : IDisposable, IServerToCameraBus, ICameraToServerBus
    {

        private IHubProxy _proxy;
        private HubConnection _connection;
        private bool _connected;
        
        public event EventHandler<ProcessingMode> SetMode;
        
        public event EventHandler<PiSettings> SettingsChanged;

        public event EventHandler<Rectangle> SetRegionOfInterest;

        public event EventHandler<PanTiltSettingCommand> PanTiltCommand;

        public event EventHandler<CaptureConfig> UpdateCapture;


        public void InvokeMoveAbsolute(PanTiltSetting setting)
        {
            
        }
        
        public void Connect()
        {
            var endpoint = $"http://{Config.ServerHost}:{Config.ServerPort}/";
            Console.WriteLine($"Connecting to {endpoint}");
            
            _connection = new HubConnection(endpoint);
            
            _proxy = _connection.CreateHubProxy("CameraHub");
            
            _connection.Start().ContinueWith(task => {
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error opening the connection:{0}", task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine("Connected");
                    _connected = true;
                }

            }).Wait();

            
            InformIp();

            _proxy.On<ProcessingMode>("setMode", param =>{SetMode?.Invoke(this, param);});

            _proxy.On<PanTiltSettingCommand>("panTiltCommand", s => PanTiltCommand?.Invoke(this, s));
            
            _proxy.On<PiSettings>("updateSettings", settings => SettingsChanged?.Invoke(this, settings));

            _proxy.On<Rectangle>("setRegionOfInterest", r => SetRegionOfInterest?.Invoke(this, r));

            _proxy.On<CaptureConfig>("updateCapture", c => UpdateCapture?.Invoke(this, c));

        }

        public void ScreenWriteLine(string message)
        {
            if (!_connected)
            {
                return;
            }
            _proxy.Invoke<string>("ScreenWriteLine", message);
        }

        public void ScreenClear()
        {
            if (!_connected)
            {
                return;
            }
            _proxy.Invoke("ScreenClear");
        }

        public void InformIp()
        {
            if (!_connected)
            {
                return;
            }
            var networkService= new NetworkService();
            var allIps = new List<string>();
            allIps.AddRange(networkService.GetAllLocalIPv4(NetworkInterfaceType.Ethernet));
            allIps.AddRange(networkService.GetAllLocalIPv4(NetworkInterfaceType.Wireless80211));
            
            var allIpCsv = string.Join(",", allIps);
            _proxy.Invoke("InformIp", allIpCsv);
        }

        public void Dispose()
        {
            _connection?.Stop();
        }
    }
}
