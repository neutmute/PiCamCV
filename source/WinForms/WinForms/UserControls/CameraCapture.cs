using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;

namespace PiCamCV.WinForms
{
    public partial class CameraCapture : UserControl
    {
        private CapturePi _capture;
        public CameraCapture()
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

            Mat grayFrame = new Mat();
            CvInvoke.CvtColor(frame, grayFrame, ColorConversion.Bgr2Gray);
            Mat smallGrayFrame = new Mat();
            CvInvoke.PyrDown(grayFrame, smallGrayFrame);
            Mat smoothedGrayFrame = new Mat();
            CvInvoke.PyrUp(smallGrayFrame, smoothedGrayFrame);

            Mat cannyFrame = new Mat();
            CvInvoke.Canny(smoothedGrayFrame, cannyFrame, 100, 60);

            imageBoxCaptured.Image = frame;
            imageBoxGray.Image = grayFrame;
            imageBoxSmoothedGray.Image = smoothedGrayFrame;
            imageBoxCanny.Image = cannyFrame;
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {

            _capture.Start();
        }

        private void btnFlipVertical_Click(object sender, EventArgs e)
        {

        }

        private void btnFlipHorizontal_Click(object sender, EventArgs e)
        {

        }
    }
}
