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
    public partial class BasicCaptureControl : CameraConsumerUserControl
    {
        public BasicCaptureControl()
        {
            InitializeComponent();
        }


        public override void ImageGrabbedHandler(object sender, EventArgs e)
        {
            Mat frame = new Mat();
            CameraCapture.Retrieve(frame);

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



        private void CameraCapture_Load(object sender, EventArgs e)
        {
        }
    }
}
