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
    public class ThresholdSelector
    {
        private ThresholdSettings _settings;
        private ColourDetector _colourDetector;
        private Rectangle _targetRegion;
        private Mat _input;

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
            _settings.LowThreshold = _settings.HighThreshold.WithV0(lowV0);

            var highV0 = GetDimensionResults((int)_settings.LowThreshold.V0, hueMax, true, (i, s) => s.WithV0(i));
            _settings.HighThreshold = _settings.HighThreshold.WithV0(highV0);
            
            return _settings;
        }

        int GetDimensionResults(int dimensionMin, int dimensionMax, bool isHighSetting, Func<int, MCvScalar, MCvScalar> scalarUpdator)
        {
            var results = new Dictionary<int, ColourDetectorProcessOutput>();

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

                var detectorOutput = _colourDetector.Process(detectorInput);
                if (_targetRegion.Contains(detectorOutput.CentralPoint.ToPoint()))
                {
                    results.Add(i, detectorOutput);
                }
            }

            var result = results
                            .Where(r => r.Value.MomentArea <= _targetRegion.Area())
                            .OrderByDescending(r => r.Value.MomentArea)
                            .Select(r => r.Key)
                            .FirstOrDefault();

            return result;
        }
    }
}
