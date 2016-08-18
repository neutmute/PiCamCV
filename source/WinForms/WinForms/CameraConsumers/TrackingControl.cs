using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Kraken.Core;
using PiCamCV.Common;
using Raspberry.IO.GeneralPurpose;

namespace PiCamCV.WinForms.CameraConsumers
{
    public partial class TrackingControl : CameraConsumerUserControl
    {
        private TrackingDetector _trackingDetector;
        Bgr _bgrRed;
        Bgr _bgrBlue;
        private Point _mouseDownLocation;
        private Rectangle _seedingRectangle;
        private Rectangle _readyRectangle;
        private ThresholdSelector _thresholdSelector;

        public TrackingControl()
        {
            InitializeComponent();


            //The following detector types are supported: "MIL" – TrackerMIL; "BOOSTING" – TrackerBoosting
            _trackingDetector = new TrackingDetector("MIL");
            _thresholdSelector = new ThresholdSelector();
            _bgrRed = new Bgr(Color.Red);
            _bgrBlue = new Bgr(Color.Blue);
            this.Load += TrackingControl_Load;
        }

        private void TrackingControl_Load(object sender, EventArgs e)
        {
            imageBoxTracking.FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
            imageBoxTracking.MouseDown += imageBoxTracking_MouseDown;
            imageBoxTracking.MouseUp += imageBoxTracking_MouseUp;
            imageBoxTracking.MouseMove += imageBoxTracking_MouseMove;
            _thresholdSelector.ColourCheckTick += ThresholdSelector_ColourCheckTick;

            _thresholdSelector.Intercept = s =>
            {
                NotifyStatus($"Tick {s}");
                Thread.Sleep(250);
            };
        }

        private void ThresholdSelector_ColourCheckTick(object sender, ColourDetectorOutput e)
        {
            imageBoxProcessed.Image = e.ThresholdImage;
        }

        public override void ImageGrabbedHandler(object sender, EventArgs e)
        {
            using (var frame = new Mat())
            {
                CameraCapture.Retrieve(frame);
                var inputImage = frame.ToImage<Bgr, byte>();

                if (radTrackingApi.Checked)
                {
                    inputImage = DoTrackingApi(frame, inputImage);
                }
                else if (radColourTracking.Checked)
                {
                    if (_readyRectangle != Rectangle.Empty)
                    {
                        DoColourThresholding(frame);
                    }

                    inputImage.Draw(_seedingRectangle, _bgrBlue);
                }

                imageBoxTracking.Image = inputImage;
            }
        }

        private void DoColourThresholding(Mat frame)
        {
            ThresholdSettings settings = null;
            if (_readyRectangle != Rectangle.Empty)
            {
                settings = _thresholdSelector.Select(frame, _readyRectangle);
                _readyRectangle = Rectangle.Empty;
            }
            else
            {
                imageBoxTracking.Image = frame;
            }
        }

        private Image<Bgr, byte> DoTrackingApi(Mat frame, Image<Bgr, byte> inputImage)
        {
            var input = new TrackingInput();

            if (_readyRectangle != Rectangle.Empty)
            {
                input.ObjectOfInterest = _readyRectangle;
                input.StartNewTrack = true;
                _readyRectangle = Rectangle.Empty;
            }

            input.Captured = frame;

            var output = _trackingDetector.Process(input);

            if (output.ObjectOfInterest != Rectangle.Empty)
            {
                inputImage.Draw(output.ObjectOfInterest, _bgrRed);
            }
            
            NotifyStatus($"Tracking took {output.Elapsed.ToHumanReadable()}");

            return inputImage;
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
                _seedingRectangle = imageBoxTracking.GetRectangle(_mouseDownLocation, e.Location);
            }
        }

        private void imageBoxTracking_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _readyRectangle = imageBoxTracking.GetRectangle(_mouseDownLocation, e.Location);
                Log.Info("Rectangle ready");

                _mouseDownLocation = Point.Empty;
                _seedingRectangle = Rectangle.Empty;
            }
        }
    }
}
