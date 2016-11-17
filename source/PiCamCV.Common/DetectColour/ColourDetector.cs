using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using PiCamCV.Common.ExtensionMethods;
using PiCamCV.Common.Interfaces;
using PiCamCV.Interfaces;

namespace PiCamCV.Common
{
    /// <summary>
    /// Based off http://opencv-srf.blogspot.com.au/2010/09/object-detection-using-color-seperation.html
    /// </summary>
    public class ColourDetector : CameraProcessor<ColourDetectorInput, ColourDetectorOutput>
    {
        protected override ColourDetectorOutput DoProcess(ColourDetectorInput input)
        {
            var output = new ColourDetectorOutput();
            using(var hsvFrame = new Mat())
            using(var matThresholded = new Mat())
            {
                var inputMat = input.Captured;

                var settings = input.Settings;

                if (settings == null)
                {
                    return output;
                }

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
