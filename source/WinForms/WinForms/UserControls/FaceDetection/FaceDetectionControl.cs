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
    public partial class FaceDetectionControl : UserControl
    {
        private CapturePi _capture;
        private bool _captureInProgress;
        private FileInfo haarEyeFile;
        private FileInfo haarFaceFile;

        public FaceDetectionControl()
        {
            InitializeComponent();
        }
        private void FaceDetectionControl_Load(object sender, EventArgs e)
        {
            CvInvoke.UseOpenCL = false;
            try
            {
                CvInvoke.CheckLibraryLoaded();
                _capture = new CapturePi();
                _capture.ImageGrabbed += _capture_ImageGrabbed;
            }
            catch (NullReferenceException excpt)
            {
                MessageBox.Show(excpt.Message);
            }

            var assemblyPath = Assembly.GetExecutingAssembly().Location;
            var haarCascadePath = Path.Combine(new FileInfo(assemblyPath).DirectoryName, "UserControls\\FaceDetection");
            haarEyeFile = new FileInfo(Path.Combine(haarCascadePath, "haarcascade_eye.xml"));
            haarFaceFile = new FileInfo(Path.Combine(haarCascadePath, "haarcascade_frontalface_default.xml"));
        }

        void _capture_ImageGrabbed(object sender, EventArgs e)
        {
            var image = _capture.RetrieveBgrFrame();

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
            labelDetectionTime.Invoke((MethodInvoker)delegate
            {
                labelDetectionTime.Text = string.Format("Detected in {0}", result.DetectionTime.ToHumanReadable());
                
            });
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            _capture.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _capture.Stop();
            labelDetectionTime.Text = string.Empty;
        }

    }
}

