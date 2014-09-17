using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using PiCamCV;
using PiCamCV.Common;
using PiCamCV.Interfaces;
using PiCamCV.WinForms;
using PiCamCV.WinForms.CameraConsumers;
using PiCamCV.WinForms.UserControls;

namespace WinForms
{
    public partial class MainForm : Form
    {
        private ICaptureGrab _capture;
        readonly List<KeyValuePair<TabPage, CameraConsumerUserControl>> _tabPageLinks;
        private FpsTracker _fpsTracker;
        public bool CameraCaptureInProgress { get; set; }
        public MainForm()
        {
            InitializeComponent();
            _tabPageLinks = new List<KeyValuePair<TabPage, CameraConsumerUserControl>>();
        }

        private void tabPageCameraCapture_Click(object sender, EventArgs e)
        {
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var captureDevice = CaptureDevice.Usb;
            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                captureDevice = CaptureDevice.Pi;
            }
            _capture = CaptureFactory.GetCapture(captureDevice);

            SetupCameraConsumers(_capture);
            SetupFramerateTracking(_capture);

            var capSettings = new CaptureProperties();
            capSettings.FrameWidth = 320;
            capSettings.FrameHeight = 240;
          //  _capture.SetCaptureProperties(capSettings); access violation
        }

        private void SetupFramerateTracking(ICaptureGrab capture)
        {
            _fpsTracker = new FpsTracker();
            capture.ImageGrabbed += _fpsTracker.NotifyImageGrabbed;
            _fpsTracker.ReportFrames = s => Invoke((MethodInvoker) delegate { labelFrameRate.Text = s; });
        }

        private void SetupCameraConsumers(ICaptureGrab capture)
        {
            var basicCapture = new BasicCaptureControl();
            var faceDetection = new FaceDetectionControl();
            var colourDetection = new ColourDetectionControl();
            var haarDetection = new HaarCascadeControl();

            var consumers = new List<CameraConsumerUserControl>();
            consumers.Add(basicCapture);
            consumers.Add(faceDetection);
            consumers.Add(colourDetection);
            consumers.Add(haarDetection);

            _tabPageLinks.Add(new KeyValuePair<TabPage, CameraConsumerUserControl>(tabPageCameraCapture, basicCapture));
            _tabPageLinks.Add(new KeyValuePair<TabPage, CameraConsumerUserControl>(tabPageFaceDetection, faceDetection));
            _tabPageLinks.Add(new KeyValuePair<TabPage, CameraConsumerUserControl>(tabPageColourDetect, colourDetection));
            _tabPageLinks.Add(new KeyValuePair<TabPage, CameraConsumerUserControl>(tabPageHaarCascade, haarDetection));

            foreach (var consumer in consumers)
            {
                consumer.CameraCapture = capture;
                var tabPage = _tabPageLinks.Find(kvp => kvp.Value == consumer).Key;
                tabPage.Controls.Add(consumer);
                consumer.Dock = DockStyle.Fill;
                consumer.StatusUpdated += consumer_StatusUpdated;
            }

            tabControlMain.SelectedIndexChanged += tabControlMain_SelectedIndexChanged;
            tabControlMain_SelectedIndexChanged(null, null);
        }

        void consumer_StatusUpdated(object sender, StatusEventArgs e)
        {
            labelStatus.Invoke((MethodInvoker)delegate
            {
                labelStatus.Text = e.Message;
            });
        }

        void tabControlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var consumer in _tabPageLinks.Select(kvp => kvp.Value))
            {
                consumer.Unsubscribe();    
            }
            var selectedControl = _tabPageLinks.First(kvp => kvp.Key == tabControlMain.SelectedTab).Value;
            selectedControl.Subscribe();
            consumer_StatusUpdated(this, new StatusEventArgs(null));
        }


        private void btnFlipVertical_Click(object sender, EventArgs e)
        {
            if (_capture != null) _capture.FlipVertical = !_capture.FlipVertical;
        }

        private void btnFlipHorizontal_Click(object sender, EventArgs e)
        {
            if (_capture != null) _capture.FlipHorizontal = !_capture.FlipHorizontal;
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if (_capture != null)
            {
                if (CameraCaptureInProgress)
                {  //stop the capture
                    btnStartStop.Text = "Start Capture";
                    _capture.Pause();
                }
                else
                {
                    //start the capture
                    btnStartStop.Text = "Stop";
                    _capture.Start();
                    toolStripLabelSettings.Text = _capture.GetCaptureProperties().ToString();
                }

                CameraCaptureInProgress = !CameraCaptureInProgress;
            }
        }
    }
}
