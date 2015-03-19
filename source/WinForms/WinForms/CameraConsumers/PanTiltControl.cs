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
using Kraken.Core;
using PiCamCV.Common;
using PiCamCV.Common.ExtensionMethods;
using PiCamCV.Common.Interfaces;
using PiCamCV.ConsoleApp.Runners.PanTilt;
using PiCamCV.Interfaces;
using PiCamCV.WinForms.ExtensionMethods;
using RPi.Pwm;

namespace PiCamCV.WinForms.CameraConsumers
{
    public partial class PanTiltControl : CameraConsumerUserControl
    {
        protected IPanTiltMechanism PanTiltMechanism { get; set; }
        private FaceTrackingPanTiltController _faceTrackingController;
        private ColourTrackingPanTiltController _colourTrackingController;
        private readonly IColourSettingsRepository _colourSettingsRepo;

        public Point? UserReticle { get; set; }

        public PanTiltControl()
        {
            InitializeComponent();
            UserReticle = null;
            _colourSettingsRepo = new ColourSettingsRepository();
        }

        private void btnGoto_Click(object sender, EventArgs e)
        {
            decimal panPercent, tiltPercent;

            var panOK = Decimal.TryParse(txtPanPercent.Text, out panPercent);
            var tiltOK = Decimal.TryParse(txtTiltPercent.Text, out tiltPercent);

            if (panOK && tiltOK)
            {
                PanTiltMechanism.PanServo.MoveTo(panPercent);
                PanTiltMechanism.TiltServo.MoveTo(tiltPercent);
            }
        }

        protected override void OnSubscribe()
        {
            var captureConfig = CameraCapture.GetCaptureProperties();
            var center = captureConfig.GetCenter();
            
            txtReticleX.Text = center.X.ToString();
            txtReticleY.Text = center.Y.ToString();

            InitI2C();

            var screen = new TextboxScreen(txtScreen);
            _faceTrackingController = new FaceTrackingPanTiltController(PanTiltMechanism, captureConfig, screen);
            _colourTrackingController = new ColourTrackingPanTiltController(PanTiltMechanism, captureConfig, screen);
            _colourTrackingController.Settings = _colourSettingsRepo.Read();
        }

        private void InitI2C()
        {
            Log.Info("Initialising I2C bus");
            if (PanTiltMechanism == null)
            {
                var pwmDeviceFactory = new Pca9685DeviceFactory();
                var pwmDevice = pwmDeviceFactory.GetDevice();
                PanTiltMechanism = new PanTiltMechanism(pwmDevice);
            }
        }

        public override void ImageGrabbedHandler(object sender, EventArgs e)
        {
            using (var matCaptured = new Mat())
            {
                CameraCapture.Retrieve(matCaptured);

                var bgrImage = matCaptured.ToImage<Bgr, byte>();
                var captureConfig = CameraCapture.GetCaptureProperties();
                var centre = captureConfig.GetCenter();

                DrawReticle(bgrImage, centre, Color.Red);

                if (UserReticle != null)
                {
                    DrawReticle(bgrImage, UserReticle.Value, Color.Green);
                }
                
                var input = new CameraProcessInput();
                input.SetCapturedImage = true;
                input.Captured = matCaptured;

                Action<int, string> writeTextToImage = (height, message) => bgrImage.Draw(message, new Point(0, height), Emgu.CV.CvEnum.FontFace.HersheySimplex, 0.4, new Bgr(Color.White));
                
                if (chkBoxColourTracking.Checked)
                {
                    var result = _colourTrackingController.Process(input);
                    if (result.IsDetected)
                    {
                        DrawReticle(bgrImage, result.Target, Color.Yellow);
                    }
                    writeTextToImage(captureConfig.Height - 10, "Colour Tracking");
                    NotifyStatus("Colour tracking took {0}", result.Elapsed.ToHumanReadable());
                }

                if (chkBoxFaceTracker.Checked)
                {
                    writeTextToImage(captureConfig.Height - 50, "Face Tracking");
                    var result = _faceTrackingController.Process(input);
                    if (result.IsDetected)
                    {
                        foreach (var face in result.Faces)
                        {
                            bgrImage.Draw(face.Region, new Bgr(Color.Yellow), 2);
                        }

                        DrawReticle(bgrImage, result.Target, Color.Yellow);
                    }

                    NotifyStatus("Face tracking took {0}", result.Elapsed.ToHumanReadable());
                }

                imageBoxCaptured.Image = bgrImage;
            }
        }

        private void DrawReticle(Image<Bgr, byte> image, Point center, Color colorIn)
        {
            const int reticleRadius = 25;
            var color = colorIn.ToBgr();
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
                UserReticle = new Point(xCoord, yCoord);
            }
            else
            {
                UserReticle = null;
            }
        }
    }
}
