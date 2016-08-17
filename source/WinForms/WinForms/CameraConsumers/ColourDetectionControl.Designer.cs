namespace PiCamCV.WinForms.CameraConsumers
{
    partial class ColourDetectionControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnReadSettings = new System.Windows.Forms.Button();
            this.btnWriteSettingsForConsole = new System.Windows.Forms.Button();
            this.groupBoxMoments = new System.Windows.Forms.GroupBox();
            this.sliderMomentAreaMax = new PiCamCV.WinForms.UserControls.SliderControl();
            this.sliderMomentAreaMin = new PiCamCV.WinForms.UserControls.SliderControl();
            this.groupBoxRoi = new System.Windows.Forms.GroupBox();
            this.sliderRoiBottom = new PiCamCV.WinForms.UserControls.SliderControl();
            this.sliderRoiTop = new PiCamCV.WinForms.UserControls.SliderControl();
            this.sliderRoiRight = new PiCamCV.WinForms.UserControls.SliderControl();
            this.sliderRoiLeft = new PiCamCV.WinForms.UserControls.SliderControl();
            this.checkBoxRoi = new System.Windows.Forms.CheckBox();
            this.groupBoxPresets = new System.Windows.Forms.GroupBox();
            this.btnRedDaylight = new System.Windows.Forms.Button();
            this.btnRedLights = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.sliderValueMax = new PiCamCV.WinForms.UserControls.SliderControl();
            this.sliderValueMin = new PiCamCV.WinForms.UserControls.SliderControl();
            this.sliderSaturationMax = new PiCamCV.WinForms.UserControls.SliderControl();
            this.sliderSaturationMin = new PiCamCV.WinForms.UserControls.SliderControl();
            this.sliderHueMax = new PiCamCV.WinForms.UserControls.SliderControl();
            this.sliderHueMin = new PiCamCV.WinForms.UserControls.SliderControl();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBoxCaptured = new System.Windows.Forms.GroupBox();
            this.imageBoxCaptured = new Emgu.CV.UI.ImageBox();
            this.groupBoxFiltered = new System.Windows.Forms.GroupBox();
            this.imageBoxFiltered = new Emgu.CV.UI.ImageBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.spinDilateIterations = new System.Windows.Forms.NumericUpDown();
            this.panelLeft.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.groupBoxMoments.SuspendLayout();
            this.groupBoxRoi.SuspendLayout();
            this.groupBoxPresets.SuspendLayout();
            this.flowLayoutPanel.SuspendLayout();
            this.groupBoxCaptured.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCaptured)).BeginInit();
            this.groupBoxFiltered.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxFiltered)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDilateIterations)).BeginInit();
            this.SuspendLayout();
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.panelButtons);
            this.panelLeft.Controls.Add(this.groupBoxMoments);
            this.panelLeft.Controls.Add(this.groupBoxRoi);
            this.panelLeft.Controls.Add(this.groupBoxPresets);
            this.panelLeft.Controls.Add(this.sliderValueMax);
            this.panelLeft.Controls.Add(this.sliderValueMin);
            this.panelLeft.Controls.Add(this.sliderSaturationMax);
            this.panelLeft.Controls.Add(this.sliderSaturationMin);
            this.panelLeft.Controls.Add(this.sliderHueMax);
            this.panelLeft.Controls.Add(this.sliderHueMin);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(194, 731);
            this.panelLeft.TabIndex = 5;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnReadSettings);
            this.panelButtons.Controls.Add(this.btnWriteSettingsForConsole);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelButtons.Location = new System.Drawing.Point(0, 623);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(194, 69);
            this.panelButtons.TabIndex = 10;
            // 
            // btnReadSettings
            // 
            this.btnReadSettings.Location = new System.Drawing.Point(3, 43);
            this.btnReadSettings.Name = "btnReadSettings";
            this.btnReadSettings.Size = new System.Drawing.Size(179, 23);
            this.btnReadSettings.TabIndex = 1;
            this.btnReadSettings.Text = "Read settings from disk";
            this.btnReadSettings.UseVisualStyleBackColor = true;
            this.btnReadSettings.Click += new System.EventHandler(this.btnReadSettings_Click);
            // 
            // btnWriteSettingsForConsole
            // 
            this.btnWriteSettingsForConsole.Location = new System.Drawing.Point(3, 15);
            this.btnWriteSettingsForConsole.Name = "btnWriteSettingsForConsole";
            this.btnWriteSettingsForConsole.Size = new System.Drawing.Size(179, 23);
            this.btnWriteSettingsForConsole.TabIndex = 0;
            this.btnWriteSettingsForConsole.Text = "Write settings to disk";
            this.btnWriteSettingsForConsole.UseVisualStyleBackColor = true;
            this.btnWriteSettingsForConsole.Click += new System.EventHandler(this.btnWriteSettingsForConsole_Click);
            // 
            // groupBoxMoments
            // 
            this.groupBoxMoments.Controls.Add(this.sliderMomentAreaMax);
            this.groupBoxMoments.Controls.Add(this.sliderMomentAreaMin);
            this.groupBoxMoments.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxMoments.Location = new System.Drawing.Point(0, 534);
            this.groupBoxMoments.Name = "groupBoxMoments";
            this.groupBoxMoments.Size = new System.Drawing.Size(194, 89);
            this.groupBoxMoments.TabIndex = 11;
            this.groupBoxMoments.TabStop = false;
            this.groupBoxMoments.Text = "Moments";
            // 
            // sliderMomentAreaMax
            // 
            this.sliderMomentAreaMax.Dock = System.Windows.Forms.DockStyle.Top;
            this.sliderMomentAreaMax.Label = "Area Max";
            this.sliderMomentAreaMax.LargeChange = 200;
            this.sliderMomentAreaMax.Location = new System.Drawing.Point(3, 56);
            this.sliderMomentAreaMax.Maximum = 100000;
            this.sliderMomentAreaMax.Minimum = 0;
            this.sliderMomentAreaMax.Name = "sliderMomentAreaMax";
            this.sliderMomentAreaMax.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.sliderMomentAreaMax.Size = new System.Drawing.Size(188, 40);
            this.sliderMomentAreaMax.SmallChange = 100;
            this.sliderMomentAreaMax.TabIndex = 5;
            this.sliderMomentAreaMax.TickFrequency = 1000;
            this.sliderMomentAreaMax.Value = 0;
            // 
            // sliderMomentAreaMin
            // 
            this.sliderMomentAreaMin.Dock = System.Windows.Forms.DockStyle.Top;
            this.sliderMomentAreaMin.Label = "Area Min";
            this.sliderMomentAreaMin.LargeChange = 5;
            this.sliderMomentAreaMin.Location = new System.Drawing.Point(3, 16);
            this.sliderMomentAreaMin.Maximum = 1000;
            this.sliderMomentAreaMin.Minimum = 0;
            this.sliderMomentAreaMin.Name = "sliderMomentAreaMin";
            this.sliderMomentAreaMin.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.sliderMomentAreaMin.Size = new System.Drawing.Size(188, 40);
            this.sliderMomentAreaMin.SmallChange = 1;
            this.sliderMomentAreaMin.TabIndex = 4;
            this.sliderMomentAreaMin.TickFrequency = 1;
            this.sliderMomentAreaMin.Value = 0;
            // 
            // groupBoxRoi
            // 
            this.groupBoxRoi.Controls.Add(this.progressBar1);
            this.groupBoxRoi.Controls.Add(this.sliderRoiBottom);
            this.groupBoxRoi.Controls.Add(this.sliderRoiTop);
            this.groupBoxRoi.Controls.Add(this.sliderRoiRight);
            this.groupBoxRoi.Controls.Add(this.sliderRoiLeft);
            this.groupBoxRoi.Controls.Add(this.checkBoxRoi);
            this.groupBoxRoi.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxRoi.Location = new System.Drawing.Point(0, 316);
            this.groupBoxRoi.Name = "groupBoxRoi";
            this.groupBoxRoi.Size = new System.Drawing.Size(194, 218);
            this.groupBoxRoi.TabIndex = 9;
            this.groupBoxRoi.TabStop = false;
            this.groupBoxRoi.Text = "Region of Interest";
            // 
            // sliderRoiBottom
            // 
            this.sliderRoiBottom.Dock = System.Windows.Forms.DockStyle.Left;
            this.sliderRoiBottom.Label = "Bottom";
            this.sliderRoiBottom.LargeChange = 5;
            this.sliderRoiBottom.Location = new System.Drawing.Point(95, 125);
            this.sliderRoiBottom.Maximum = 255;
            this.sliderRoiBottom.Minimum = 0;
            this.sliderRoiBottom.Name = "sliderRoiBottom";
            this.sliderRoiBottom.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.sliderRoiBottom.Size = new System.Drawing.Size(90, 90);
            this.sliderRoiBottom.SmallChange = 1;
            this.sliderRoiBottom.TabIndex = 3;
            this.sliderRoiBottom.TickFrequency = 1;
            this.sliderRoiBottom.Value = 0;
            // 
            // sliderRoiTop
            // 
            this.sliderRoiTop.Dock = System.Windows.Forms.DockStyle.Left;
            this.sliderRoiTop.Label = "Top";
            this.sliderRoiTop.LargeChange = 5;
            this.sliderRoiTop.Location = new System.Drawing.Point(3, 125);
            this.sliderRoiTop.Maximum = 255;
            this.sliderRoiTop.Minimum = 0;
            this.sliderRoiTop.Name = "sliderRoiTop";
            this.sliderRoiTop.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.sliderRoiTop.Size = new System.Drawing.Size(92, 90);
            this.sliderRoiTop.SmallChange = 1;
            this.sliderRoiTop.TabIndex = 2;
            this.sliderRoiTop.TickFrequency = 1;
            this.sliderRoiTop.Value = 0;
            // 
            // sliderRoiRight
            // 
            this.sliderRoiRight.Dock = System.Windows.Forms.DockStyle.Top;
            this.sliderRoiRight.Label = "Right";
            this.sliderRoiRight.LargeChange = 5;
            this.sliderRoiRight.Location = new System.Drawing.Point(3, 79);
            this.sliderRoiRight.Maximum = 255;
            this.sliderRoiRight.Minimum = 0;
            this.sliderRoiRight.Name = "sliderRoiRight";
            this.sliderRoiRight.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.sliderRoiRight.Size = new System.Drawing.Size(188, 46);
            this.sliderRoiRight.SmallChange = 1;
            this.sliderRoiRight.TabIndex = 1;
            this.sliderRoiRight.TickFrequency = 1;
            this.sliderRoiRight.Value = 0;
            // 
            // sliderRoiLeft
            // 
            this.sliderRoiLeft.Dock = System.Windows.Forms.DockStyle.Top;
            this.sliderRoiLeft.Label = "Left";
            this.sliderRoiLeft.LargeChange = 5;
            this.sliderRoiLeft.Location = new System.Drawing.Point(3, 33);
            this.sliderRoiLeft.Maximum = 255;
            this.sliderRoiLeft.Minimum = 0;
            this.sliderRoiLeft.Name = "sliderRoiLeft";
            this.sliderRoiLeft.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.sliderRoiLeft.Size = new System.Drawing.Size(188, 46);
            this.sliderRoiLeft.SmallChange = 1;
            this.sliderRoiLeft.TabIndex = 0;
            this.sliderRoiLeft.TickFrequency = 1;
            this.sliderRoiLeft.Value = 0;
            // 
            // checkBoxRoi
            // 
            this.checkBoxRoi.AutoSize = true;
            this.checkBoxRoi.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxRoi.Location = new System.Drawing.Point(3, 16);
            this.checkBoxRoi.Name = "checkBoxRoi";
            this.checkBoxRoi.Size = new System.Drawing.Size(188, 17);
            this.checkBoxRoi.TabIndex = 4;
            this.checkBoxRoi.Text = "Enabled";
            this.checkBoxRoi.UseVisualStyleBackColor = true;
            this.checkBoxRoi.CheckedChanged += new System.EventHandler(this.checkBoxRoiEnabled_CheckedChanged);
            // 
            // groupBoxPresets
            // 
            this.groupBoxPresets.Controls.Add(this.spinDilateIterations);
            this.groupBoxPresets.Controls.Add(this.btnRedDaylight);
            this.groupBoxPresets.Controls.Add(this.btnRedLights);
            this.groupBoxPresets.Controls.Add(this.btnReset);
            this.groupBoxPresets.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxPresets.Location = new System.Drawing.Point(0, 240);
            this.groupBoxPresets.Name = "groupBoxPresets";
            this.groupBoxPresets.Size = new System.Drawing.Size(194, 76);
            this.groupBoxPresets.TabIndex = 8;
            this.groupBoxPresets.TabStop = false;
            this.groupBoxPresets.Text = "HSV Presets";
            // 
            // btnRedDaylight
            // 
            this.btnRedDaylight.Location = new System.Drawing.Point(66, 48);
            this.btnRedDaylight.Name = "btnRedDaylight";
            this.btnRedDaylight.Size = new System.Drawing.Size(114, 23);
            this.btnRedDaylight.TabIndex = 9;
            this.btnRedDaylight.Text = "Red in Daylight";
            this.btnRedDaylight.UseVisualStyleBackColor = true;
            this.btnRedDaylight.Click += new System.EventHandler(this.btnRedDaylight_Click);
            // 
            // btnRedLights
            // 
            this.btnRedLights.Location = new System.Drawing.Point(66, 19);
            this.btnRedLights.Name = "btnRedLights";
            this.btnRedLights.Size = new System.Drawing.Size(114, 23);
            this.btnRedLights.TabIndex = 8;
            this.btnRedLights.Text = "Red under Lights";
            this.btnRedLights.UseVisualStyleBackColor = true;
            this.btnRedLights.Click += new System.EventHandler(this.btnRedLights_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(6, 19);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(54, 23);
            this.btnReset.TabIndex = 7;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // sliderValueMax
            // 
            this.sliderValueMax.Dock = System.Windows.Forms.DockStyle.Top;
            this.sliderValueMax.Label = "Value Max";
            this.sliderValueMax.LargeChange = 5;
            this.sliderValueMax.Location = new System.Drawing.Point(0, 200);
            this.sliderValueMax.Maximum = 255;
            this.sliderValueMax.Minimum = 0;
            this.sliderValueMax.Name = "sliderValueMax";
            this.sliderValueMax.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.sliderValueMax.Size = new System.Drawing.Size(194, 40);
            this.sliderValueMax.SmallChange = 1;
            this.sliderValueMax.TabIndex = 4;
            this.sliderValueMax.TickFrequency = 1;
            this.sliderValueMax.Value = 0;
            // 
            // sliderValueMin
            // 
            this.sliderValueMin.Dock = System.Windows.Forms.DockStyle.Top;
            this.sliderValueMin.Label = "Value Min";
            this.sliderValueMin.LargeChange = 5;
            this.sliderValueMin.Location = new System.Drawing.Point(0, 160);
            this.sliderValueMin.Maximum = 255;
            this.sliderValueMin.Minimum = 0;
            this.sliderValueMin.Name = "sliderValueMin";
            this.sliderValueMin.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.sliderValueMin.Size = new System.Drawing.Size(194, 40);
            this.sliderValueMin.SmallChange = 1;
            this.sliderValueMin.TabIndex = 5;
            this.sliderValueMin.TickFrequency = 1;
            this.sliderValueMin.Value = 0;
            // 
            // sliderSaturationMax
            // 
            this.sliderSaturationMax.Dock = System.Windows.Forms.DockStyle.Top;
            this.sliderSaturationMax.Label = "Saturation Max";
            this.sliderSaturationMax.LargeChange = 5;
            this.sliderSaturationMax.Location = new System.Drawing.Point(0, 120);
            this.sliderSaturationMax.Maximum = 255;
            this.sliderSaturationMax.Minimum = 0;
            this.sliderSaturationMax.Name = "sliderSaturationMax";
            this.sliderSaturationMax.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.sliderSaturationMax.Size = new System.Drawing.Size(194, 40);
            this.sliderSaturationMax.SmallChange = 1;
            this.sliderSaturationMax.TabIndex = 2;
            this.sliderSaturationMax.TickFrequency = 1;
            this.sliderSaturationMax.Value = 0;
            // 
            // sliderSaturationMin
            // 
            this.sliderSaturationMin.Dock = System.Windows.Forms.DockStyle.Top;
            this.sliderSaturationMin.Label = "Saturation Min";
            this.sliderSaturationMin.LargeChange = 5;
            this.sliderSaturationMin.Location = new System.Drawing.Point(0, 80);
            this.sliderSaturationMin.Maximum = 255;
            this.sliderSaturationMin.Minimum = 0;
            this.sliderSaturationMin.Name = "sliderSaturationMin";
            this.sliderSaturationMin.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.sliderSaturationMin.Size = new System.Drawing.Size(194, 40);
            this.sliderSaturationMin.SmallChange = 1;
            this.sliderSaturationMin.TabIndex = 3;
            this.sliderSaturationMin.TickFrequency = 1;
            this.sliderSaturationMin.Value = 0;
            // 
            // sliderHueMax
            // 
            this.sliderHueMax.Dock = System.Windows.Forms.DockStyle.Top;
            this.sliderHueMax.Label = "Hue Max";
            this.sliderHueMax.LargeChange = 5;
            this.sliderHueMax.Location = new System.Drawing.Point(0, 40);
            this.sliderHueMax.Maximum = 255;
            this.sliderHueMax.Minimum = 0;
            this.sliderHueMax.Name = "sliderHueMax";
            this.sliderHueMax.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.sliderHueMax.Size = new System.Drawing.Size(194, 40);
            this.sliderHueMax.SmallChange = 1;
            this.sliderHueMax.TabIndex = 0;
            this.sliderHueMax.TickFrequency = 1;
            this.sliderHueMax.Value = 0;
            // 
            // sliderHueMin
            // 
            this.sliderHueMin.Dock = System.Windows.Forms.DockStyle.Top;
            this.sliderHueMin.Label = "Hue Min";
            this.sliderHueMin.LargeChange = 5;
            this.sliderHueMin.Location = new System.Drawing.Point(0, 0);
            this.sliderHueMin.Maximum = 255;
            this.sliderHueMin.Minimum = 0;
            this.sliderHueMin.Name = "sliderHueMin";
            this.sliderHueMin.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.sliderHueMin.Size = new System.Drawing.Size(194, 40);
            this.sliderHueMin.SmallChange = 1;
            this.sliderHueMin.TabIndex = 1;
            this.sliderHueMin.TickFrequency = 1;
            this.sliderHueMin.Value = 0;
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Controls.Add(this.groupBoxCaptured);
            this.flowLayoutPanel.Controls.Add(this.groupBoxFiltered);
            this.flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel.Location = new System.Drawing.Point(194, 0);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(623, 731);
            this.flowLayoutPanel.TabIndex = 8;
            // 
            // groupBoxCaptured
            // 
            this.groupBoxCaptured.Controls.Add(this.imageBoxCaptured);
            this.groupBoxCaptured.Location = new System.Drawing.Point(3, 3);
            this.groupBoxCaptured.Name = "groupBoxCaptured";
            this.groupBoxCaptured.Size = new System.Drawing.Size(374, 383);
            this.groupBoxCaptured.TabIndex = 8;
            this.groupBoxCaptured.TabStop = false;
            this.groupBoxCaptured.Text = "Captured";
            // 
            // imageBoxCaptured
            // 
            this.imageBoxCaptured.BackColor = System.Drawing.SystemColors.Control;
            this.imageBoxCaptured.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBoxCaptured.Location = new System.Drawing.Point(3, 16);
            this.imageBoxCaptured.Name = "imageBoxCaptured";
            this.imageBoxCaptured.Size = new System.Drawing.Size(368, 364);
            this.imageBoxCaptured.TabIndex = 5;
            this.imageBoxCaptured.TabStop = false;
            this.imageBoxCaptured.OnZoomScaleChange += new System.EventHandler(this.imageBoxCaptured_OnZoomScaleChange);
            // 
            // groupBoxFiltered
            // 
            this.groupBoxFiltered.Controls.Add(this.imageBoxFiltered);
            this.groupBoxFiltered.Location = new System.Drawing.Point(3, 392);
            this.groupBoxFiltered.Name = "groupBoxFiltered";
            this.groupBoxFiltered.Size = new System.Drawing.Size(371, 373);
            this.groupBoxFiltered.TabIndex = 8;
            this.groupBoxFiltered.TabStop = false;
            this.groupBoxFiltered.Text = "Filtered";
            // 
            // imageBoxFiltered
            // 
            this.imageBoxFiltered.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBoxFiltered.Location = new System.Drawing.Point(3, 16);
            this.imageBoxFiltered.Name = "imageBoxFiltered";
            this.imageBoxFiltered.Size = new System.Drawing.Size(365, 354);
            this.imageBoxFiltered.TabIndex = 6;
            this.imageBoxFiltered.TabStop = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(18, 45);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(8, 21);
            this.progressBar1.TabIndex = 5;
            // 
            // spinDilateIterations
            // 
            this.spinDilateIterations.Location = new System.Drawing.Point(12, 49);
            this.spinDilateIterations.Name = "spinDilateIterations";
            this.spinDilateIterations.Size = new System.Drawing.Size(47, 20);
            this.spinDilateIterations.TabIndex = 10;
            this.spinDilateIterations.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ColourDetectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel);
            this.Controls.Add(this.panelLeft);
            this.Name = "ColourDetectionControl";
            this.Size = new System.Drawing.Size(817, 731);
            this.Load += new System.EventHandler(this.ColourDetectionControl_Load);
            this.panelLeft.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            this.groupBoxMoments.ResumeLayout(false);
            this.groupBoxRoi.ResumeLayout(false);
            this.groupBoxRoi.PerformLayout();
            this.groupBoxPresets.ResumeLayout(false);
            this.flowLayoutPanel.ResumeLayout(false);
            this.groupBoxCaptured.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCaptured)).EndInit();
            this.groupBoxFiltered.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxFiltered)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDilateIterations)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelLeft;
        private UserControls.SliderControl sliderHueMin;
        private UserControls.SliderControl sliderHueMax;
        private UserControls.SliderControl sliderValueMin;
        private UserControls.SliderControl sliderValueMax;
        private UserControls.SliderControl sliderSaturationMin;
        private UserControls.SliderControl sliderSaturationMax;
        private System.Windows.Forms.GroupBox groupBoxPresets;
        private System.Windows.Forms.Button btnRedDaylight;
        private System.Windows.Forms.Button btnRedLights;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.GroupBox groupBoxCaptured;
        private Emgu.CV.UI.ImageBox imageBoxCaptured;
        private System.Windows.Forms.GroupBox groupBoxFiltered;
        private Emgu.CV.UI.ImageBox imageBoxFiltered;
        private System.Windows.Forms.GroupBox groupBoxRoi;
        private UserControls.SliderControl sliderRoiTop;
        private UserControls.SliderControl sliderRoiRight;
        private UserControls.SliderControl sliderRoiLeft;
        private UserControls.SliderControl sliderRoiBottom;
        private System.Windows.Forms.CheckBox checkBoxRoi;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnWriteSettingsForConsole;
        private System.Windows.Forms.GroupBox groupBoxMoments;
        private UserControls.SliderControl sliderMomentAreaMax;
        private UserControls.SliderControl sliderMomentAreaMin;
        private System.Windows.Forms.Button btnReadSettings;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.NumericUpDown spinDilateIterations;
    }
}
