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
using Kraken.Core;
using PiCamCV.Common;
using PiCamCV.Common.ExtensionMethods;

namespace PiCamCV.WinForms.CameraConsumers
{
    public partial class ColourDetectionControl : CameraConsumerUserControl
    {
        private ColourDetector _colorDetector;
        public ColourDetectionControl()
        {
            InitializeComponent();
            _colorDetector = new ColourDetector();
        }

        public override void ImageGrabbedHandler(object sender, EventArgs e)
        {
            var lowH = sliderHueMin.Value;
            var lowS = sliderSaturationMin.Value;
            var lowV = sliderValueMin.Value;

            var highH = sliderHueMax.Value;
            var highS = sliderSaturationMax.Value;
            var highV = sliderValueMax.Value;


            var matCaptured = new Mat();

            var w = Stopwatch.StartNew();
            CameraCapture.Retrieve(matCaptured);
            NotifyStatus("Retrieved in {0}", w.Elapsed.ToHumanReadable());


            var input = new ColourDetectorInput
            {
               Captured = matCaptured
               ,LowThreshold =new MCvScalar(lowH, lowS, lowV)
               ,HighThreshold = new MCvScalar(highH, highS, highV)
            };
            var output = _colorDetector.Process(input);

            if (output.IsDetected)
            {
                var radius = 50;
                var circle = new CircleF(output.CentralPoint, radius);
                var color = new Bgr(Color.Yellow);
                output.CapturedImage.Draw(circle, color, 3);
                var ballTextLocation = output.CentralPoint.ToPoint();
                ballTextLocation.X += radius;
                output.CapturedImage.Draw("ball", ballTextLocation, FontFace.HersheyPlain, 3, color);
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

            imageBoxCaptured.Image = output.CapturedImage;
            imageBoxFiltered.Image = output.ThresholdImage;
        }

        private void ColourDetectionControl_Load(object sender, EventArgs e)
        {
            sliderHueMin.Value = 0;
            sliderSaturationMin.Value = 0;
            sliderValueMin.Value = 0;

            sliderHueMax.Value = 255;
            sliderSaturationMax.Value = 255;
            sliderValueMax.Value = 255;

            // red ball preset
            sliderHueMin.Value = 140;
            sliderSaturationMin.Value = 57;
            sliderValueMin.Value = 25;

            sliderHueMax.Value = 187;
            sliderSaturationMax.Value = 153;
            sliderValueMax.Value = 82;
        }
    }
}
