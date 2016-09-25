using System;
using System.Collections.Generic;
using System.Linq;
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
        
        public event EventHandler<ProcessingMode> SetMode;

        public event EventHandler<PanTiltSetting> MoveAbsolute;

        public event EventHandler<PanTiltSetting> MoveRelative;

        public event EventHandler<TimeSpan> SetImageTransmitPeriod;


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

            _proxy.On<PanTiltSetting>("moveRelative", param =>{MoveRelative?.Invoke(this, param);});

            _proxy.On<TimeSpan>("setImageTransmitPeriod", ts => SetImageTransmitPeriod?.Invoke(this, ts));

        }

        public void ScreenWriteLine(string message)
        {
            _proxy.Invoke<string>("ScreenWriteLine", message);
        }

        public void ScreenClear()
        {
            _proxy.Invoke("ScreenClear");
        }

        //public void SendImage(Image<Bgr, byte> image)
        //{
        //    throw new NotImplementedException();
        //}

        public void Dispose()
        {
            _connection?.Stop();
        }
    }
}
