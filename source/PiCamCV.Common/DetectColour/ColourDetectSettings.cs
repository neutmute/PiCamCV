using System;
using System.Drawing;
using Emgu.CV.Structure;
using PiCamCV.Common.ExtensionMethods;

namespace PiCamCV.Common
{

    public class TemporaryThresholdSettings : IDisposable
    {
        private ThresholdSettings _original;
        private ColourDetectorInput _target;

        public TemporaryThresholdSettings(ColourDetectorInput target, ThresholdSettings tempSettings)
        {
            _original = new ThresholdSettings();
            _original.Absorb(target.Settings);
            _target = target;
            target.Settings?.Absorb(tempSettings);
        }

        public void Dispose()
        {
            _target.Settings?.Absorb(_original);
        }
    }

    /// <summary>
    /// Easier to serialise
    /// </summary>
    public class ColourDetectSettings : ThresholdSettings
    {

        /// <summary>
        /// To trigger a detection this criteria must be met - minimum and maximum
        /// Too small: noise
        /// Too much: more than one object/noise
        /// </summary>
        public RangeF MomentArea { get; set; }

        /// <summary>
        /// Region of interest
        /// </summary>
        public Rectangle Roi { get; set; }

        public void Accept(ThresholdSettings settings)
        {
            HighThreshold = settings.HighThreshold;
            LowThreshold = settings.LowThreshold;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, MomentArea={MomentArea.ToStringE()}, Roi={Roi}";
        }
    }
}