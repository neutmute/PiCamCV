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
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageCameraCapture = new System.Windows.Forms.TabPage();
            this.tabPageFaceDetection = new System.Windows.Forms.TabPage();
            this.btnFlipHorizontal = new System.Windows.Forms.Button();
            this.btnFlipVertical = new System.Windows.Forms.Button();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.labelStatus);
            this.panelTop.Controls.Add(this.btnFlipHorizontal);
            this.panelTop.Controls.Add(this.btnFlipVertical);
            this.panelTop.Controls.Add(this.btnStartStop);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(873, 47);
            this.panelTop.TabIndex = 1;
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageCameraCapture);
            this.tabControlMain.Controls.Add(this.tabPageFaceDetection);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 47);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(873, 493);
            this.tabControlMain.TabIndex = 2;
            // 
            // tabPageCameraCapture
            // 
            this.tabPageCameraCapture.Location = new System.Drawing.Point(4, 22);
            this.tabPageCameraCapture.Name = "tabPageCameraCapture";
            this.tabPageCameraCapture.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCameraCapture.Size = new System.Drawing.Size(865, 467);
            this.tabPageCameraCapture.TabIndex = 0;
            this.tabPageCameraCapture.Text = "Camera Capture";
            this.tabPageCameraCapture.UseVisualStyleBackColor = true;
            this.tabPageCameraCapture.Click += new System.EventHandler(this.tabPageCameraCapture_Click);
            // 
            // tabPageFaceDetection
            // 
            this.tabPageFaceDetection.Location = new System.Drawing.Point(4, 22);
            this.tabPageFaceDetection.Name = "tabPageFaceDetection";
            this.tabPageFaceDetection.Size = new System.Drawing.Size(865, 488);
            this.tabPageFaceDetection.TabIndex = 1;
            this.tabPageFaceDetection.Text = "Face Detection";
            this.tabPageFaceDetection.UseVisualStyleBackColor = true;
            // 
            // btnFlipHorizontal
            // 
            this.btnFlipHorizontal.Location = new System.Drawing.Point(188, 12);
            this.btnFlipHorizontal.Name = "btnFlipHorizontal";
            this.btnFlipHorizontal.Size = new System.Drawing.Size(118, 23);
            this.btnFlipHorizontal.TabIndex = 5;
            this.btnFlipHorizontal.Text = "Flip Horizontal";
            this.btnFlipHorizontal.UseVisualStyleBackColor = true;
            this.btnFlipHorizontal.Click += new System.EventHandler(this.btnFlipHorizontal_Click);
            // 
            // btnFlipVertical
            // 
            this.btnFlipVertical.Location = new System.Drawing.Point(93, 15);
            this.btnFlipVertical.Name = "btnFlipVertical";
            this.btnFlipVertical.Size = new System.Drawing.Size(80, 20);
            this.btnFlipVertical.TabIndex = 4;
            this.btnFlipVertical.Text = "Flip Vertical";
            this.btnFlipVertical.UseVisualStyleBackColor = true;
            this.btnFlipVertical.Click += new System.EventHandler(this.btnFlipVertical_Click);
            // 
            // btnStartStop
            // 
            this.btnStartStop.Location = new System.Drawing.Point(12, 12);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(75, 23);
            this.btnStartStop.TabIndex = 3;
            this.btnStartStop.Text = "Start Capture";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(643, 19);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(37, 13);
            this.labelStatus.TabIndex = 6;
            this.labelStatus.Text = "Status";
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

    }
}

