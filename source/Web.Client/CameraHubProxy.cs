using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using Microsoft.AspNet.SignalR.Client;
using PiCamCV.Common.Interfaces;
using PiCamCV.Common.PanTilt.Controllers;
using PiCamCV.ConsoleApp.Runners.PanTilt;

namespace Web.Client
{

    public class CameraHubProxy : IDisposable, IServerToCameraBus, ICameraToServerBus
    {

        private IHubProxy _proxy;
        private HubConnection _connection;
        private bool _connected;
        
        public event EventHandler<ProcessingMode> SetMode;

        public event EventHandler<PanTiltSetting> MoveAbsolute;

        public event EventHandler<PanTiltSetting> MoveRelative;

        public event EventHandler<TimeSpan> SetImageTransmitPeriod;

        public event EventHandler<Rectangle> SetRegionOfInterest;


        public void InvokeMoveAbsolute(PanTiltSetting setting)
        {
            
        }
        
        public void Connect()
        {
            _connection = new HubConnection($"http://{Config.ServerHost}:{Config.ServerPort}/");
            
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

            _proxy.On<PanTiltSetting>("moveAbsolute", s => MoveAbsolute?.Invoke(this, s));
            
            _proxy.On<PanTiltSetting>("moveRelative", param =>{MoveRelative?.Invoke(this, param);});

            _proxy.On<string>("writeLine", Console.WriteLine);

            _proxy.On<TimeSpan>("setImageTransmitPeriod", ts => SetImageTransmitPeriod?.Invoke(this, ts));

            _proxy.On<Rectangle>("setRegionOfInterest", r => SetRegionOfInterest?.Invoke(this, r));

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
