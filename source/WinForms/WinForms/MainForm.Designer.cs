namespace WinForms
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelTop = new System.Windows.Forms.Panel();
            this.groupBoxRealTime = new System.Windows.Forms.GroupBox();
            this.btnFlipVertical = new System.Windows.Forms.Button();
            this.btnFlipHorizontal = new System.Windows.Forms.Button();
            this.groupBoxPreCapture = new System.Windows.Forms.GroupBox();
            this.txtFramerate = new System.Windows.Forms.TextBox();
            this.lblFrames = new System.Windows.Forms.Label();
            this.txtBitrate = new System.Windows.Forms.TextBox();
            this.lblWidthHeight = new System.Windows.Forms.Label();
            this.lblBitrate = new System.Windows.Forms.Label();
            this.chkMonochrome = new System.Windows.Forms.CheckBox();
            this.cmbResolution = new System.Windows.Forms.ComboBox();
            this.radFile = new System.Windows.Forms.RadioButton();
            this.radCamera = new System.Windows.Forms.RadioButton();
            this.labelCameraIndex = new System.Windows.Forms.Label();
            this.spinEditCameraIndex = new System.Windows.Forms.NumericUpDown();
            this.chkOpenCL = new System.Windows.Forms.CheckBox();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageCameraCapture = new System.Windows.Forms.TabPage();
            this.tabPageFaceDetection = new System.Windows.Forms.TabPage();
            this.tabPageHaarCascade = new System.Windows.Forms.TabPage();
            this.tabPageColourDetect = new System.Windows.Forms.TabPage();
            this.tabPageShapes = new System.Windows.Forms.TabPage();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripLabelSettings = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripLabelFrames = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripLabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabPagePanTiltCalibration = new System.Windows.Forms.TabPage();
            this.panelTop.SuspendLayout();
            this.groupBoxRealTime.SuspendLayout();
            this.groupBoxPreCapture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditCameraIndex)).BeginInit();
            this.tabControlMain.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.groupBoxRealTime);
            this.panelTop.Controls.Add(this.groupBoxPreCapture);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1192, 79);
            this.panelTop.TabIndex = 1;
            // 
            // groupBoxRealTime
            // 
            this.groupBoxRealTime.Controls.Add(this.btnFlipVertical);
            this.groupBoxRealTime.Controls.Add(this.btnFlipHorizontal);
            this.groupBoxRealTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxRealTime.Location = new System.Drawing.Point(710, 0);
            this.groupBoxRealTime.Name = "groupBoxRealTime";
            this.groupBoxRealTime.Size = new System.Drawing.Size(482, 79);
            this.groupBoxRealTime.TabIndex = 12;
            this.groupBoxRealTime.TabStop = false;
            this.groupBoxRealTime.Text = "Mid Capture";
            // 
            // btnFlipVertical
            // 
            this.btnFlipVertical.Location = new System.Drawing.Point(130, 19);
            this.btnFlipVertical.Name = "btnFlipVertical";
            this.btnFlipVertical.Size = new System.Drawing.Size(118, 23);
            this.btnFlipVertical.TabIndex = 7;
            this.btnFlipVertical.Text = "Flip Vertical";
            this.btnFlipVertical.UseVisualStyleBackColor = true;
            this.btnFlipVertical.Click += new System.EventHandler(this.btnFlipVertical_Click);
            // 
            // btnFlipHorizontal
            // 
            this.btnFlipHorizontal.Location = new System.Drawing.Point(6, 19);
            this.btnFlipHorizontal.Name = "btnFlipHorizontal";
            this.btnFlipHorizontal.Size = new System.Drawing.Size(118, 23);
            this.btnFlipHorizontal.TabIndex = 6;
            this.btnFlipHorizontal.Text = "Flip Horizontal";
            this.btnFlipHorizontal.UseVisualStyleBackColor = true;
            this.btnFlipHorizontal.Click += new System.EventHandler(this.btnFlipHorizontal_Click);
            // 
            // groupBoxPreCapture
            // 
            this.groupBoxPreCapture.Controls.Add(this.txtFramerate);
            this.groupBoxPreCapture.Controls.Add(this.lblFrames);
            this.groupBoxPreCapture.Controls.Add(this.txtBitrate);
            this.groupBoxPreCapture.Controls.Add(this.lblWidthHeight);
            this.groupBoxPreCapture.Controls.Add(this.lblBitrate);
            this.groupBoxPreCapture.Controls.Add(this.chkMonochrome);
            this.groupBoxPreCapture.Controls.Add(this.cmbResolution);
            this.groupBoxPreCapture.Controls.Add(this.radFile);
            this.groupBoxPreCapture.Controls.Add(this.radCamera);
            this.groupBoxPreCapture.Controls.Add(this.labelCameraIndex);
            this.groupBoxPreCapture.Controls.Add(this.spinEditCameraIndex);
            this.groupBoxPreCapture.Controls.Add(this.chkOpenCL);
            this.groupBoxPreCapture.Controls.Add(this.btnStartStop);
            this.groupBoxPreCapture.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBoxPreCapture.Location = new System.Drawing.Point(0, 0);
            this.groupBoxPreCapture.Name = "groupBoxPreCapture";
            this.groupBoxPreCapture.Size = new System.Drawing.Size(710, 79);
            this.groupBoxPreCapture.TabIndex = 11;
            this.groupBoxPreCapture.TabStop = false;
            this.groupBoxPreCapture.Text = "Setup";
            // 
            // txtFramerate
            // 
            this.txtFramerate.Location = new System.Drawing.Point(403, 18);
            this.txtFramerate.Name = "txtFramerate";
            this.txtFramerate.Size = new System.Drawing.Size(40, 20);
            this.txtFramerate.TabIndex = 22;
            this.txtFramerate.Text = "25";
            // 
            // lblFrames
            // 
            this.lblFrames.AutoSize = true;
            this.lblFrames.Location = new System.Drawing.Point(346, 21);
            this.lblFrames.Name = "lblFrames";
            this.lblFrames.Size = new System.Drawing.Size(51, 13);
            this.lblFrames.TabIndex = 21;
            this.lblFrames.Text = "Frames/s";
            // 
            // txtBitrate
            // 
            this.txtBitrate.Location = new System.Drawing.Point(250, 43);
            this.txtBitrate.Name = "txtBitrate";
            this.txtBitrate.Size = new System.Drawing.Size(87, 20);
            this.txtBitrate.TabIndex = 20;
            this.txtBitrate.Text = "17000000";
            // 
            // lblWidthHeight
            // 
            this.lblWidthHeight.AutoSize = true;
            this.lblWidthHeight.Location = new System.Drawing.Point(200, 21);
            this.lblWidthHeight.Name = "lblWidthHeight";
            this.lblWidthHeight.Size = new System.Drawing.Size(44, 13);
            this.lblWidthHeight.TabIndex = 19;
            this.lblWidthHeight.Text = "(w) x (h)";
            // 
            // lblBitrate
            // 
            this.lblBitrate.AutoSize = true;
            this.lblBitrate.Location = new System.Drawing.Point(200, 46);
            this.lblBitrate.Name = "lblBitrate";
            this.lblBitrate.Size = new System.Drawing.Size(37, 13);
            this.lblBitrate.TabIndex = 18;
            this.lblBitrate.Text = "Bitrate";
            // 
            // chkMonochrome
            // 
            this.chkMonochrome.AutoSize = true;
            this.chkMonochrome.Location = new System.Drawing.Point(465, 40);
            this.chkMonochrome.Name = "chkMonochrome";
            this.chkMonochrome.Size = new System.Drawing.Size(88, 17);
            this.chkMonochrome.TabIndex = 17;
            this.chkMonochrome.Text = "Monochrome";
            this.chkMonochrome.UseVisualStyleBackColor = true;
            // 
            // cmbResolution
            // 
            this.cmbResolution.FormattingEnabled = true;
            this.cmbResolution.Items.AddRange(new object[] {
            "160x120",
            "320x240",
            "640x480",
            "960x720"});
            this.cmbResolution.Location = new System.Drawing.Point(250, 18);
            this.cmbResolution.Name = "cmbResolution";
            this.cmbResolution.Size = new System.Drawing.Size(87, 21);
            this.cmbResolution.TabIndex = 16;
            // 
            // radFile
            // 
            this.radFile.AutoSize = true;
            this.radFile.Location = new System.Drawing.Point(3, 40);
            this.radFile.Name = "radFile";
            this.radFile.Size = new System.Drawing.Size(41, 17);
            this.radFile.TabIndex = 15;
            this.radFile.TabStop = true;
            this.radFile.Text = "File";
            this.radFile.UseVisualStyleBackColor = true;
            this.radFile.CheckedChanged += new System.EventHandler(this.radFile_CheckedChanged);
            // 
            // radCamera
            // 
            this.radCamera.AutoSize = true;
            this.radCamera.Location = new System.Drawing.Point(3, 19);
            this.radCamera.Name = "radCamera";
            this.radCamera.Size = new System.Drawing.Size(61, 17);
            this.radCamera.TabIndex = 14;
            this.radCamera.TabStop = true;
            this.radCamera.Text = "Camera";
            this.radCamera.UseVisualStyleBackColor = true;
            // 
            // labelCameraIndex
            // 
            this.labelCameraIndex.AutoSize = true;
            this.labelCameraIndex.Location = new System.Drawing.Point(70, 21);
            this.labelCameraIndex.Name = "labelCameraIndex";
            this.labelCameraIndex.Size = new System.Drawing.Size(72, 13);
            this.labelCameraIndex.TabIndex = 12;
            this.labelCameraIndex.Text = "Camera Index";
            // 
            // spinEditCameraIndex
            // 
            this.spinEditCameraIndex.Location = new System.Drawing.Point(148, 19);
            this.spinEditCameraIndex.Name = "spinEditCameraIndex";
            this.spinEditCameraIndex.Size = new System.Drawing.Size(30, 20);
            this.spinEditCameraIndex.TabIndex = 11;
            // 
            // chkOpenCL
            // 
            this.chkOpenCL.AutoSize = true;
            this.chkOpenCL.Location = new System.Drawing.Point(465, 20);
            this.chkOpenCL.Name = "chkOpenCL";
            this.chkOpenCL.Size = new System.Drawing.Size(87, 17);
            this.chkOpenCL.TabIndex = 9;
            this.chkOpenCL.Text = "Use OpenCL";
            this.chkOpenCL.UseVisualStyleBackColor = true;
            this.chkOpenCL.CheckedChanged += new System.EventHandler(this.chkOpenCL_CheckedChanged);
            // 
            // btnStartStop
            // 
            this.btnStartStop.Location = new System.Drawing.Point(582, 16);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(118, 23);
            this.btnStartStop.TabIndex = 4;
            this.btnStartStop.Text = "Start Capture";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageCameraCapture);
            this.tabControlMain.Controls.Add(this.tabPageFaceDetection);
            this.tabControlMain.Controls.Add(this.tabPageHaarCascade);
            this.tabControlMain.Controls.Add(this.tabPageColourDetect);
            this.tabControlMain.Controls.Add(this.tabPageShapes);
            this.tabControlMain.Controls.Add(this.tabPagePanTiltCalibration);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 79);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(1192, 461);
            this.tabControlMain.TabIndex = 3;
            // 
            // tabPageCameraCapture
            // 
            this.tabPageCameraCapture.Location = new System.Drawing.Point(4, 22);
            this.tabPageCameraCapture.Name = "tabPageCameraCapture";
            this.tabPageCameraCapture.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCameraCapture.Size = new System.Drawing.Size(1184, 435);
            this.tabPageCameraCapture.TabIndex = 0;
            this.tabPageCameraCapture.Text = "Camera Capture";
            this.tabPageCameraCapture.UseVisualStyleBackColor = true;
            // 
            // tabPageFaceDetection
            // 
            this.tabPageFaceDetection.Location = new System.Drawing.Point(4, 22);
            this.tabPageFaceDetection.Name = "tabPageFaceDetection";
            this.tabPageFaceDetection.Size = new System.Drawing.Size(1184, 435);
            this.tabPageFaceDetection.TabIndex = 1;
            this.tabPageFaceDetection.Text = "Face Detection";
            this.tabPageFaceDetection.UseVisualStyleBackColor = true;
            // 
            // tabPageHaarCascade
            // 
            this.tabPageHaarCascade.Location = new System.Drawing.Point(4, 22);
            this.tabPageHaarCascade.Name = "tabPageHaarCascade";
            this.tabPageHaarCascade.Size = new System.Drawing.Size(1184, 435);
            this.tabPageHaarCascade.TabIndex = 3;
            this.tabPageHaarCascade.Text = "Haar Cascade Detection";
            this.tabPageHaarCascade.UseVisualStyleBackColor = true;
            // 
            // tabPageColourDetect
            // 
            this.tabPageColourDetect.Location = new System.Drawing.Point(4, 22);
            this.tabPageColourDetect.Name = "tabPageColourDetect";
            this.tabPageColourDetect.Size = new System.Drawing.Size(1184, 435);
            this.tabPageColourDetect.TabIndex = 2;
            this.tabPageColourDetect.Text = "Colour Detection";
            this.tabPageColourDetect.UseVisualStyleBackColor = true;
            // 
            // tabPageShapes
            // 
            this.tabPageShapes.Location = new System.Drawing.Point(4, 22);
            this.tabPageShapes.Name = "tabPageShapes";
            this.tabPageShapes.Size = new System.Drawing.Size(1184, 435);
            this.tabPageShapes.TabIndex = 4;
            this.tabPageShapes.Text = "Shape Detection";
            this.tabPageShapes.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabelSettings,
            this.toolStripLabelFrames,
            this.toolStripLabelStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 516);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1192, 24);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripLabelSettings
            // 
            this.toolStripLabelSettings.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.toolStripLabelSettings.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.toolStripLabelSettings.Name = "toolStripLabelSettings";
            this.toolStripLabelSettings.Size = new System.Drawing.Size(60, 19);
            this.toolStripLabelSettings.Text = "(settings)";
            // 
            // toolStripLabelFrames
            // 
            this.toolStripLabelFrames.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripLabelFrames.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.toolStripLabelFrames.Name = "toolStripLabelFrames";
            this.toolStripLabelFrames.Size = new System.Drawing.Size(55, 19);
            this.toolStripLabelFrames.Text = "(frames)";
            // 
            // toolStripLabelStatus
            // 
            this.toolStripLabelStatus.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.toolStripLabelStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripLabelStatus.Name = "toolStripLabelStatus";
            this.toolStripLabelStatus.Size = new System.Drawing.Size(46, 19);
            this.toolStripLabelStatus.Text = "(status)";
            // 
            // tabPagePanTiltCalibration
            // 
            this.tabPagePanTiltCalibration.Location = new System.Drawing.Point(4, 22);
            this.tabPagePanTiltCalibration.Name = "tabPagePanTiltCalibration";
            this.tabPagePanTiltCalibration.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePanTiltCalibration.Size = new System.Drawing.Size(1184, 435);
            this.tabPagePanTiltCalibration.TabIndex = 5;
            this.tabPagePanTiltCalibration.Text = "Pan Tilt Calibration";
            this.tabPagePanTiltCalibration.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 540);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.panelTop);
            this.Name = "MainForm";
            this.Text = "PiCamCV WinForms";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelTop.ResumeLayout(false);
            this.groupBoxRealTime.ResumeLayout(false);
            this.groupBoxPreCapture.ResumeLayout(false);
            this.groupBoxPreCapture.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditCameraIndex)).EndInit();
            this.tabControlMain.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.GroupBox groupBoxRealTime;
        private System.Windows.Forms.Button btnFlipVertical;
        private System.Windows.Forms.Button btnFlipHorizontal;
        private System.Windows.Forms.GroupBox groupBoxPreCapture;
        private System.Windows.Forms.Label labelCameraIndex;
        private System.Windows.Forms.NumericUpDown spinEditCameraIndex;
        private System.Windows.Forms.CheckBox chkOpenCL;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.RadioButton radFile;
        private System.Windows.Forms.RadioButton radCamera;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageCameraCapture;
        private System.Windows.Forms.TabPage tabPageFaceDetection;
        private System.Windows.Forms.TabPage tabPageHaarCascade;
        private System.Windows.Forms.TabPage tabPageColourDetect;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLabelSettings;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLabelFrames;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLabelStatus;
        private System.Windows.Forms.TabPage tabPageShapes;
        private System.Windows.Forms.CheckBox chkMonochrome;
        private System.Windows.Forms.ComboBox cmbResolution;
        private System.Windows.Forms.TextBox txtBitrate;
        private System.Windows.Forms.Label lblWidthHeight;
        private System.Windows.Forms.Label lblBitrate;
        private System.Windows.Forms.TextBox txtFramerate;
        private System.Windows.Forms.Label lblFrames;
        private System.Windows.Forms.TabPage tabPagePanTiltCalibration;

    }
}

