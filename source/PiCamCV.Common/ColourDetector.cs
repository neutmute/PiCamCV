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
    public class ColourDetectorInput : CameraProcessInput
    {
        public MCvScalar LowThreshold { get; set; }
        public MCvScalar HighThreshold { get; set; }

        public int MinimumDetectionArea { get; set; }
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
            using (var matThresholded = new Mat())
            {
                CvInvoke.CvtColor(input.Captured, hsvFrame, ColorConversion.Bgr2Hsv);

                using (var lowerScalar = new ScalarArray(input.LowThreshold))
                {
                    using (var upperScalar = new ScalarArray(input.HighThreshold))
                    {
                        CvInvoke.InRange(hsvFrame, lowerScalar, upperScalar, matThresholded); //Threshold the image
                    }
                }

                output.ThresholdImage = matThresholded.ToImage<Gray, byte>();
                const int erodeDilateIterations = 10;

                //morphological opening (remove small objects from the foreground)
                output.ThresholdImage.Erode(erodeDilateIterations);
                output.ThresholdImage.Dilate(erodeDilateIterations);

                //morphological closing (fill small holes in the foreground)
                output.ThresholdImage.Dilate(erodeDilateIterations);
                output.ThresholdImage.Erode(erodeDilateIterations);

                output.CapturedImage = input.Captured.ToImage<Bgr, byte>();
                var moments = output.ThresholdImage.GetMoments(true);
                moments.GetCentralMoment(0, 0);

                var area = moments.M00;
                if (area > input.MinimumDetectionArea)
                {
                    var posX = Convert.ToInt32(moments.M10/area);
                    var posY = Convert.ToInt32(moments.M01/area);
                    output.IsDetected = true;
                    output.CentralPoint = new PointF(posX, posY);
                }
            }
            return output;
        }
    }
}
