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
        private MultimodePanTiltController _multimodePanTiltController;
        private IImageTransmitter _bsonPoster;
        private Task _transmitTask;
        public ServerProcessingControl()
        {
            InitializeComponent();
        }

        protected override void OnSubscribe()
        {
            //base.OnSubscribe();
            //var captureConfig = CameraCapture.GetCaptureProperties();
            //var screen = new RemoteScreen(CameraHubProxy);
            //_multimodePanTiltController = new MultimodePanTiltController(PanTiltMechanism, captureConfig, screen, CameraHubProxy, imageTransmitter, imageTransmitter);

            _bsonPoster = new BsonPostImageTransmitter();
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

                    _transmitTask = _bsonPoster.Transmit(bgrImage);
                }
            }
        }

    }
}
