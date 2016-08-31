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
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV.Util;
using Kraken.Core;
using PiCamCV.Common;
using PiCamCV.Common.ExtensionMethods;
using PiCamCV.WinForms.CameraConsumers.Base;
using Raspberry.IO.GeneralPurpose;

namespace PiCamCV.WinForms.CameraConsumers
{
    public partial class TrackingControl : CameraConsumerUserControl
    {
        private TrackingDetector _trackingDetector;
        private CamshiftDetector _camshiftDetector;
        Bgr _bgrRed;
        Bgr _bgrBlue;
        private ImageBoxSelector _imageBoxSelector;
        private Rectangle _readyRectangle;

        public TrackingControl()
        {
            InitializeComponent();


            //The following detector types are supported: "MIL" – TrackerMIL; "BOOSTING" – TrackerBoosting
            _trackingDetector = new TrackingDetector("MIL");
            _camshiftDetector= new CamshiftDetector();

            _bgrRed = new Bgr(Color.Red);
            _bgrBlue = new Bgr(Color.Blue);
            _imageBoxSelector = new ImageBoxSelector();
            this.Load += TrackingControl_Load;
        }

        private void TrackingControl_Load(object sender, EventArgs e)
        {
            _imageBoxSelector.ConfigureBoxSelections(imageBoxTracking);
            _imageBoxSelector.SelectionMade += _imageBoxSelector_SelectionMade;
        }

        private void _imageBoxSelector_SelectionMade(object sender, Rectangle e)
        {
            _readyRectangle= e;
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
                else if (radCamshift.Checked)
                {
                    var output = DoCamShift(frame, inputImage);
                    imageBoxProcessed.Image = output.BackProjection;
                }

                if (!_imageBoxSelector.SeedingRectangle.IsEmpty)
                {
                    inputImage.Draw(_imageBoxSelector.SeedingRectangle, new Bgr(Color.Chartreuse));
                }

                imageBoxTracking.Image = inputImage;
            }
        }

        private CamshiftOutput DoCamShift(Mat frame, Image<Bgr, byte> inputImage)
        {
            var input = new TrackingInput();

            if (_readyRectangle != Rectangle.Empty)
            {
                input.Config.ObjectOfInterest = _readyRectangle;
                input.Config.StartNewTrack = true;
                _readyRectangle = Rectangle.Empty;
            }

            input.Captured = frame;

            var output = _camshiftDetector.Process(input);


            if (!output.ObjectOfInterest.Equals(RotatedRect.Empty))
            {
                var vertices = output.ObjectOfInterest.GetVertices();
                var points = vertices.ToList().ConvertAll(f => f.ToPoint()).ToArray();
                CvInvoke.Polylines(inputImage, points, true, new Bgr(Color.Red).MCvScalar, 2);
            }

            return output;
        }
        
        private Image<Bgr, byte> DoTrackingApi(Mat frame, Image<Bgr, byte> inputImage)
        {
            var input = new TrackingInput();

            if (_readyRectangle != Rectangle.Empty)
            {
                input.Config.ObjectOfInterest = _readyRectangle;
                input.Config.StartNewTrack = true;
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

    }
}
