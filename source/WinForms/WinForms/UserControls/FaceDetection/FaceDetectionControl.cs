using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using FaceDetection;
using Kraken.Core;

namespace PiCamCV.WinForms.UserControls
{
    public partial class FaceDetectionControl : CameraConsumerUserControl
    {
        private FileInfo haarEyeFile;
        private FileInfo haarFaceFile;
        
        public FaceDetectionControl()
        {
            InitializeComponent();
        }
        public void ControlLoad(object sender, EventArgs e)
        {
            var assemblyPath = Assembly.GetExecutingAssembly().Location;
            var haarCascadePath = Path.Combine(new FileInfo(assemblyPath).DirectoryName, "UserControls/FaceDetection");
            haarEyeFile = new FileInfo(Path.Combine(haarCascadePath, "haarcascade_eye.xml"));
            haarFaceFile = new FileInfo(Path.Combine(haarCascadePath, "haarcascade_frontalface_default.xml"));
        }

        public override void ImageGrabbedHandler(object sender, EventArgs e)
        {
            var image = CameraCapture.RetrieveBgrFrame();

            var result = DetectFace.Detect(image, haarFaceFile.FullName, haarEyeFile.FullName);

            foreach (Rectangle face in result.Faces)
            {
                image.Draw(face, new Bgr(Color.Red), 2);
            }
            foreach (Rectangle eye in result.Eyes)
            {
                image.Draw(eye, new Bgr(Color.Blue), 2);
            }

            imageBox.Image = image;
            NotifyStatus("Face detection took {0}", result.DetectionTime.ToHumanReadable());
        }
        
    }
}

