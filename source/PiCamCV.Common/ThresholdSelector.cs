using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.CvEnum;

namespace PiCamCV.Common
{
    public class ThresholdSelector
    {
        private ThresholdSettings _settings;

        public ThresholdSettings Select(Mat input, Rectangle targetRegion)
        {
            const int hueMax = 180;
            _settings = ThresholdSettings.Get(0, 0, 0, hueMax, 255, 255);

            using (var hsvFrame = new Mat())
            using (var matThresholded = new Mat())
            {
                CvInvoke.CvtColor(input, hsvFrame, ColorConversion.Bgr2Hsv);
                //H Threshold
                for (int i = 0; i < hueMax; i++)
                {
                    using (var lowerScalar = new ScalarArray(_settings.LowThreshold))
                    using (var upperScalar = new ScalarArray(_settings.HighThreshold))
                    {
                        CvInvoke.InRange(hsvFrame, lowerScalar, upperScalar, matThresholded); //Threshold the image
                    }
                }
            }

            return _settings;
        }
    }
}
