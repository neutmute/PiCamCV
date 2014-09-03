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
            CvInvoke.CvtColor(matCaptured, hsvFrame, ColorConversion.Bgr2Gray);

            //var matThresholded = new Mat();
            //var lowH = sliderHueMin.Value;
            //var lowS = sliderSaturationMin.Value;
            //var lowV = sliderValueMin.Value;

            //var highH = sliderHueMax.Value;
            //var highS = sliderSaturationMax.Value;
            //var highV = sliderValueMax.Value;
                
            //var lowMScalar = new MCvScalar(lowH, lowS, lowV);
            //var highMScalar = new MCvScalar(highH, highS, highV);


        
            //using (var lowerScalar = new ScalarArray(lowMScalar))
            //{
            //    using (var upperScalar = new ScalarArray(highMScalar))
            //    {
            //     //   CvInvoke.InRange(hsvFrame, lowerScalar, upperScalar, matThresholded); //Threshold the image
            //    }
            //}

            //morphological opening (remove small objects from the foreground)
            //CvInvoke.Erode(matThresholded, matThresholded, getStructuringElement(MORPH_ELLIPSE, Size(5, 5)));
            //dilate(imgThresholded, imgThresholded, getStructuringElement(MORPH_ELLIPSE, Size(5, 5)));

            ////morphological closing (fill small holes in the foreground)
            //dilate(imgThresholded, imgThresholded, getStructuringElement(MORPH_ELLIPSE, Size(5, 5)));
            //erode(imgThresholded, imgThresholded, getStructuringElement(MORPH_ELLIPSE, Size(5, 5)));


            imageBoxCaptured.Image = matCaptured;
            imageBoxFiltered.Image = matThresholded;
        }

        private void ColourDetectionControl_Load(object sender, EventArgs e)
        {
            sliderHueMin.Value = 0;
            sliderSaturationMin.Value = 0;
            sliderValueMin.Value = 0;

            sliderHueMax.Value = 255;
            sliderSaturationMax.Value = 255;
            sliderValueMax.Value = 255;
        }
    }
}
