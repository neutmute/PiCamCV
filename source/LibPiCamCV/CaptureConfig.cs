using System.Drawing;

namespace PiCamCV
{
    public class Resolution
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public bool IsValid { get {return Width > 0 && Height > 0; } }

        public Resolution()
        {
            
        }

        public Resolution(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override int GetHashCode()
        {
            return Width * 10000 + Height;
        }

        public override bool Equals(object obj)
        {
            var otherAsResolution = obj as Resolution;
            return otherAsResolution.GetHashCode() == GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0}x{1}", Width, Height);
        }

        public Point GetCenter()
        {
            return new Point(Width / 2, Height / 2);
        }
    }

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
            return string.Format(
                "res={0}, bitrate={1}, framerate={2}, monochrome={3}"
                ,Resolution
                ,Bitrate
                ,Framerate
                ,Monochrome
                );
        }
    }
}