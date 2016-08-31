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

        public int RequiredMomentAreaInRoiPercent { get; set; } = 90;

        public int ErodeDilateIterations { get; set; } = 1;

        public ThresholdSelector()
        {
            _colourDetector = new ColourDetector();
        }

        public ThresholdSettings Select(Mat input, Rectangle targetRegion)
        {
            _input = input;
            _targetRegion = targetRegion;

            const int hueMax = 180;
            const int satValueMax = 255;
            
            _settings = ThresholdSettings.Get(0, 0, 0, hueMax, 255, 255);
            
            // Hue
            var lowV0 = GetDimensionResults(0, hueMax, false, (i, s) => s.WithV0(i));
            _settings.LowThreshold = _settings.LowThreshold.WithV0(lowV0);

            var highV0 = GetDimensionResults((int)_settings.LowThreshold.V0, hueMax, true, (i, s) => s.WithV0(i));
            _settings.HighThreshold = _settings.HighThreshold.WithV0(highV0);

            // Saturation
            var lowV1 = GetDimensionResults(0, satValueMax, false, (i, s) => s.WithV1(i));
            _settings.LowThreshold = _settings.LowThreshold.WithV1(lowV1);

            var highV1 = GetDimensionResults((int)_settings.LowThreshold.V1, satValueMax, true, (i, s) => s.WithV1(i));
            _settings.HighThreshold = _settings.HighThreshold.WithV1(highV1);

            // Value
            var lowV2 = GetDimensionResults(0, satValueMax, false, (i, s) => s.WithV2(i));
            _settings.LowThreshold = _settings.LowThreshold.WithV2(lowV2);

            var highV2 = GetDimensionResults((int)_settings.LowThreshold.V2, satValueMax, true, (i, s) => s.WithV2(i));
            _settings.HighThreshold = _settings.HighThreshold.WithV2(highV2);

            return _settings;
        }

        int GetDimensionResults(int dimensionMin, int dimensionMax, bool isHighSetting, Func<int, MCvScalar, MCvScalar> scalarUpdator)
        {
            var results = new List<AutoThresholdResult>();
            var requiredArea = _targetRegion.Area() * RequiredMomentAreaInRoiPercent / 100;
            Func<ColourDetectorOutput, bool> meetsMinimumAreaRequired = o => o.MomentArea >= requiredArea;
            
            for (int i = dimensionMin; i < dimensionMax; i++)
            {
                var detectorInput = new ColourDetectorInput();
                detectorInput.Captured = _input;
                detectorInput.ErodeDilateIterations = ErodeDilateIterations;
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

                detectorInput.Settings.Roi = _targetRegion;
                tickResult.RoiOutput = _colourDetector.Process(detectorInput);

                // Save processing the full screen if it doesn't meet minimum ROI requirements
                if (meetsMinimumAreaRequired(tickResult.RoiOutput))
                {
                    detectorInput.Settings.Roi = Rectangle.Empty;
                    tickResult.FullOutput = _colourDetector.Process(detectorInput);

                    results.Add(tickResult);
                }

                ColourCheckTick?.Invoke(this, tickResult);
                Intercept?.Invoke(detectorInput);
            }
            
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
