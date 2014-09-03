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
using Emgu.CV.Structure;
using System.Diagnostics;

namespace PiCamCV.WinForms.CameraConsumers
{
    public partial class ColourDetectionControl : CameraConsumerUserControl
    {
        public ColourDetectionControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Based off http://opencv-srf.blogspot.com.au/2010/09/object-detection-using-color-seperation.html
        /// </summary>
        public override void ImageGrabbedHandler(object sender, EventArgs e)
        {
            var matCaptured = new Mat();
            CameraCapture.Retrieve(matCaptured);

            var hsvFrame = new Mat();
            CvInvoke.CvtColor(matCaptured, hsvFrame, ColorConversion.Bgr2Hsv);

            var matThresholded = new Mat();
            var lowH = sliderHueMin.Value;
            var lowS = sliderSaturationMin.Value;
            var lowV = sliderValueMin.Value;

            var highH = sliderHueMax.Value;
            var highS = sliderSaturationMax.Value;
            var highV = sliderValueMax.Value;

            var lowMScalar = new MCvScalar(lowH, lowS, lowV);
            var highMScalar = new MCvScalar(highH, highS, highV);

            using (var lowerScalar = new ScalarArray(lowMScalar))
            {
                using (var upperScalar = new ScalarArray(highMScalar))
                {
                       CvInvoke.InRange(hsvFrame, lowerScalar, upperScalar, matThresholded); //Threshold the image
                }
            }

            var thresholdImage = matThresholded.ToImage<Gray, byte>();
            const int erodeDilateIterations = 10;
            //morphological opening (remove small objects from the foreground)
            thresholdImage.Erode(erodeDilateIterations);
            thresholdImage.Dilate(erodeDilateIterations);

            //morphological closing (fill small holes in the foreground)
            thresholdImage.Dilate(erodeDilateIterations);
            thresholdImage.Erode(erodeDilateIterations);

            var capturedImage = matCaptured.ToImage<Bgr, byte>();
            var moments = thresholdImage.GetMoments(true);
            moments.GetCentralMoment(0, 0);

            var area = moments.M00;
            if (area > 200)
            {
                int posX = Convert.ToInt32(moments.M10/area);
                int posY = Convert.ToInt32(moments.M01/area);
                var circleCenter = new PointF(posX, posY);
                var radius = 50;
                var circle = new CircleF( circleCenter, radius);
                var color = new Bgr(Color.Yellow);
                capturedImage.Draw(circle, color, 3);
                capturedImage.Draw("ball", new Point(posX + radius, posY), FontFace.HersheyPlain,3, color);
            }

            //#region circle detection
            //var watch = Stopwatch.StartNew();
            //double cannyThreshold = 180.0;
            //double circleAccumulatorThreshold = 120;
            //CircleF[] circles = CvInvoke.HoughCircles(
            //    thresholdImage
            //    , HoughType.Gradient
            //    , 2.0
            //    , 20.0
            //    , cannyThreshold
            //    , circleAccumulatorThreshold
            //    , 5);

            //watch.Stop();
            //NotifyStatus("Hough circles - {0} ms; ", watch.ElapsedMilliseconds);
            //#endregion

            //#region draw circles
            //var circleImage = matCaptured.ToImage<Bgr, byte>();
            //foreach (CircleF circle in circles)
            //{
            //    circleImage.Draw(circle, new Bgr(Color.Brown), 2);
            //}
            //#endregion

            imageBoxCaptured.Image = capturedImage;
            imageBoxFiltered.Image = thresholdImage;
        }

        private void ColourDetectionControl_Load(object sender, EventArgs e)
        {
            sliderHueMin.Value = 0;
            sliderSaturationMin.Value = 0;
            sliderValueMin.Value = 0;

            sliderHueMax.Value = 255;
            sliderSaturationMax.Value = 255;
            sliderValueMax.Value = 255;


            sliderHueMin.Value = 140;
            sliderSaturationMin.Value = 57;
            sliderValueMin.Value = 25;

            sliderHueMax.Value = 187;
            sliderSaturationMax.Value = 153;
            sliderValueMax.Value = 82;
        }
    }
}
