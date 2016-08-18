using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Kraken.Core;
using PiCamCV.Common.ExtensionMethods;
using PiCamCV.Common.Interfaces;
using PiCamCV.ExtensionMethods;
using PiCamCV.Interfaces;

namespace PiCamCV.Common
{
    public class ThresholdSettings
    {
        public MCvScalar LowThreshold { get; set; }
        public MCvScalar HighThreshold { get; set; }

        public static ThresholdSettings Get(double lowHue, double lowSat, double lowValue, double highHue, double highSat, double highValue)
        {
            var s = new ThresholdSettings();
            s.LowThreshold = GetScalar(lowHue, lowSat, lowValue);
            s.HighThreshold = GetScalar(highHue, highSat, highValue);
            return s;
        }

        public void Absorb(ThresholdSettings s)
        {
            LowThreshold = s.LowThreshold;
            HighThreshold = s.HighThreshold;
        }
        
        private static MCvScalar GetScalar(double hue, double sat, double value)
        {
            var scalar = new MCvScalar();
            scalar.V0 = hue;
            scalar.V1 = sat;
            scalar.V2 = value;
            return scalar;
        }

        public override string ToString()
        {
            return $"Low={LowThreshold.ToStringE()}, High={HighThreshold.ToStringE()}";
        }
    }

    /// <summary>
    /// Easier to serialise
    /// </summary>
    public class ColourDetectSettings : ThresholdSettings
    {

        /// <summary>
        /// To trigger a detection this criteria must be met.
        /// Too small: noise
        /// Too much: more than one object/noise
        /// </summary>
        public RangeF MomentArea { get; set; }

        /// <summary>
        /// Region of interest
        /// </summary>
        public Rectangle Roi { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}, MomentArea={MomentArea.ToStringE()}, Roi={Roi}";
        }
    }

    public class ColourDetectorInput : CameraProcessInput
    {
        public ColourDetectSettings Settings { get; set; }

        public int ErodeDilateIterations { get; set; }
        
        public ColourDetectorInput()
        {
            Settings = new ColourDetectSettings();
            ErodeDilateIterations = 1;
        }
    }

    public class ColourDetectorProcessOutput : CameraProcessOutput
    {
        /// <summary>
        /// The image that has had the filters applied before moments detected
        /// </summary>
        public Image<Gray, byte> ThresholdImage { get;  set; }

        public bool IsDetected { get; set; }

        public PointF CentralPoint { get; set; }

        public double MomentArea { get; set; }
        
        public override string ToString()
        {
            return string.Format(
                "Processed colour in {3}, IsDetected={0}, MomentArea={2}, CentralPoint={1}"
                , IsDetected
                , CentralPoint
                , MomentArea
                , Elapsed.ToHumanReadable(HumanReadableTimeSpanOptions.Abbreviated));
        }
    }

    /// <summary>
    /// Based off http://opencv-srf.blogspot.com.au/2010/09/object-detection-using-color-seperation.html
    /// </summary>
    public class ColourDetector : CameraProcessor<ColourDetectorInput, ColourDetectorProcessOutput>
    {
        protected override ColourDetectorProcessOutput DoProcess(ColourDetectorInput input)
        {
            var output = new ColourDetectorProcessOutput();
            using(var hsvFrame = new Mat())
            using(var matThresholded = new Mat())
            {
                var inputMat = input.Captured;

                var settings = input.Settings;

                if (!settings.Roi.IsEmpty)
                {
                    inputMat = new Mat(inputMat, settings.Roi);
                }

                CvInvoke.CvtColor(inputMat, hsvFrame, ColorConversion.Bgr2Hsv);

                using (var lowerScalar = new ScalarArray(settings.LowThreshold))
                using (var upperScalar = new ScalarArray(settings.HighThreshold))
                {
                    CvInvoke.InRange(hsvFrame, lowerScalar, upperScalar, matThresholded); //Threshold the image
                }

                output.ThresholdImage = matThresholded.ToImage<Gray, byte>();

                if (input.ErodeDilateIterations > 0)
                {
                    //morphological opening (remove small objects from the foreground)
                    output.ThresholdImage = output.ThresholdImage.Erode(input.ErodeDilateIterations);
                    output.ThresholdImage = output.ThresholdImage.Dilate(input.ErodeDilateIterations);

                    //morphological closing (fill small holes in the foreground)
                    output.ThresholdImage = output.ThresholdImage.Dilate(input.ErodeDilateIterations);
                    output.ThresholdImage = output.ThresholdImage.Erode(input.ErodeDilateIterations);
                }

                var moments = output.ThresholdImage.GetMoments(true);
                //moments.GetCentralMoment(0, 0);

                output.MomentArea = moments.M00;
                if (settings.MomentArea.IsInRange(output.MomentArea))
                {
                    output.IsDetected = true;
                }

                var posX = Convert.ToSingle(moments.M10 / output.MomentArea);
                var posY = Convert.ToSingle(moments.M01 / output.MomentArea);

                if (!settings.Roi.IsEmpty)
                    {
                        // transpose the detected coordinates to non ROI space
                        posX += settings.Roi.X;
                        posY += settings.Roi.Y;
                    }

                    output.CentralPoint = new PointF(posX, posY);
            }
            return output;
        }
    }
}
