using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Kraken.Core;
using PiCamCV.Common;

namespace PiCamCV.WinForms.CameraConsumers
{
    public partial class MotionDetectionControl : CameraConsumerUserControl
    {
        private MotionDetector _motionDetector;
        public MotionDetectionControl()
        {
            InitializeComponent();
            
        }

        public override void ImageGrabbedHandler(object sender, EventArgs e)
        {
            using (var frame = new Mat())
            {
                CameraCapture.Retrieve(frame);
                var input = new MotionDetectorInput();
                input.MinimumArea = sliderMinimumArea.Value;
                var inputImage = frame.ToImage<Bgr,byte>();
                input.Captured = frame;

                var output = _motionDetector.Process(input);

                var bgrRed = new Bgr(Color.Red);

                foreach (var motionRegion in output.MotionSections)
                {
                    inputImage.Draw(motionRegion.Region, bgrRed);
                    DrawMotion(output.MotionImage, motionRegion.Region, motionRegion.Angle, bgrRed);
                }

                DrawMotion(output.MotionImage, new Rectangle(Point.Empty, output.MotionImage.Size), output.OverallAngle,
                    new Bgr(Color.Green));

                imageBoxCaptured.Image = inputImage;
                imageBoxMasked.Image = output.ForegroundImage;
                imageBoxMotion.Image = output.MotionImage;

                NotifyStatus(
                    "Motion detection took {0}. {1} motions, {2} over all pixel count"
                    , output.Elapsed.ToHumanReadable()
                    , output.MotionSections.Count
                    , output.OverallMotionPixelCount);
            }
        }

        private static void DrawMotion(IInputOutputArray image, Rectangle motionRegion, double angle, Bgr color)
        {
            float circleRadius = (motionRegion.Width + motionRegion.Height) >> 2;
            var center = new Point(motionRegion.X + (motionRegion.Width >> 1), motionRegion.Y + (motionRegion.Height >> 1));

            var circle = new CircleF(
               center,
               circleRadius);

            int xDirection = (int)(Math.Cos(angle * (Math.PI / 180.0)) * circleRadius);
            int yDirection = (int)(Math.Sin(angle * (Math.PI / 180.0)) * circleRadius);
            var pointOnCircle = new Point(
                center.X + xDirection,
                center.Y - yDirection);
            var line = new LineSegment2D(center, pointOnCircle);

            CvInvoke.Circle(image, Point.Round(circle.Center), (int)circle.Radius, color.MCvScalar);
            CvInvoke.Line(image, line.P1, line.P2, color.MCvScalar);
        }

        private void MotionDetectionControl_Load(object sender, EventArgs e)
        {
            _motionDetector = new MotionDetector();
        }

        private void sliderControl1_ValueChanged(object sender, EventArgs e)
        {
            var defaultSize = new Size(320, 240);
            var newWidth = sliderSize.Value / 100 * defaultSize.Width;
            var newHeight = sliderSize.Value / 100 * defaultSize.Height;

            var newSize = new Size(newWidth, newHeight);
            groupBoxCaptured.Size = newSize;
            groupBoxMasked.Size = newSize;
            groupBoxMotion.Size = newSize;
        }

    }
}
