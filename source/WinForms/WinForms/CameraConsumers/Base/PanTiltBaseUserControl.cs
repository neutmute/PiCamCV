using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PiCamCV.Common;
using Web.Client;
using RPi.Pwm;

namespace PiCamCV.WinForms.CameraConsumers.Base
{
    public class PanTiltBaseUserControl : CameraConsumerUserControl
    {
        protected CameraHubProxy CameraHubProxy { get; private set; }

        protected IPanTiltMechanism PanTiltMechanism { get; set; }

        protected override void OnSubscribe()
        {
            CameraHubProxy = new CameraHubProxy();
            CameraHubProxy.Connect();
            InitI2C();
        }


        private void InitI2C()
        {
            Log.Info("Initialising I2C bus");
            if (PanTiltMechanism == null)
            {
                var pwmDeviceFactory = new Pca9685DeviceFactory();
                var pwmDevice = pwmDeviceFactory.GetDevice();
                PanTiltMechanism = new PanTiltMechanism(pwmDevice);
            }
        }
    }
}
