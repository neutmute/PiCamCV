namespace PiCamCV
{
    public class CaptureConfig
    {
        public int Width { get;set; }
        public int Height { get;set; }
        public int Bitrate { get;set; }
        public int Framerate { get;set; }
        public bool Monochrome { get; set; }

        public override string ToString()
        {
            return string.Format(
                "w={0}, h={1}, bitrate={2}, framerate={3}, monochrome={4}"
                ,Width
                ,Height
                ,Bitrate
                ,Framerate
                ,Monochrome
                );
        }
    }
}