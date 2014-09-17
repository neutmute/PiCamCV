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

            //var xmlContent = Resource.GetStringFromEmbedded(typeof(CascadeDetector).Assembly, "PiCamCV.Common.haarcascades.haarcascade_lego_batman4.xml");
            //var xmlContent =File.ReadAllText(@"C:\CodeOther\PiCamCV\source\WinForms\WinForms\CameraConsumers\FaceDetection\haarcascade_frontalface_default.xml");
            //var xmlContent = File.ReadAllText(@"C:\CodeOther\PiCamCV\source\PiCamCV.Common\haarcascades\haarcascade_castrillon_mouth.xml");
            //var xmlContent = File.ReadAllText(@"C:\CodeOther\PiCamCV\source\PiCamCV.Common\haarcascades\haarcascade_lego_batman5.xml");

            var cascadeToLoad = @"C:\CodeOther\PiCamCV\source\PiCamCV.Common\haarcascades\haarcascade_lego_batmanU1.xml";
            var cascadeFileInfo = new FileInfo(cascadeToLoad);
            if (cascadeFileInfo.Exists)
            {
                var xmlContent = File.ReadAllText(cascadeToLoad);
                _detector = new CascadeDetector(xmlContent);
            }
            else
            {
                Log.Error(m=>m("Failed to load cascade {0}", cascadeToLoad));
            }
         
        }

        public override void ImageGrabbedHandler(object sender, EventArgs e)
        {
            using (var matCaptured = new Mat())
            {
                CameraCapture.Retrieve(matCaptured);
                var input = new CascadeDetectorInput {Captured = matCaptured};
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
}
