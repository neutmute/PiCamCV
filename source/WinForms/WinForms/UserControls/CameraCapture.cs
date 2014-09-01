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
        private bool _captureInProgress;
        public CameraCapture()
        {
            InitializeComponent();

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
            if (_capture != null)
            {
                if (_captureInProgress)
                {  //stop the capture
                    btnStartStop.Text = "Start Capture";
                    _capture.Pause();
                }
                else
                {
                    //start the capture
                    btnStartStop.Text = "Stop";
                    _capture.Start();
                }

                _captureInProgress = !_captureInProgress;
            }
        }

        private void btnFlipVertical_Click(object sender, EventArgs e)
        {
            if (_capture != null) _capture.FlipVertical = !_capture.FlipVertical;
        }

        private void btnFlipHorizontal_Click(object sender, EventArgs e)
        {
            if (_capture != null) _capture.FlipHorizontal = !_capture.FlipHorizontal;
        }
    }
}
