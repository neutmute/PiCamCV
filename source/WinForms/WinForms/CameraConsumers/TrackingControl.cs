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

        public TrackingControl()
        {
            InitializeComponent();


            //The following detector types are supported: "MIL" – TrackerMIL; "BOOSTING" – TrackerBoosting
            _trackingDetector = new TrackingDetector("MIL");
            _bgrRed = new Bgr(Color.Red);
            _bgrBlue = new Bgr(Color.Blue);
            this.Load += TrackingControl_Load;
        }

        private void TrackingControl_Load(object sender, EventArgs e)
        {
            imageBoxTracking.FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
        }

        public override void ImageGrabbedHandler(object sender, EventArgs e)
        {
            using (var frame = new Mat())
            {
                CameraCapture.Retrieve(frame);
                var input = new TrackingInput();

                if (_readyRectangle != Rectangle.Empty)
                {
                    input.ObjectOfInterest = _readyRectangle;
                    input.StartNewTrack = true;
                    _readyRectangle = Rectangle.Empty;
                }

                var inputImage = frame.ToImage<Bgr, byte>();
                input.Captured = frame;
                
                var output = _trackingDetector.Process(input);

                if (output.ObjectOfInterest != Rectangle.Empty)
                {
                    inputImage.Draw(output.ObjectOfInterest, _bgrRed);
                }

                if (_seedingRectangle != Rectangle.Empty)
                {
                    inputImage.Draw(_seedingRectangle, _bgrBlue);
                }

                imageBoxTracking.Image = inputImage;
                NotifyStatus($"Tracking took {output.Elapsed.ToHumanReadable()}");
            }
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
