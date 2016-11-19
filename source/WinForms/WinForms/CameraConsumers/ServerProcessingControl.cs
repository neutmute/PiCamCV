using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using PiCamCV.Common;
using PiCamCV.Common.Interfaces;
using PiCamCV.Common.PanTilt.Controllers;
using PiCamCV.Interfaces;
using PiCamCV.WinForms.CameraConsumers.Base;
using Web.Client;

namespace PiCamCV.WinForms.CameraConsumers
{
    public partial class ServerProcessingControl : PanTiltBaseUserControl
    {
        private IImageTransmitter _imageTransmitter;

        private IImageTransmitter _jpegTransmitter;
        
        private Task _transmitTask;

        public ServerProcessingControl()
        {
            InitializeComponent();
        }

        protected override void OnSubscribe()
        {
            base.OnSubscribe();
            _imageTransmitter = new BsonPostImageTransmitter();
            _jpegTransmitter = new BsonPostJpegTransmitter();
            lblHost.Text = $"{Config.ServerHost}:{Config.ServerPort}";
        }

        public override void ImageGrabbedHandler(object sender, EventArgs e)
        {
            if (_transmitTask == null || _transmitTask.IsCompleted)
            {
                using (var matCaptured = new Mat())
                {
                    CameraCapture.Retrieve(matCaptured);
                    var bgrImage = matCaptured.ToImage<Bgr, byte>();
                    WriteText(bgrImage, 30, DateTime.Now.ToString("HH:mm:ss tt"));
                    imageBoxCaptured.Image = bgrImage;


                    IImageTransmitter transmitter = null;
                    if (radBsonImage.Checked)
                    {
                        transmitter = _imageTransmitter;
                    }

                    if (radBsonJpeg.Checked)
                    {
                        transmitter = _jpegTransmitter;
                    }

                    if (transmitter != null)
                    {
                        _transmitTask = transmitter.Transmit(bgrImage);
                    }
                }
            }
        }

    }
}
