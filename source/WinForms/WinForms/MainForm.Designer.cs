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
            this.labelFrameRate = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.btnFlipHorizontal = new System.Windows.Forms.Button();
            this.btnFlipVertical = new System.Windows.Forms.Button();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageCameraCapture = new System.Windows.Forms.TabPage();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripLabelSettings = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabPageFaceDetection = new System.Windows.Forms.TabPage();
            this.tabPageHaarCascade = new System.Windows.Forms.TabPage();
            this.tabPageColourDetect = new System.Windows.Forms.TabPage();
            this.chkOpenCL = new System.Windows.Forms.CheckBox();
            this.panelTop.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageCameraCapture.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.chkOpenCL);
            this.panelTop.Controls.Add(this.labelFrameRate);
            this.panelTop.Controls.Add(this.labelStatus);
            this.panelTop.Controls.Add(this.btnFlipHorizontal);
            this.panelTop.Controls.Add(this.btnFlipVertical);
            this.panelTop.Controls.Add(this.btnStartStop);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(873, 63);
            this.panelTop.TabIndex = 1;
            // 
            // labelFrameRate
            // 
            this.labelFrameRate.AutoSize = true;
            this.labelFrameRate.Location = new System.Drawing.Point(392, 9);
            this.labelFrameRate.Name = "labelFrameRate";
            this.labelFrameRate.Size = new System.Drawing.Size(62, 13);
            this.labelFrameRate.TabIndex = 7;
            this.labelFrameRate.Text = "Frame Rate";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(392, 31);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(37, 13);
            this.labelStatus.TabIndex = 6;
            this.labelStatus.Text = "Status";
            // 
            // btnFlipHorizontal
            // 
            this.btnFlipHorizontal.Location = new System.Drawing.Point(131, 12);
            this.btnFlipHorizontal.Name = "btnFlipHorizontal";
            this.btnFlipHorizontal.Size = new System.Drawing.Size(118, 23);
            this.btnFlipHorizontal.TabIndex = 5;
            this.btnFlipHorizontal.Text = "Flip Horizontal";
            this.btnFlipHorizontal.UseVisualStyleBackColor = true;
            this.btnFlipHorizontal.Click += new System.EventHandler(this.btnFlipHorizontal_Click);
            // 
            // btnFlipVertical
            // 
            this.btnFlipVertical.Location = new System.Drawing.Point(255, 12);
            this.btnFlipVertical.Name = "btnFlipVertical";
            this.btnFlipVertical.Size = new System.Drawing.Size(118, 23);
            this.btnFlipVertical.TabIndex = 4;
            this.btnFlipVertical.Text = "Flip Vertical";
            this.btnFlipVertical.UseVisualStyleBackColor = true;
            this.btnFlipVertical.Click += new System.EventHandler(this.btnFlipVertical_Click);
            // 
            // btnStartStop
            // 
            this.btnStartStop.Location = new System.Drawing.Point(7, 12);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(118, 23);
            this.btnStartStop.TabIndex = 3;
            this.btnStartStop.Text = "Start Capture";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageCameraCapture);
            this.tabControlMain.Controls.Add(this.tabPageFaceDetection);
            this.tabControlMain.Controls.Add(this.tabPageHaarCascade);
            this.tabControlMain.Controls.Add(this.tabPageColourDetect);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 63);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(873, 477);
            this.tabControlMain.TabIndex = 2;
            // 
            // tabPageCameraCapture
            // 
            this.tabPageCameraCapture.Controls.Add(this.statusStrip1);
            this.tabPageCameraCapture.Location = new System.Drawing.Point(4, 22);
            this.tabPageCameraCapture.Name = "tabPageCameraCapture";
            this.tabPageCameraCapture.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCameraCapture.Size = new System.Drawing.Size(865, 451);
            this.tabPageCameraCapture.TabIndex = 0;
            this.tabPageCameraCapture.Text = "Camera Capture";
            this.tabPageCameraCapture.UseVisualStyleBackColor = true;
            this.tabPageCameraCapture.Click += new System.EventHandler(this.tabPageCameraCapture_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabelSettings});
            this.statusStrip1.Location = new System.Drawing.Point(3, 426);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(859, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripLabelSettings
            // 
            this.toolStripLabelSettings.Name = "toolStripLabelSettings";
            this.toolStripLabelSettings.Size = new System.Drawing.Size(0, 17);
            // 
            // tabPageFaceDetection
            // 
            this.tabPageFaceDetection.Location = new System.Drawing.Point(4, 22);
            this.tabPageFaceDetection.Name = "tabPageFaceDetection";
            this.tabPageFaceDetection.Size = new System.Drawing.Size(865, 467);
            this.tabPageFaceDetection.TabIndex = 1;
            this.tabPageFaceDetection.Text = "Face Detection";
            this.tabPageFaceDetection.UseVisualStyleBackColor = true;
            // 
            // tabPageHaarCascade
            // 
            this.tabPageHaarCascade.Location = new System.Drawing.Point(4, 22);
            this.tabPageHaarCascade.Name = "tabPageHaarCascade";
            this.tabPageHaarCascade.Size = new System.Drawing.Size(865, 467);
            this.tabPageHaarCascade.TabIndex = 3;
            this.tabPageHaarCascade.Text = "Haar Cascade Detection";
            this.tabPageHaarCascade.UseVisualStyleBackColor = true;
            // 
            // tabPageColourDetect
            // 
            this.tabPageColourDetect.Location = new System.Drawing.Point(4, 22);
            this.tabPageColourDetect.Name = "tabPageColourDetect";
            this.tabPageColourDetect.Size = new System.Drawing.Size(865, 467);
            this.tabPageColourDetect.TabIndex = 2;
            this.tabPageColourDetect.Text = "Colour Detection";
            this.tabPageColourDetect.UseVisualStyleBackColor = true;
            // 
            // chkOpenCL
            // 
            this.chkOpenCL.AutoSize = true;
            this.chkOpenCL.Location = new System.Drawing.Point(7, 40);
            this.chkOpenCL.Name = "chkOpenCL";
            this.chkOpenCL.Size = new System.Drawing.Size(87, 17);
            this.chkOpenCL.TabIndex = 8;
            this.chkOpenCL.Text = "Use OpenCL";
            this.chkOpenCL.UseVisualStyleBackColor = true;
            this.chkOpenCL.CheckedChanged += new System.EventHandler(this.chkOpenCL_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 540);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.panelTop);
            this.Name = "MainForm";
            this.Text = "PiCamCV WinForms";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.tabControlMain.ResumeLayout(false);
            this.tabPageCameraCapture.ResumeLayout(false);
            this.tabPageCameraCapture.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageCameraCapture;
        private System.Windows.Forms.TabPage tabPageFaceDetection;
        private System.Windows.Forms.Button btnFlipHorizontal;
        private System.Windows.Forms.Button btnFlipVertical;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.TabPage tabPageColourDetect;
        private System.Windows.Forms.TabPage tabPageHaarCascade;
        private System.Windows.Forms.Label labelFrameRate;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLabelSettings;
        private System.Windows.Forms.CheckBox chkOpenCL;

    }
}

