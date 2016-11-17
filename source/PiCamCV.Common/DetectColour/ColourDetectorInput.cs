using PiCamCV.Common.Interfaces;

namespace PiCamCV.Common
{
    public class ColourDetectorInput : CameraProcessInput
    {
        public ColourDetectSettings Settings { get; set; }

        public int ErodeDilateIterations { get; set; }
        
        public ColourDetectorInput()
        {
            Settings = new ColourDetectSettings();
            ErodeDilateIterations = 1;
        }

        public override string ToString()
        {
            return Settings.ToString();
        }
    }
}