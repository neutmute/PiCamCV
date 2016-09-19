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
using PiCamCV.Common.PanTilt.Controllers;
using PiCamCV.Interfaces;
using PiCamCV.WinForms.CameraConsumers.Base;
using Web.Client;

namespace PiCamCV.WinForms.CameraConsumers
{
    public partial class ServerProcessingControl : PanTiltBaseUserControl
    {
        private PiServerClient _server;
        private Task _postTask;
        private MultimodePanTiltController _multimodePanTiltController;

        public ServerProcessingControl()
        {
            InitializeComponent();
            _server = new PiServerClient();
        }

        protected override void OnSubscribe()
        {
            base.OnSubscribe();

            var screen = new RemoteScreen(CameraHubProxy);

            var captureConfig = CameraCapture.GetCaptureProperties();

            _multimodePanTiltController = new MultimodePanTiltController(PanTiltMechanism, captureConfig, screen);
        }

        public override void ImageGrabbedHandler(object sender, EventArgs e)
        {
            if (_postTask != null && !_postTask.IsCompleted)
            {
                return;
            }
            using (var matCaptured = new Mat())
            {
                CameraCapture.Retrieve(matCaptured);
                var bgrImage = matCaptured.ToImage<Bgr, byte>();
                WriteText(bgrImage, 30, DateTime.Now.ToString("HH:mm:ss tt"));
                imageBoxCaptured.Image = bgrImage;
                
                _postTask = _server.PostBson(bgrImage);
            }
        }

    }
}
