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
            var frame = new Mat();
            if (!CameraCapture.Retrieve(frame)) return;

            var grayFrame = new Mat();
            CvInvoke.CvtColor(frame, grayFrame, ColorConversion.Bgr2Gray);
            
            var smallGrayFrame = new Mat();
            CvInvoke.PyrDown(grayFrame, smallGrayFrame);
            
            var smoothedGrayFrame = new Mat();
            CvInvoke.PyrUp(smallGrayFrame, smoothedGrayFrame);

            var cannyFrame = new Mat();
            CvInvoke.Canny(smoothedGrayFrame, cannyFrame, 100, 60);

            imageBoxCaptured.Image = frame;
            imageBoxGray.Image = grayFrame;
            imageBoxSmoothedGray.Image = smoothedGrayFrame;
            imageBoxCanny.Image = cannyFrame;

            NotifyStatus(string.Empty);
        }
    }
}
