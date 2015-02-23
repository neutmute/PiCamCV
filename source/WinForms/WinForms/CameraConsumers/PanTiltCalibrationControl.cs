using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using PiCamCV.Common.ExtensionMethods;
using PiCamCV.Interfaces;
using PiCamCV.WinForms.ExtensionMethods;

namespace PiCamCV.WinForms.CameraConsumers
{
    public partial class PanTiltCalibrationControl : CameraConsumerUserControl
    {
        public Point? Reticle { get; set; }

        public PanTiltCalibrationControl()
        {
            InitializeComponent();
            Reticle = null;
        }

        private void btnGoto_Click(object sender, EventArgs e)
        {

        }

        protected override void OnSubscribe()
        {
            var center = CameraCapture.GetCaptureProperties().GetCenter();
            
            txtReticleX.Text = center.X.ToString();
            txtReticleY.Text = center.Y.ToString();
        }

        public override void ImageGrabbedHandler(object sender, EventArgs e)
        {
            using (var matCaptured = new Mat())
            {
                CameraCapture.Retrieve(matCaptured);

                var bgraImage = matCaptured.ToImage<Bgra, byte>();
                var captureConfig = CameraCapture.GetCaptureProperties();
                var centre = captureConfig.GetCenter();

                DrawReticle(bgraImage, centre, Color.Red);

                if (Reticle != null)
                {
                    DrawReticle(bgraImage, Reticle.Value, Color.Green);    
                }

                imageBoxCaptured.Image = bgraImage;
            }
        }

        private void DrawReticle(Image<Bgra, byte> image, Point center, Color colorIn)
        {
            var reticleRadius = 50;
            var color = colorIn.ToBgra();
            var topVert = new Point(center.X, center.Y - reticleRadius);
            var bottomVert = new Point(center.X, center.Y + reticleRadius);

            var leftHoriz = new Point(center.X - reticleRadius, center.Y);
            var rightHoriz = new Point(center.X + reticleRadius, center.Y);

            var horizontalLine = new LineSegment2D(topVert, bottomVert);
            var verticalLine = new LineSegment2D(leftHoriz, rightHoriz);

            image.Draw(horizontalLine, color, 1);
            image.Draw(verticalLine  , color, 1);
        }

        private void btnPaintReticle_Click(object sender, EventArgs e)
        {
            int xCoord, yCoord;

            var xOK = int.TryParse(txtReticleX.Text, out xCoord);
            var yOK = int.TryParse(txtReticleY.Text, out yCoord);

            if (xOK && yOK)
            {
                Reticle = new Point(xCoord, yCoord);
            }
            else
            {
                Reticle = null;
            }
        }
    }
}
