using System.Drawing;

namespace PiCamCV.Common
{
    public class Resolution
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public bool IsValid => Width > 0 && Height > 0;

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
            return $"{Width}x{Height}";
        }

        public Point GetCenter()
        {
            return new Point(Width / 2, Height / 2);
        }
    }
}