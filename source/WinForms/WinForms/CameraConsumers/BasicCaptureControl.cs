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
            using(var inputFrame = new Mat())
            {
                if (!CameraCapture.Retrieve(inputFrame)) return;

                using (var smoothedGrayFrame = new Mat())
                using (var smallGrayFrame = new Mat())
                using (var cannyFrame = new Mat())
                using (var grayFrame = new Mat())
                {
                    if (inputFrame.NumberOfChannels > 1)
                    {
                        CvInvoke.CvtColor(inputFrame, grayFrame, ColorConversion.Bgr2Gray);
                    }
                    else
                    {
                        inputFrame.CopyTo(grayFrame);
                    }

                    CvInvoke.PyrDown(grayFrame, smallGrayFrame);

                    CvInvoke.PyrUp(smallGrayFrame, smoothedGrayFrame);

                    CvInvoke.Canny(smoothedGrayFrame, cannyFrame, 100, 60);

                    imageBoxCaptured.Image = inputFrame;
                    imageBoxGray.Image = grayFrame;
                    imageBoxSmoothedGray.Image = smoothedGrayFrame;
                    imageBoxCanny.Image = cannyFrame;
                }
            }

            NotifyStatus(string.Empty);
        }
    }
}
