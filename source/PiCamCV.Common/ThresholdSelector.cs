using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using PiCamCV.Common.ExtensionMethods;

namespace PiCamCV.Common
{
    public class AutoThresholdResult
    {
        public int DimensionValue { get; set; }

        public ColourDetectorOutput FullOutput { get; set; }

        public ColourDetectorOutput RoiOutput { get; set; }
    }

    public class ThresholdSelector
    {
        private ThresholdSettings _settings;
        private ColourDetector _colourDetector;
        private Rectangle _targetRegion;
        private Mat _input;

        public event EventHandler<AutoThresholdResult> ColourCheckTick;
        public Action<ColourDetectorInput> Intercept { get; set; }

        public ThresholdSelector()
        {
            _colourDetector = new ColourDetector();
        }

        public ThresholdSettings Select(Mat input, Rectangle targetRegion)
        {
            _input = input;
            _targetRegion = targetRegion;

            const int hueMax = 180;
            
            _settings = ThresholdSettings.Get(0, 0, 0, hueMax, 255, 255);
            
            var lowV0 = GetDimensionResults(0, hueMax, false, (i, s) => s.WithV0(i));
            _settings.LowThreshold = _settings.LowThreshold.WithV0(lowV0);

            var highV0 = GetDimensionResults((int)_settings.LowThreshold.V0, hueMax, true, (i, s) => s.WithV0(i));
            _settings.HighThreshold = _settings.HighThreshold.WithV0(highV0);

            return _settings;
        }

        int GetDimensionResults(int dimensionMin, int dimensionMax, bool isHighSetting, Func<int, MCvScalar, MCvScalar> scalarUpdator)
        {
            var results = new List<AutoThresholdResult>();

            for (int i = dimensionMin; i < dimensionMax; i++)
            {
                var detectorInput = new ColourDetectorInput();
                detectorInput.Captured = _input;
                detectorInput.Settings.Absorb(_settings);
                detectorInput.Settings.MomentArea = new RangeF(0, float.MaxValue);

                if (isHighSetting)
                {
                    detectorInput.Settings.HighThreshold = scalarUpdator(i, detectorInput.Settings.HighThreshold);
                }
                else
                {
                    detectorInput.Settings.LowThreshold = scalarUpdator(i, detectorInput.Settings.LowThreshold);
                }
                
                var tickResult = new AutoThresholdResult();
                tickResult.DimensionValue = i;
                tickResult.FullOutput = _colourDetector.Process(detectorInput);

                detectorInput.Settings.Roi = _targetRegion;
                tickResult.RoiOutput = _colourDetector.Process(detectorInput);
                
                results.Add(tickResult);
                
                ColourCheckTick?.Invoke(this, tickResult);
                Intercept?.Invoke(detectorInput);
            }

            const int percentMomentAreaInRoi = 90;
            var requiredArea = _targetRegion.Area()*percentMomentAreaInRoi/100;

            // Remove any where region of interest isn't highlighted
            results.RemoveAll(r =>r.RoiOutput.MomentArea < requiredArea);

            // Pick the smallest thresholded area left in full screen
            var result = results
                            .OrderBy(r => r.FullOutput.MomentArea)
                            .FirstOrDefault();

            if (result == null)
            {
                return isHighSetting ? dimensionMax : dimensionMin;
            }

            return result.DimensionValue;
        }
    }
}
