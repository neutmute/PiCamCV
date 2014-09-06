using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Kraken.Core;
using PiCamCV.Common;

namespace PiCamCV.WinForms.CameraConsumers
{
    public partial class HaarCascadeControl : CameraConsumerUserControl
    {
        private CascadeDetector _detector;
        public HaarCascadeControl()
        {
            InitializeComponent();

            var xmlContent = Resource.GetStringFromEmbedded(typeof(CascadeDetector).Assembly, "PiCamCV.Common.haarcascades.haarcascade_lego_batman2.xml");
            //var xmlContent =File.ReadAllText(@"C:\CodeOther\PiCamCV\source\WinForms\WinForms\CameraConsumers\FaceDetection\haarcascade_frontalface_default.xml");
            _detector = new CascadeDetector(xmlContent);
        }

        public override void ImageGrabbedHandler(object sender, EventArgs e)
        {
            
            var matCaptured = new Mat();
            CameraCapture.Retrieve(matCaptured);
            var input = new CascadeDetectorInput{Captured = matCaptured};
            var result = _detector.Process(input);
            var image = matCaptured.ToImage<Bgr, byte>();

            foreach (Rectangle item in result.Objects)
            {
                image.Draw(item, new Bgr(Color.Blue), 2);
            }

            imageBoxCaptured.Image = image;
        }
    }
}
