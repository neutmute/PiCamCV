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
using PiCamCV.WinForms;
using PiCamCV.WinForms.UserControls;

namespace WinForms
{
    public partial class MainForm : Form
    {
        private CapturePi _capture;
        readonly List<KeyValuePair<TabPage, CameraConsumerUserControl>> _tabPageLinks;
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
            CvInvoke.UseOpenCL = false;
            CvInvoke.CheckLibraryLoaded();
            _capture = new CapturePi();
            
            var basicCapture = new BasicCaptureControl();
            var faceDetection = new FaceDetectionControl();;

            var consumers = new List<CameraConsumerUserControl>();
            consumers.Add(basicCapture);
            consumers.Add(faceDetection);
            
            _tabPageLinks.Add(new KeyValuePair<TabPage, CameraConsumerUserControl>(tabPageCameraCapture, basicCapture));
            _tabPageLinks.Add(new KeyValuePair<TabPage, CameraConsumerUserControl>(tabPageFaceDetection, faceDetection));
            
            foreach (var consumer in consumers)
            {
                consumer.CameraCapture = _capture;
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
                }

                CameraCaptureInProgress = !CameraCaptureInProgress;
            }
        }
    }
}
