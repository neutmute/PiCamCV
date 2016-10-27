namespace PiCamCV.Common
{
    public class CaptureConfig
    {
        public Resolution Resolution { get; set; }
        public int Bitrate { get;set; }
        public int Framerate { get;set; }
        public bool Monochrome { get; set; }

        public CaptureConfig()
        {
            Resolution = new Resolution();
        }

        public override string ToString()
        {
            return $"res={Resolution}, bitrate={Bitrate}, framerate={Framerate}, monochrome={Monochrome}";
        }
    }
}