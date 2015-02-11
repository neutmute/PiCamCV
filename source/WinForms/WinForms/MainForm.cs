using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Logging;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Kraken.Core;
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
        protected static ILog Log = LogManager.GetCurrentClassLogger();
        private FileInfo _videoFileSource;
        private ICaptureGrab _capture;
        readonly List<KeyValuePair<TabPage, CameraConsumerUserControl>> _tabPageLinks;
        private readonly FpsTracker _fpsTracker;
        private List<CameraConsumerUserControl> _consumers;

        public bool CameraCaptureInProgress { get; set; }
        public MainForm()
        {
            InitializeComponent();
            _tabPageLinks = new List<KeyValuePair<TabPage, CameraConsumerUserControl>>();
            _fpsTracker = new FpsTracker();
        }
        
        private void MainForm_Load(object sender, EventArgs e)
        {
            var appData = ExecutionEnvironment.GetApplicationMetadata();
            Log.Info(appData);

            SetupCameraConsumers();
            radCamera.Checked = true;
            cmbResolution.SelectedIndex = 1;
        }

        private CaptureConfig HarvestFormCaptureConfig()
        {
            var config = new CaptureConfig();
            var resComponents = cmbResolution.Text.Split('x');
            config.Width = Convert.ToInt32(resComponents[0]);
            config.Height = Convert.ToInt32(resComponents[1]);
            
            //switch (cmbResolution.SelectedIndex)
            //{
            //    case 0:
            //        config.Width = 320;
            //        config.Height = 240;
            //        break;
            //    case 1:
            //        config.Width = 640;
            //        config.Height = 480;
            //        break;
            //    case 2:
            //        config.Width = 960;
            //        config.Height = 720;
            //        break;
            //}
            config.Monochrome = chkMonochrome.Checked;

            return config;
        }

        private void SetCaptureProperties()
        {
            var capSettings = HarvestFormCaptureConfig();
            _capture.SetCaptureProperties(capSettings); //access violation
        }

        private void SetupFramerateTracking(ICaptureGrab capture)
        {
            capture.ImageGrabbed += _fpsTracker.NotifyImageGrabbed;
            _fpsTracker.ReportFrames = s => InvokeUI(() => { toolStripLabelFrames.Text = s; });
        }

        protected void InvokeUI(Action action)
        {
            if (!IsDisposed)
            {
                Invoke((MethodInvoker) (() => action()));
            }
        }

        private void AssignCaptureToConsumers(ICaptureGrab capture)
        {
            foreach (var consumer in _consumers)
            {
                consumer.CameraCapture = capture;
                var tabPage = _tabPageLinks.Find(kvp => kvp.Value == consumer).Key;
                tabPage.Controls.Add(consumer);
                consumer.Dock = DockStyle.Fill;
                consumer.StatusUpdated += consumer_StatusUpdated;
            }
        }

        private void SetupCameraConsumers()
        {
            var basicCapture = new BasicCaptureControl();
            var faceDetection = new FaceDetectionControl();
            var colourDetection = new ColourDetectionControl();
            var haarDetection = new HaarCascadeControl();
            var shapeDetection = new ShapeDetectionControl();

            _consumers = new List<CameraConsumerUserControl>();
            _consumers.Add(basicCapture);
            _consumers.Add(faceDetection);
            _consumers.Add(colourDetection);
            _consumers.Add(haarDetection);
            _consumers.Add(shapeDetection);

            _tabPageLinks.Add(new KeyValuePair<TabPage, CameraConsumerUserControl>(tabPageCameraCapture, basicCapture));
            _tabPageLinks.Add(new KeyValuePair<TabPage, CameraConsumerUserControl>(tabPageFaceDetection, faceDetection));
            _tabPageLinks.Add(new KeyValuePair<TabPage, CameraConsumerUserControl>(tabPageColourDetect, colourDetection));
            _tabPageLinks.Add(new KeyValuePair<TabPage, CameraConsumerUserControl>(tabPageHaarCascade, haarDetection));
            _tabPageLinks.Add(new KeyValuePair<TabPage, CameraConsumerUserControl>(tabPageShapes, shapeDetection));


            tabControlMain.SelectedIndexChanged += tabControlMain_SelectedIndexChanged;
        }

        void consumer_StatusUpdated(object sender, StatusEventArgs e)
        {
            InvokeUI(() =>
            {
                if (!toolStripLabelStatus.IsDisposed)
                { 
                    toolStripLabelStatus.Text = e.Message;
                }
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
            if (_capture == null)
            {
                SetupCapture();
            }
            if (_capture != null)
            {
                if (CameraCaptureInProgress)
                {  
                    //stop the capture
                    btnStartStop.Text = "Start Capture";
                    _capture.Dispose();
                    _capture = null;
                }
                else
                {
                    btnStartStop.Text = "Stop";
                    _capture.Start();
                    toolStripLabelSettings.Text = _capture.GetCaptureProperties().ToString();
                }

                CameraCaptureInProgress = !CameraCaptureInProgress;
            }
        }

        private void chkOpenCL_CheckedChanged(object sender, EventArgs e)
        {
            CvInvoke.UseOpenCL = chkOpenCL.Checked;
        }

        private void SetupCapture()
        {
            if (_capture != null)
            {
                _capture.Dispose();
            }

            var request = new CaptureRequest
            {
                Device = CaptureDevice.Usb
                ,CameraIndex = (int) spinEditCameraIndex.Value
            };

            request.Config = HarvestFormCaptureConfig();

            if (radFile.Checked)
            {
                request.File = _videoFileSource;
            }

            //var captureDevice = CaptureDevice.Pi;
            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                request.Device = CaptureDevice.Pi;
            }

            CapturePi.DoMatMagic("CreateCapture");

            _capture = CaptureFactory.GetCapture(request);
            
            AssignCaptureToConsumers(_capture);
            SetupFramerateTracking(_capture);

            tabControlMain_SelectedIndexChanged(null, null);
        }

        /// <summary>
        /// mono text boxes broken hence unsual UI
        /// </summary>
        private void radFile_CheckedChanged(object sender, EventArgs e)
        {
            if (radFile.Checked)
            {
                var result = openFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    _videoFileSource = new FileInfo(openFileDialog.FileName);
                }
                else
                {
                    radCamera.Checked = true;
                }
            }
        }

    }
}
