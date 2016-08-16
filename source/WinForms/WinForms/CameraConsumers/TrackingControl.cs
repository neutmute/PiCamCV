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
    public partial class TrackingControl : CameraConsumerUserControl
    {
        private TrackingDetector _trackingDetector;
        Bgr _bgrRed;

        public TrackingControl()
        {
            InitializeComponent();
            _trackingDetector = new TrackingDetector();
            _bgrRed = new Bgr(Color.Red);
        }

        public override void ImageGrabbedHandler(object sender, EventArgs e)
        {
            using (var frame = new Mat())
            {
                CameraCapture.Retrieve(frame);
                var input = new TrackingInput();

                var inputImage = frame.ToImage<Bgr, byte>();
                input.Captured = frame;
                
                var output = _trackingDetector.Process(input);
                
                if (output.ObjectOfInterest != Rectangle.Empty)
                {
                    inputImage.Draw(output.ObjectOfInterest, _bgrRed);
                }
                imageBoxTracking.Image = inputImage;
                NotifyStatus($"Tracking took {output.Elapsed.ToHumanReadable()}");
            }
        }

        private void imageBoxTracking_MouseDown(object sender, MouseEventArgs e)
        {
            //this.imageBoxTracking.FunctionalMode= 
        }
    }
}
