using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Diagnostics;
using System.Threading;
using Kraken.Core;
using PiCamCV.Common;
using PiCamCV.Common.ExtensionMethods;
using PiCamCV.WinForms.CameraConsumers.Base;
using PiCamCV.WinForms.ExtensionMethods;

namespace PiCamCV.WinForms.CameraConsumers
{
    public partial class ColourDetectionControl : CameraConsumerUserControl
    {
        private readonly ColourDetector _colorDetector;
        private readonly IColourSettingsRepository _colorSettingsRepo;
        private bool _suppressUpdates;
        private bool _captureResized;
        private ThresholdSelector _thresholdSelector;
        private ImageBoxSelector _imageBoxSelector;
        private Rectangle _readyRectangle;

        private readonly ColourDetectorInput _detectorInput;

        public ColourDetectionControl()
        {
            InitializeComponent();
            _colorDetector = new ColourDetector();
            _detectorInput = new ColourDetectorInput();
            _colorSettingsRepo = new ColourSettingsRepository();
            _thresholdSelector = new ThresholdSelector();

            _imageBoxSelector = new ImageBoxSelector();
        }
        
        private void ThresholdSelector_ColourCheckTick(object sender, AutoThresholdResult e)
        {
            var processed = e.FullOutput.ThresholdImage;

            var enhanced = processed.Mat.ToImage<Bgr, byte>();

            enhanced.Draw(_readyRectangle, new Bgr(Color.Blue));

            imageBoxFiltered.Image = enhanced;
        }

        private void _imageBoxSelector_SelectionMade(object sender, Rectangle e)
        {
            _readyRectangle = e;
        }

        private void DoAutoThresholding(Mat frame)
        {
            ThresholdSettings settings = null;
            if (_readyRectangle != Rectangle.Empty)
            {
                _thresholdSelector.ErodeDilateIterations = (int)spinDilateIterations.Value;
                _thresholdSelector.RequiredMomentAreaInRoiPercent = (int) spinEditAutoTunePercent.Value;

                var autoSelected = _thresholdSelector.Select(frame, _readyRectangle);
                _detectorInput.Settings.Absorb(autoSelected);

                UpdateThresholdSlidersFromSettings();

                _readyRectangle = Rectangle.Empty;
            }
            else
            {
                imageBoxCaptured.Image = frame;
            }
        }


        public override void ImageGrabbedHandler(object sender, EventArgs e)
        {
            using (var matCaptured = new Mat())
            {
                var retrieveElapsed = Stopwatch.StartNew();
                CameraCapture.Retrieve(matCaptured);
                retrieveElapsed.Stop();
                
                if (_readyRectangle.IsEmpty)
                {
                    _detectorInput.ErodeDilateIterations = (int) spinDilateIterations.Value;
                    _detectorInput.Settings.Roi = GetRegionOfInterestFromControls();
                    _detectorInput.Captured = matCaptured;

                    var output = _colorDetector.Process(_detectorInput);

                    DrawReticle(output.CapturedImage, output.CentralPoint.ToPoint(), Color.Aqua);

                    if (output.IsDetected)
                    {
                        var radius = 50;
                        var circle = new CircleF(output.CentralPoint, radius);
                        var color = new Bgr(Color.Yellow);
                        output.CapturedImage.Draw(circle, color, 3);
                        var ballTextLocation = output.CentralPoint.ToPoint();
                        ballTextLocation.X += radius;
                        //  output.CapturedImage.Draw("ball", ballTextLocation, FontFace.HersheyPlain, 3, color);
                    }

                    if (checkBoxRoi.Checked)
                    {
                        output.CapturedImage.Draw(_detectorInput.Settings.Roi, Color.Green.ToBgr(), 3);
                    }

                    if (!_imageBoxSelector.SeedingRectangle.IsEmpty)
                    {
                        output.CapturedImage.Draw(_imageBoxSelector.SeedingRectangle, new Bgr(Color.Chartreuse));
                    }

                    imageBoxCaptured.Image = output.CapturedImage;
                    imageBoxFiltered.Image = output.ThresholdImage;

                    NotifyStatus(
                        "Retrieved frame in {0}, {1}"
                        , retrieveElapsed.Elapsed.ToHumanReadable(HumanReadableTimeSpanOptions.Abbreviated)
                        , output);
                }
                else
                {
                    DoAutoThresholding(matCaptured);
                }

                ResizeImageControls();

            }
        }

        private ColourDetectSettings GetColourDetectSettings()
        {
            var settings = _detectorInput.Settings;
            settings.Roi = GetRegionOfInterestFromControls();
            return settings;
        }

        private Rectangle GetRegionOfInterestFromControls()
        {
             var roiRectangle = Rectangle.Empty;

            if (checkBoxRoi.Checked)
            {
                // transpose top/bottom to make ui up/down more intuitive
                var imageHeight = sliderRoiTop.Maximum;
                var top = imageHeight - sliderRoiTop.Value;
                var bottom = imageHeight - sliderRoiBottom.Value;

                var left = sliderRoiLeft.Value;
                var width = (sliderRoiRight.Value - sliderRoiLeft.Value);
                var height = bottom - top;

                roiRectangle = new Rectangle(left, top, width, height);
            }

            return roiRectangle;
        }

        private void ResizeImageControls(bool forceResize = false)
        {
            if (!_captureResized || forceResize)
            {
                _captureResized = true;

                IImage image = imageBoxCaptured.Image;

                var marginFat = 20;
                var newSize = new Size(
                    (int)(image.Size.Width * imageBoxCaptured.ZoomScale) + marginFat
                    , (int)(image.Size.Height * imageBoxCaptured.ZoomScale) + marginFat);
                InvokeUI(() =>
                {
                    var width = image.Size.Width;
                    var height = image.Size.Height;
                    groupBoxCaptured.Size = newSize;
                    groupBoxFiltered.Size = newSize;

                    sliderRoiLeft.Maximum = width;
                    sliderRoiRight.Maximum = width;

                    sliderRoiTop.Maximum = height;
                    sliderRoiBottom.Maximum = height;

                    sliderRoiRight.Value = width;
                    sliderRoiTop.Value = height;
                });
            }
        }

        private void ColourDetectionControl_Load(object sender, EventArgs e)
        {
            _imageBoxSelector.ConfigureBoxSelections(imageBoxCaptured);
            _imageBoxSelector.SelectionMade += _imageBoxSelector_SelectionMade;

            _thresholdSelector.ColourCheckTick += ThresholdSelector_ColourCheckTick;

            _thresholdSelector.Intercept = s =>
            {
                NotifyStatus($"Tick {s}");
               // Thread.Sleep(20);
            };

            sliderHueMin.ValueChanged += HsvSlider_ValueChanged;
            sliderHueMax.ValueChanged += HsvSlider_ValueChanged;
            sliderSaturationMin.ValueChanged += HsvSlider_ValueChanged;
            sliderSaturationMax.ValueChanged += HsvSlider_ValueChanged;
            sliderValueMin.ValueChanged += HsvSlider_ValueChanged;
            sliderValueMax.ValueChanged += HsvSlider_ValueChanged;

            sliderMomentAreaMin.ValueChanged += MomentSlider_ValueChanged;
            sliderMomentAreaMax.ValueChanged += MomentSlider_ValueChanged;
            
            sliderHueMax.Maximum = 180;
            sliderHueMin.Maximum = 180;
            sliderMomentAreaMax.Maximum = (int) (640*480*0.25);

            btnReset_Click(null, null);
        }
        
        void HsvSlider_ValueChanged(object sender, EventArgs e)
        {
            UpdateThresholdSettingsFromControls();
        }

        void MomentSlider_ValueChanged(object sender, EventArgs e)
        {
            UpdateMomentSettingsFromControls();
        }

        
        private void SetThresholdScalars(double lh, double ls, double lv, double hh, double hs, double hv)
        {
            var settings = ThresholdSettings.Get(lh, ls, lv, hh, hs, hv);
            _detectorInput.Settings.Absorb(settings);
            UpdateThresholdSlidersFromSettings();
        }
        
        private void SetMomentArea(float min, float max)
        {
            var momentRange = new RangeF();
            
            momentRange.Min = min;
            momentRange.Max = max;

            _detectorInput.Settings.MomentArea = momentRange;

            UpdateMomentSlidersFromSettings();
        }

        private void UpdateThresholdSlidersFromSettings()
        {
            _suppressUpdates = true;

            var settings = _detectorInput.Settings;
            sliderHueMin.Value = (int)settings.LowThreshold.V0;
            sliderSaturationMin.Value = (int)settings.LowThreshold.V1;
            sliderValueMin.Value = (int)settings.LowThreshold.V2;

            sliderHueMax.Value = (int)settings.HighThreshold.V0;
            sliderSaturationMax.Value = (int)settings.HighThreshold.V1;
            sliderValueMax.Value = (int)settings.HighThreshold.V2;
            
            _suppressUpdates = false;
        }

        private void UpdateMomentSlidersFromSettings()
        {
            _suppressUpdates = true;

            var settings = _detectorInput.Settings;

            sliderMomentAreaMin.Value = (int)settings.MomentArea.Min;
            sliderMomentAreaMax.Value = (int)settings.MomentArea.Max;

            _suppressUpdates = false;
        }
        
        private void UpdateMomentSettingsFromControls()
        {
            if (!_suppressUpdates)
            {
                SetMomentArea(sliderMomentAreaMin.Value, sliderMomentAreaMax.Value);
            }
        }

        private void UpdateThresholdSettingsFromControls()
        {
            if (!_suppressUpdates)
            {
                var lowH = sliderHueMin.Value;
                var lowS = sliderSaturationMin.Value;
                var lowV = sliderValueMin.Value;

                var highH = sliderHueMax.Value;
                var highS = sliderSaturationMax.Value;
                var highV = sliderValueMax.Value;

                SetThresholdScalars(lowH, lowS, lowV, highH, highS, highV);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            SetThresholdScalars(0, 0, 0, 180, 255, 255);
            SetMomentArea(200, 1000);
        }

        /// <summary>
        /// Under CFL lights
        /// </summary>
        private void btnRedLights_Click(object sender, EventArgs e)
        {
            SetThresholdScalars(140, 57, 25, 180, 153, 182);
        }

        /// <summary>
        /// -t=155,128,44|182,214,105
        /// </summary>
        private void btnRedDaylight_Click(object sender, EventArgs e)
        {
            SetThresholdScalars(155, 128, 44, 180, 214, 105);
        }

        private void checkBoxRoiEnabled_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnWriteSettingsForConsole_Click(object sender, EventArgs e)
        {
            _colorSettingsRepo.Write(GetColourDetectSettings());
            NotifyStatus("Wrote settings to respository");
        }

        private void btnReadSettings_Click(object sender, EventArgs e)
        {
            if (_colorSettingsRepo.IsPresent)
            {
                _detectorInput.Settings = _colorSettingsRepo.Read();
                UpdateMomentSlidersFromSettings();
                UpdateThresholdSlidersFromSettings();
                NotifyStatus("Read settings from repository");
            }
            else
            {
                Log.Info("Settings not found");
            }
        }
        
        private void imageBoxCaptured_OnZoomScaleChange(object sender, EventArgs e)
        {
            ResizeImageControls(true);
        }
    }
}
