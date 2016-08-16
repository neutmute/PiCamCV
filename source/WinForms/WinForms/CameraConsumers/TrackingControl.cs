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

namespace PiCamCV.WinForms.CameraConsumers
{
    public partial class TrackingControl : CameraConsumerUserControl
    {
        public TrackingControl()
        {
            InitializeComponent();
        }
        public override void ImageGrabbedHandler(object sender, EventArgs e)
        {
            using (var frame = new Mat())
            {
                //CameraCapture.Retrieve(frame);
                //var input = new MotionDetectorInput();

                //var inputImage = frame.ToImage<Bgr, byte>();
                //input.Captured = frame;
                //input.Settings = _currentSettings;

                //var output = _motionDetector.Process(input);

                //var bgrRed = new Bgr(Color.Red);
                //var bgrBlue = new Bgr(Color.Blue);

                //foreach (var motionRegion in output.MotionSections)
                //{
                //    var text = string.Format("A={0}, M={1}", motionRegion.Area, motionRegion.PixelsInMotionCount);
                //    inputImage.Draw(motionRegion.Region, bgrRed);
                //    if (chkRectangleStats.Checked)
                //    {
                //        inputImage.Draw(text, motionRegion.Region.Location, Emgu.CV.CvEnum.FontFace.HersheyComplexSmall, .8, bgrRed);
                //    }
                //    DrawMotion(output.MotionImage, motionRegion.Region, motionRegion.Angle, bgrRed);
                //}

                //DrawMotion(output.MotionImage, new Rectangle(Point.Empty, output.MotionImage.Size), output.OverallAngle, new Bgr(Color.Green));

                //if (output.BiggestMotion != null)
                //{
                //    var motion = output.BiggestMotion;
                //    inputImage.Draw(motion.Region, bgrBlue);
                //}

                //imageBoxCaptured.Image = inputImage;
                //imageBoxMasked.Image = output.ForegroundImage;
                //imageBoxMotion.Image = output.MotionImage;

                //NotifyStatus(
                //    "Motion detection took {0}. {1} motions, {2} over all pixel count"
                //    , output.Elapsed.ToHumanReadable()
                //    , output.MotionSections.Count
                //    , output.OverallMotionPixelCount);
            }
        }


    }
}
