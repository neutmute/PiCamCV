using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Kraken.Core;

namespace PiCamCV.WinForms.CameraConsumers
{
    public partial class ShapeDetectionControl : CameraConsumerUserControl
    {
        public ShapeDetectionControl()
        {
            InitializeComponent();
        }

        public override void ImageGrabbedHandler(object sender, EventArgs e)
        {
            using (var matCaptured = new Mat())
            {
                CameraCapture.Retrieve(matCaptured);

                var grayImage = matCaptured.ToImage<Gray, byte>();

                #region circle detection
                var watch = Stopwatch.StartNew();
                double cannyThreshold = 180.0;
                double circleAccumulatorThreshold = 120;
                CircleF[] circles = CvInvoke.HoughCircles(
                    grayImage
                    , HoughType.Gradient
                    , 2.0
                    , 40.0
                    , cannyThreshold
                    , circleAccumulatorThreshold
                    , 5);

                watch.Stop();
                NotifyStatus("{0} Hough circles in {1}; ", circles.Length, watch.Elapsed.ToHumanReadable());
                #endregion

                #region draw circles
                var circleImage = matCaptured.ToImage<Bgr, byte>();
                foreach (CircleF circle in circles)
                {
                    circleImage.Draw(circle, new Bgr(Color.Green), 10);
                }
                #endregion

                imageBoxCaptured.Image = circleImage;
            }
        }
    }
}
