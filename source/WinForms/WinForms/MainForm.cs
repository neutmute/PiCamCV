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
using PiCamCV;

namespace WinForms
{
    public partial class MainForm : Form
    {
        private CapturePi _capture;

        public MainForm()
        {
            InitializeComponent();

            CvInvoke.CheckLibraryLoaded();
            _capture = new CapturePi();
            _capture.ImageGrabbed += _capture_ImageGrabbed;
        }

        void _capture_ImageGrabbed(object sender, EventArgs e)
        {
            Mat frame = new Mat();
            _capture.Retrieve(frame);
            //Mat grayFrame = new Mat();
            //CvInvoke.CvtColor(frame, grayFrame, ColorConversion.Bgr2Gray);
            //Mat smallGrayFrame = new Mat();
            //CvInvoke.PyrDown(grayFrame, smallGrayFrame);
            //Mat smoothedGrayFrame = new Mat();
            //CvInvoke.PyrUp(smallGrayFrame, smoothedGrayFrame);

            ////Image<Gray, Byte> smallGrayFrame = grayFrame.PyrDown();
            ////Image<Gray, Byte> smoothedGrayFrame = smallGrayFrame.PyrUp();
            //Mat cannyFrame = new Mat();
            //CvInvoke.Canny(smoothedGrayFrame, cannyFrame, 100, 60);

            //Image<Gray, Byte> cannyFrame = smoothedGrayFrame.Canny(100, 60);

            imageBox.Image = frame;
            //grayscaleImageBox.Image = grayFrame;
            //smoothedGrayscaleImageBox.Image = smoothedGrayFrame;
            //cannyImageBox.Image = cannyFrame;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            _capture.Start();
        }
    }
}
