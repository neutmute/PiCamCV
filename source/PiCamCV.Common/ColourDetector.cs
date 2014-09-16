using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using PiCamCV.Common.Interfaces;
using PiCamCV.Interfaces;

namespace PiCamCV.Common
{
    /// <summary>
    /// Easier to serialise
    /// </summary>
    public class ColourDetectSettings
    {
        public MCvScalar LowThreshold { get; set; }
        public MCvScalar HighThreshold { get; set; }

        public int MinimumDetectionArea { get; set; }
        /// <summary>
        /// Region of interest
        /// </summary>
        public Rectangle Roi { get; set; }

        public override string ToString()
        {
            return string.Format("Low={0}, High={1}, MinimumDetectionArea={2}, Roi={3}", LowThreshold, HighThreshold, MinimumDetectionArea, Roi);
        }
    }

    public class ColourDetectorInput : CameraProcessInput
    {
        public ColourDetectSettings Settings { get; set; }

        /// <summary>
        /// Turn off for console perf tweak
        /// </summary>
        public bool SetCapturedImage { get; set; }

        public ColourDetectorInput()
        {
            SetCapturedImage = true;
        }
    }

    public class ColourDetectorProcessOutput : CameraProcessOutput
    {
        public Image<Bgr, byte> CapturedImage { get;  set; }

        public Image<Gray, byte> ThresholdImage { get;  set; }

        public PointF CentralPoint { get; set; }
        public bool IsDetected { get; set; }

        public override string ToString()
        {
            return string.Format("IsDetected={0}, CentralPoint={1}", IsDetected, CentralPoint);
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
                const int erodeDilateIterations = 1;

                //morphological opening (remove small objects from the foreground)
                output.ThresholdImage.Erode(erodeDilateIterations);
                output.ThresholdImage.Dilate(erodeDilateIterations);

                //morphological closing (fill small holes in the foreground)
                output.ThresholdImage.Dilate(erodeDilateIterations);
                output.ThresholdImage.Erode(erodeDilateIterations);
                
                var moments = output.ThresholdImage.GetMoments(true);
                moments.GetCentralMoment(0, 0);

                var area = moments.M00;
                if (area > settings.MinimumDetectionArea)
                {
                    var posX = Convert.ToSingle(moments.M10/area);
                    var posY = Convert.ToSingle(moments.M01/area);
                    output.IsDetected = true;

                    if (!settings.Roi.IsEmpty)
                    {
                        // transpose the detected coordinates to non ROI space
                        posX += settings.Roi.X;
                        posY += settings.Roi.Y;
                    }

                    output.CentralPoint = new PointF(posX, posY);
                }

                if (input.SetCapturedImage)
                {
                    output.CapturedImage = input.Captured.ToImage<Bgr, byte>();
                }
            }
            return output;
        }
    }
}
