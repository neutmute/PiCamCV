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
using PiCamCV.Common.Repositories;

namespace PiCamCV.WinForms.CameraConsumers
{
    public partial class MotionDetectionControl : CameraConsumerUserControl
    {
        private readonly IMotionDetectSettingsRepository _motionSettingsRepo;
        private MotionDetectSettings _currentSettings;
        private MotionDetector _motionDetector;

        public MotionDetectionControl()
        {
            InitializeComponent();
            _motionSettingsRepo = new MotionDetectSettingRepository();
        }

        private void MotionDetectionControl_Load(object sender, EventArgs e)
        {
            _currentSettings = new MotionDetectSettings();
            _motionDetector = new MotionDetector();
            SetUIFromSubtractorConfig();

            ddlBiggestTargeting.SelectedText = BiggestMotionType.Area.ToString();
        }

        public MotionDetectSettings HarvestSettingsFromUI()
        {
            var settings = new MotionDetectSettings();
            
            var minArea = sliderMinimumArea.Value * sliderMinimumArea.Value;
            var maxArea = sliderMaximumArea.Value * sliderMaximumArea.Value;
            settings.MinimumArea = minArea;
            settings.MaximumArea = maxArea;
            settings.MinimumPercentMotionInArea = ((decimal)sliderMinimumPercentMotion.Value) / 100;
            
            settings.SubtractorConfig.History = Convert.ToInt32(txtBoxHistory.Text);
            settings.SubtractorConfig.Threshold = Convert.ToInt32(txtBoxThreshold.Text);
            settings.SubtractorConfig.ShadowDetection = chkBoxShadowDetection.Checked;

            if (!string.IsNullOrEmpty(ddlBiggestTargeting.SelectedText))
            {
                settings.BiggestMotionType = Enumeration.FromValue<BiggestMotionType>(ddlBiggestTargeting.SelectedText);
            }

            return settings;
        }

        public override void ImageGrabbedHandler(object sender, EventArgs e)
        {
            using (var frame = new Mat())
            {
                CameraCapture.Retrieve(frame);
                var input = new MotionDetectorInput();

                var inputImage = frame.ToImage<Bgr,byte>();
                input.Captured = frame;
                input.Settings = _currentSettings;

                var output = _motionDetector.Process(input);

                var bgrRed = new Bgr(Color.Red);
                var bgrBlue = new Bgr(Color.Blue);

                foreach (var motionRegion in output.MotionSections)
                {
                    var text = string.Format("A={0}, M={1}", motionRegion.Area, motionRegion.PixelsInMotionCount);
                    inputImage.Draw(motionRegion.Region, bgrRed);
                    if (chkRectangleStats.Checked)
                    {
                        inputImage.Draw(text, motionRegion.Region.Location, Emgu.CV.CvEnum.FontFace.HersheyComplexSmall, .8, bgrRed);
                    }
                    DrawMotion(output.MotionImage, motionRegion.Region, motionRegion.Angle, bgrRed);
                }

                DrawMotion(output.MotionImage, new Rectangle(Point.Empty, output.MotionImage.Size), output.OverallAngle, new Bgr(Color.Green));

                if (output.BiggestMotion != null)
                {
                    var motion = output.BiggestMotion;
                    inputImage.Draw(motion.Region, bgrBlue);
                }

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


        private void sliderControl1_ValueChanged(object sender, EventArgs e)
        {
            var defaultSize = new Size(320, 240);
            var newWidth = (sliderSize.Value  * defaultSize.Width) / 100;
            var newHeight = (sliderSize.Value * defaultSize.Height) / 100;

            var newSize = new Size(newWidth, newHeight);
            groupBoxCaptured.Size = newSize;
            groupBoxMasked.Size = newSize;
            groupBoxMotion.Size = newSize;
        }

        private void SetUIFromAreaConfig()
        {
            sliderMinimumArea.Value =  Convert.ToInt32(Math.Sqrt(_currentSettings.MinimumArea));
            sliderMaximumArea.Value = Convert.ToInt32(Math.Sqrt(_currentSettings.MaximumArea));
            sliderMinimumPercentMotion.Value = Convert.ToInt32(_currentSettings.MinimumPercentMotionInArea * 100);
        }
       
        private void SetUIFromSubtractorConfig()
        {
            var subtractorConfig = _currentSettings.SubtractorConfig;
            txtBoxHistory.Text = subtractorConfig.History.ToString();
            txtBoxThreshold.Text = subtractorConfig.Threshold.ToString();
            chkBoxShadowDetection.Checked = subtractorConfig.ShadowDetection;
        }


        private void btnWrite_Click(object sender, EventArgs e)
        {
            _motionSettingsRepo.Write(HarvestSettingsFromUI());
            NotifyStatus("Wrote settings to respository");
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            if (_motionSettingsRepo.IsPresent)
            {
                _currentSettings = _motionSettingsRepo.Read();
                SetUIFromSubtractorConfig();
                SetUIFromAreaConfig();

                NotifyStatus("Read settings from repository");
            }
            else
            {
                Log.Info("Settings not found");
            }
        }


        private void sliderMinimumArea_ValueChanged(object sender, EventArgs e)
        {
            _currentSettings.MinimumArea = sliderMinimumArea.Value * sliderMinimumArea.Value;
            sliderMinimumArea.Label = "Minimum Area = " + _currentSettings.MinimumArea;
        }

        private void sliderMinimumPercentMotion_ValueChanged(object sender, EventArgs e)
        {
            _currentSettings.MinimumPercentMotionInArea = ((decimal)sliderMinimumPercentMotion.Value) / 100;
        }
        private void sliderMaximumArea_ValueChanged(object sender, EventArgs e)
        {
            _currentSettings.MaximumArea = sliderMaximumArea.Value * sliderMaximumArea.Value;
            sliderMaximumArea.Label = "Maximum Area = " + _currentSettings.MaximumArea;
        }

        private void btnSetSubtractorConfig_Click(object sender, EventArgs e)
        {
            _currentSettings.SubtractorConfig = HarvestSettingsFromUI().SubtractorConfig;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentSettings.BiggestMotionType = HarvestSettingsFromUI().BiggestMotionType;
        }

        private void sliderSize_Load(object sender, EventArgs e)
        {

        }
        
    }
}
