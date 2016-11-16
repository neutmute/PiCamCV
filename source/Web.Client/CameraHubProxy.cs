using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Timers;
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

        private Timer _reconnectTimer;
        
        public event EventHandler<ProcessingMode> SetMode;
        
        public event EventHandler<PiSettings> SettingsChanged;

        public event EventHandler<Rectangle> SetRegionOfInterest;

        public event EventHandler<PanTiltSettingCommand> PanTiltCommand;

        public event EventHandler<CaptureConfig> UpdateCapture;

        public void InvokeUpdateCapture(CaptureConfig captureConfig)
        {
            UpdateCapture?.Invoke(this, captureConfig);
        }


        public void InvokeMoveAbsolute(PanTiltSetting setting)
        {
            
        }
        
        public void Connect()
        {
            if (_connection != null)
            {
                _connection.Dispose();
            }

            if (!Config.IsValid)
            {
                Log("Bad config - environment variables expected- see Web.Client.Config.cs");
                return;
            }
            var endpoint = $"http://{Config.ServerHost}:{Config.ServerPort}/";
            Log($"Connecting to {endpoint}");
            
            _connection = new HubConnection(endpoint);

            _connection.Error += _connection_Error;
            _connection.Closed += _connection_Closed;
            _connection.StateChanged += _connection_StateChanged;
            _connection.ConnectionSlow += _connection_ConnectionSlow;

            _proxy = _connection.CreateHubProxy("CameraHub");
            
            _connection.Start().ContinueWith(task => {
                if (task.IsFaulted)
                {
                    Log($"There was an error opening the connection:{task.Exception.GetBaseException()}");
                }
                else
                {
                    Log("Connected");
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

        private void _connection_ConnectionSlow()
        {
            Log("Connection slow!");
        }

        private void _connection_StateChanged(StateChange obj)
        {
            Log($"Connection state {obj.OldState}=>{obj.NewState}");
        }

        private void _connection_Closed()
        {
            Log("Connection closed");
            _connected = false;
        }

        private void _connection_Error(Exception obj)
        {
            Log("Connection error:" + obj);
            _connected = false;
        }

        public void ScreenWriteLine(string message)
        {
            if (!_connected)
            {
                return;
            }
            SafeInvokeRemote("ScreenWriteLine", message);
        }

        public void ScreenClear()
        {
            if (!_connected)
            {
                return;
            }
            SafeInvokeRemote("ScreenClear");
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

            Log($"Local IPs: {allIpCsv}");

            SafeInvokeRemote("InformIp", allIpCsv);
        }

        public void Dispose()
        {
            _connection?.Stop();
        }

        private void SafeInvokeRemote(string method, object arg = null)
        {
            try
            {
                if (arg == null)
                {
                    _proxy.Invoke(method);
                }
                else
                {
                    _proxy.Invoke(method, arg);
                }
            }
            catch (Exception e)
            {
                _connected = false;
                const int retryMilliseconds = 30000;
                _reconnectTimer = new Timer(retryMilliseconds);
                _reconnectTimer.Elapsed += ReconnectTimerElapsed;
                _reconnectTimer.Start();
                Log($"{e.Message}. Retrying in {retryMilliseconds}");
            }
        }

        private void ReconnectTimerElapsed(object sender, ElapsedEventArgs e)
        {
            Log("Reconnecting");
            Connect();
        }

        private void Log(string message)
        {
            Console.WriteLine($"CameraHub: {message}");
        }
    }
}
