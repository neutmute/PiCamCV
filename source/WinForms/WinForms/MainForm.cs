using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using PiCamCV;
using PiCamCV.WinForms;
using PiCamCV.WinForms.UserControls;

namespace WinForms
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();

        }



        private void tabPageCameraCapture_Click(object sender, EventArgs e)
        {
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //var camCapture = new CameraCapture();
            //camCapture.Dock = DockStyle.Fill;

            var faceDetection = new FaceDetectionControl();
            faceDetection.Dock = DockStyle.Fill;

            //tabPageCameraCapture.Controls.Add(camCapture);
            tabPageFaceDetection.Controls.Add(faceDetection);
        }
    }
}
