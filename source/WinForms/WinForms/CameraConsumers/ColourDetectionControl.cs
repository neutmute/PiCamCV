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
using PiCamCV.WinForms.ExtensionMethods;

namespace PiCamCV.WinForms.CameraConsumers
{
    public partial class ColourDetectionControl : CameraConsumerUserControl
    {
        private readonly ColourDetector _colorDetector;
        private MCvScalar _lowThreshold;
        private MCvScalar _highThreshold;
        private bool _suppressUpdates;
        private bool _captureResized;

        public ColourDetectionControl()
        {
            InitializeComponent();
            _colorDetector = new ColourDetector();
            _lowThreshold = new MCvScalar();
            _highThreshold = new MCvScalar();
        }

        public override void ImageGrabbedHandler(object sender, EventArgs e)
        {
            using (var matCaptured = new Mat())
            {
                var roiRectangle = Rectangle.Empty;

                var w = Stopwatch.StartNew();
                CameraCapture.Retrieve(matCaptured);
                NotifyStatus("Retrieved frame in {0}", w.Elapsed.ToHumanReadable());

                ResizeImageControls(matCaptured);

                if (checkBoxRoi.Checked)
                {
                    // transpose top/bottom to make ui up/down more intuitive
                    var imageHeight = matCaptured.Height;
                    var top = imageHeight - sliderRoiTop.Value;
                    var bottom = imageHeight - sliderRoiBottom.Value;

                    var width = (sliderRoiRight.Value - sliderRoiLeft.Value);
                    var height = bottom - top;

                    roiRectangle = new Rectangle(
                        sliderRoiLeft.Value
                        , top
                        , width
                        , height
                        );
                }

                var input = new ColourDetectorInput
                {
                    Captured = matCaptured
                    ,LowThreshold = _lowThreshold
                    ,HighThreshold = _highThreshold
                    ,RegionOfInterest = roiRectangle
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

                if (checkBoxRoi.Checked)
                {
                    output.CapturedImage.Draw(roiRectangle, Color.Green.ToBgr());
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
        }

        private void ResizeImageControls(Mat matCaptured)
        {
            if (!_captureResized)
            {
                _captureResized = true;

                var newSize = new Size(matCaptured.Width, matCaptured.Height);
                InvokeUI(() =>
                {
                    groupBoxCaptured.Size = newSize;
                    groupBoxFiltered.Size = newSize;

                    sliderRoiLeft.Maximum = matCaptured.Width;
                    sliderRoiRight.Maximum = matCaptured.Width;
                    
                    sliderRoiTop.Maximum = matCaptured.Height;
                    sliderRoiBottom.Maximum = matCaptured.Height;

                    sliderRoiRight.Value = matCaptured.Width;
                    sliderRoiTop.Value = matCaptured.Height;
                });
            }
        }

        private void ColourDetectionControl_Load(object sender, EventArgs e)
        {
            sliderHueMin.ValueChanged += HsvSlider_ValueChanged;
            sliderHueMax.ValueChanged += HsvSlider_ValueChanged;
            sliderSaturationMin.ValueChanged += HsvSlider_ValueChanged;
            sliderSaturationMax.ValueChanged += HsvSlider_ValueChanged;
            sliderValueMin.ValueChanged += HsvSlider_ValueChanged;
            sliderValueMin.ValueChanged += HsvSlider_ValueChanged;

            btnReset_Click(null, null);
        }

        void HsvSlider_ValueChanged(object sender, EventArgs e)
        {
            UpdateScalarFromControls();
        }

        private void SetLowScalar(int hue, int sat, int value)
        {
            _lowThreshold.V0 = hue;
            _lowThreshold.V1 = sat;
            _lowThreshold.V2 = value;
            UpdateControlsFromScalar();
        }
        private void SetHighScalar(int hue, int sat, int value)
        {
            _highThreshold.V0 = hue;
            _highThreshold.V1 = sat;
            _highThreshold.V2 = value;
            UpdateControlsFromScalar();
        }

        private void UpdateControlsFromScalar()
        {
            _suppressUpdates = true;
            sliderHueMin.Value = (int) _lowThreshold.V0;
            sliderSaturationMin.Value = (int)_lowThreshold.V1;
            sliderValueMin.Value = (int)_lowThreshold.V2;

            sliderHueMax.Value = (int)_highThreshold.V0;
            sliderSaturationMax.Value = (int)_highThreshold.V1;
            sliderValueMax.Value = (int)_highThreshold.V0;
            _suppressUpdates = false;
        }

        private void UpdateScalarFromControls()
        {
            if (!_suppressUpdates)
            {
                var lowH = sliderHueMin.Value;
                var lowS = sliderSaturationMin.Value;
                var lowV = sliderValueMin.Value;

                var highH = sliderHueMax.Value;
                var highS = sliderSaturationMax.Value;
                var highV = sliderValueMax.Value;

                SetLowScalar(lowH, lowS, lowV);
                SetHighScalar(highH, highS, highV);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            SetLowScalar(0, 0, 0);
            SetHighScalar(255, 255, 255);
        }

        /// <summary>
        /// Under CFL lights
        /// </summary>
        private void btnRedLights_Click(object sender, EventArgs e)
        {
            SetLowScalar(140, 57, 25);
            SetHighScalar(187,153,182);
        }

        /// <summary>
        /// -t=155,128,44|182,214,105
        /// </summary>
        private void btnRedDaylight_Click(object sender, EventArgs e)
        {
            SetLowScalar(155, 128, 44);
            SetHighScalar(182, 214, 105);
        }

        private void checkBoxRoiEnabled_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
