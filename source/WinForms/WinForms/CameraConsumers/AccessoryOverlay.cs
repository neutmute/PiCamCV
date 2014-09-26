using System.Drawing;
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;

namespace PiCamCV.WinForms.UserControls
{
    public class AccessoryOverlay
    {
        private Rectangle _calculatedRectangle;
        public Image<Bgra, byte> Overlay { get; private set; }
        public FileInfo File { get; set; }

        public Rectangle CalculatedRectangle
        {
            get { return _calculatedRectangle; }
            set
            {
                _calculatedRectangle = value;
                if (value != Rectangle.Empty)
                {
                    LastGoodRectangle = value;
                }
            }
        }

        public Rectangle LastGoodRectangle { get; set; }

        public Rectangle FinalRectangle
        {
            get
            {
                if (CalculatedRectangle == Rectangle.Empty)
                {
                    return LastGoodRectangle;
                }
                return CalculatedRectangle;
            }
            
        }

        public bool IsWearable { get; set; }

        public AccessoryOverlay(string filename)
        {
            Overlay = new Image<Bgra, byte>(filename);
        }
    }
}