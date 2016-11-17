using Emgu.CV.Structure;
using PiCamCV.ExtensionMethods;

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
            if (s == null)
            {
                return;
            }
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
}