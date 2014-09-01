namespace PiCamCV.WinForms
{
    partial class CameraCapture
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
            this.groupBoxCaptured = new System.Windows.Forms.GroupBox();
            this.imageBoxCaptured = new Emgu.CV.UI.ImageBox();
            this.groupBoxGray = new System.Windows.Forms.GroupBox();
            this.groupBoxSmoothed = new System.Windows.Forms.GroupBox();
            this.groupBoxCanny = new System.Windows.Forms.GroupBox();
            this.imageBoxCanny = new Emgu.CV.UI.ImageBox();
            this.imageBoxSmoothedGray = new Emgu.CV.UI.ImageBox();
            this.imageBoxGray = new Emgu.CV.UI.ImageBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.btnFlipVertical = new System.Windows.Forms.Button();
            this.btnFlipHorizontal = new System.Windows.Forms.Button();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.panelRight = new System.Windows.Forms.Panel();
            this.groupBoxCaptured.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCaptured)).BeginInit();
            this.groupBoxGray.SuspendLayout();
            this.groupBoxSmoothed.SuspendLayout();
            this.groupBoxCanny.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCanny)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxSmoothedGray)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxGray)).BeginInit();
            this.panel1.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxCaptured
            // 
            this.groupBoxCaptured.Controls.Add(this.imageBoxCaptured);
            this.groupBoxCaptured.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxCaptured.Location = new System.Drawing.Point(0, 0);
            this.groupBoxCaptured.Name = "groupBoxCaptured";
            this.groupBoxCaptured.Size = new System.Drawing.Size(355, 256);
            this.groupBoxCaptured.TabIndex = 0;
            this.groupBoxCaptured.TabStop = false;
            this.groupBoxCaptured.Text = "Captured Image";
            // 
            // imageBoxCaptured
            // 
            this.imageBoxCaptured.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBoxCaptured.Location = new System.Drawing.Point(3, 16);
            this.imageBoxCaptured.Name = "imageBoxCaptured";
            this.imageBoxCaptured.Size = new System.Drawing.Size(349, 237);
            this.imageBoxCaptured.TabIndex = 2;
            this.imageBoxCaptured.TabStop = false;
            // 
            // groupBoxGray
            // 
            this.groupBoxGray.Controls.Add(this.imageBoxGray);
            this.groupBoxGray.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxGray.Location = new System.Drawing.Point(0, 0);
            this.groupBoxGray.Name = "groupBoxGray";
            this.groupBoxGray.Size = new System.Drawing.Size(452, 256);
            this.groupBoxGray.TabIndex = 1;
            this.groupBoxGray.TabStop = false;
            this.groupBoxGray.Text = "Grayscale";
            // 
            // groupBoxSmoothed
            // 
            this.groupBoxSmoothed.Controls.Add(this.imageBoxSmoothedGray);
            this.groupBoxSmoothed.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBoxSmoothed.Location = new System.Drawing.Point(0, 262);
            this.groupBoxSmoothed.Name = "groupBoxSmoothed";
            this.groupBoxSmoothed.Size = new System.Drawing.Size(355, 295);
            this.groupBoxSmoothed.TabIndex = 2;
            this.groupBoxSmoothed.TabStop = false;
            this.groupBoxSmoothed.Text = "Smoothed Grayscale";
            // 
            // groupBoxCanny
            // 
            this.groupBoxCanny.Controls.Add(this.imageBoxCanny);
            this.groupBoxCanny.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBoxCanny.Location = new System.Drawing.Point(0, 262);
            this.groupBoxCanny.Name = "groupBoxCanny";
            this.groupBoxCanny.Size = new System.Drawing.Size(452, 295);
            this.groupBoxCanny.TabIndex = 2;
            this.groupBoxCanny.TabStop = false;
            this.groupBoxCanny.Text = "Canny";
            // 
            // imageBoxCanny
            // 
            this.imageBoxCanny.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBoxCanny.Location = new System.Drawing.Point(3, 16);
            this.imageBoxCanny.Name = "imageBoxCanny";
            this.imageBoxCanny.Size = new System.Drawing.Size(446, 276);
            this.imageBoxCanny.TabIndex = 2;
            this.imageBoxCanny.TabStop = false;
            // 
            // imageBoxSmoothedGray
            // 
            this.imageBoxSmoothedGray.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBoxSmoothedGray.Location = new System.Drawing.Point(3, 16);
            this.imageBoxSmoothedGray.Name = "imageBoxSmoothedGray";
            this.imageBoxSmoothedGray.Size = new System.Drawing.Size(349, 276);
            this.imageBoxSmoothedGray.TabIndex = 3;
            this.imageBoxSmoothedGray.TabStop = false;
            // 
            // imageBoxGray
            // 
            this.imageBoxGray.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBoxGray.Location = new System.Drawing.Point(3, 16);
            this.imageBoxGray.Name = "imageBoxGray";
            this.imageBoxGray.Size = new System.Drawing.Size(446, 237);
            this.imageBoxGray.TabIndex = 3;
            this.imageBoxGray.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnFlipHorizontal);
            this.panel1.Controls.Add(this.btnFlipVertical);
            this.panel1.Controls.Add(this.btnStartStop);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(843, 46);
            this.panel1.TabIndex = 3;
            // 
            // btnStartStop
            // 
            this.btnStartStop.Location = new System.Drawing.Point(15, 13);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(75, 23);
            this.btnStartStop.TabIndex = 0;
            this.btnStartStop.Text = "Start Capture";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // btnFlipVertical
            // 
            this.btnFlipVertical.Location = new System.Drawing.Point(117, 16);
            this.btnFlipVertical.Name = "btnFlipVertical";
            this.btnFlipVertical.Size = new System.Drawing.Size(80, 20);
            this.btnFlipVertical.TabIndex = 1;
            this.btnFlipVertical.Text = "Flip Vertical";
            this.btnFlipVertical.UseVisualStyleBackColor = true;
            this.btnFlipVertical.Click += new System.EventHandler(this.btnFlipVertical_Click);
            // 
            // btnFlipHorizontal
            // 
            this.btnFlipHorizontal.Location = new System.Drawing.Point(216, 15);
            this.btnFlipHorizontal.Name = "btnFlipHorizontal";
            this.btnFlipHorizontal.Size = new System.Drawing.Size(118, 23);
            this.btnFlipHorizontal.TabIndex = 2;
            this.btnFlipHorizontal.Text = "Flip Horizontal";
            this.btnFlipHorizontal.UseVisualStyleBackColor = true;
            this.btnFlipHorizontal.Click += new System.EventHandler(this.btnFlipHorizontal_Click);
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.groupBoxCaptured);
            this.panelLeft.Controls.Add(this.groupBoxSmoothed);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 46);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(355, 557);
            this.panelLeft.TabIndex = 4;
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.groupBoxGray);
            this.panelRight.Controls.Add(this.groupBoxCanny);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRight.Location = new System.Drawing.Point(391, 46);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(452, 557);
            this.panelRight.TabIndex = 5;
            // 
            // CameraCapture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.panel1);
            this.Name = "CameraCapture";
            this.Size = new System.Drawing.Size(843, 603);
            this.groupBoxCaptured.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCaptured)).EndInit();
            this.groupBoxGray.ResumeLayout(false);
            this.groupBoxSmoothed.ResumeLayout(false);
            this.groupBoxCanny.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCanny)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxSmoothedGray)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxGray)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxCaptured;
        private Emgu.CV.UI.ImageBox imageBoxCaptured;
        private System.Windows.Forms.GroupBox groupBoxGray;
        private Emgu.CV.UI.ImageBox imageBoxGray;
        private System.Windows.Forms.GroupBox groupBoxSmoothed;
        private Emgu.CV.UI.ImageBox imageBoxSmoothedGray;
        private System.Windows.Forms.GroupBox groupBoxCanny;
        private Emgu.CV.UI.ImageBox imageBoxCanny;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnFlipHorizontal;
        private System.Windows.Forms.Button btnFlipVertical;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelRight;
    }
}
