using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
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
        
        public event EventHandler<ProcessingMode> SetMode;

        public event EventHandler<PanTiltSetting> MoveAbsolute;

        public event EventHandler<PanTiltSetting> MoveRelative;


        public void InvokeMoveAbsolute(PanTiltSetting setting)
        {
            MoveAbsolute?.Invoke(this, setting);
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
                }

            }).Wait();


            _proxy.On<ProcessingMode>("setMode", param =>
            {
                SetMode?.Invoke(this, param);
            });

            _proxy.On<PanTiltSetting>("moveAbsolute", InvokeMoveAbsolute);

            _proxy.On<string>("writeLine", (s) => Console.WriteLine(s));

            _proxy.On<PanTiltSetting>("moveRelative", param =>
            {
                MoveRelative?.Invoke(this, param);
            });
        }

        public void Message(string message)
        {
            _proxy.Invoke<string>("Message", message).ContinueWith(task => {
                if (task.IsFaulted)
                {
                    //Console.WriteLine("There was an error calling send: {0}",task.Exception.GetBaseException());
                }
                else
                {
                    //Console.WriteLine(task.Result);
                }
            });
        }

        public void Dispose()
        {
            _connection?.Stop();
        }
    }
}
