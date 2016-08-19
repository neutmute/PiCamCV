using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV.UI;

namespace PiCamCV.WinForms.CameraConsumers.Base
{
    public class ImageBoxSelector
    {
        private ImageBox _imageBox;
        private Point _mouseDownLocation;
        public Rectangle SeedingRectangle { get; private set; }
        private Rectangle _readyRectangle;

        public event EventHandler<Rectangle> SelectionMade;
        public event EventHandler<Rectangle> SeedingChange;

        public void ConfigureBoxSelections(ImageBox imageBox)
        {
            imageBox.FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
            imageBox.MouseDown += imageBoxTracking_MouseDown;
            imageBox.MouseUp += imageBoxTracking_MouseUp;
            imageBox.MouseMove += imageBoxTracking_MouseMove;

            _imageBox = imageBox;
        }

        private void imageBoxTracking_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _mouseDownLocation = e.Location;
            }
        }

        private void imageBoxTracking_MouseMove(object sender, MouseEventArgs e)
        {
            if (_mouseDownLocation != Point.Empty)
            {
                SeedingRectangle = _imageBox.GetRectangle(_mouseDownLocation, e.Location);
                if (SeedingChange != null)
                {
                    SeedingChange(this, SeedingRectangle);
                }
            }
        }

        private void imageBoxTracking_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _readyRectangle = _imageBox.GetRectangle(_mouseDownLocation, e.Location);

                _mouseDownLocation = Point.Empty;
                SeedingRectangle = Rectangle.Empty;

                if (SelectionMade != null)
                {
                    SelectionMade(this, _readyRectangle);
                    _readyRectangle = Rectangle.Empty;
                }
                
            }
        }
    }
}
