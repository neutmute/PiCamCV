namespace PiCamCV.WinForms.CameraConsumers
{
    partial class PanTiltCalibrationControl
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

        private System.Windows.Forms.GroupBox groupBoxMoveTo;
        private System.Windows.Forms.Button btnGoto;
        private System.Windows.Forms.TextBox txtTiltPercent;
        private System.Windows.Forms.TextBox txtPanPercent;
        private System.Windows.Forms.Label labelTilt;
        private System.Windows.Forms.Label labelPan;
        private System.Windows.Forms.GroupBox groupBoxCaptured;
        private Emgu.CV.UI.ImageBox imageBoxCaptured;
        private System.Windows.Forms.GroupBox groupBoxControls;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBoxCaptured = new System.Windows.Forms.GroupBox();
            this.imageBoxCaptured = new Emgu.CV.UI.ImageBox();
            this.groupBoxControls = new System.Windows.Forms.GroupBox();
            this.groupBoxMoveTo = new System.Windows.Forms.GroupBox();
            this.btnGoto = new System.Windows.Forms.Button();
            this.txtTiltPercent = new System.Windows.Forms.TextBox();
            this.txtPanPercent = new System.Windows.Forms.TextBox();
            this.labelTilt = new System.Windows.Forms.Label();
            this.labelPan = new System.Windows.Forms.Label();
            this.groupBoxReticle = new System.Windows.Forms.GroupBox();
            this.txtReticleY = new System.Windows.Forms.TextBox();
            this.txtReticleX = new System.Windows.Forms.TextBox();
            this.labelReticleY = new System.Windows.Forms.Label();
            this.labelReticleX = new System.Windows.Forms.Label();
            this.btnPaintReticle = new System.Windows.Forms.Button();
            this.groupBoxCaptured.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCaptured)).BeginInit();
            this.groupBoxControls.SuspendLayout();
            this.groupBoxMoveTo.SuspendLayout();
            this.groupBoxReticle.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxCaptured
            // 
            this.groupBoxCaptured.Controls.Add(this.imageBoxCaptured);
            this.groupBoxCaptured.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxCaptured.Location = new System.Drawing.Point(200, 0);
            this.groupBoxCaptured.Name = "groupBoxCaptured";
            this.groupBoxCaptured.Size = new System.Drawing.Size(428, 439);
            this.groupBoxCaptured.TabIndex = 2;
            this.groupBoxCaptured.TabStop = false;
            this.groupBoxCaptured.Text = "Captured";
            // 
            // imageBoxCaptured
            // 
            this.imageBoxCaptured.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBoxCaptured.Location = new System.Drawing.Point(3, 16);
            this.imageBoxCaptured.Name = "imageBoxCaptured";
            this.imageBoxCaptured.Size = new System.Drawing.Size(422, 420);
            this.imageBoxCaptured.TabIndex = 4;
            this.imageBoxCaptured.TabStop = false;
            // 
            // groupBoxControls
            // 
            this.groupBoxControls.Controls.Add(this.groupBoxReticle);
            this.groupBoxControls.Controls.Add(this.groupBoxMoveTo);
            this.groupBoxControls.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBoxControls.Location = new System.Drawing.Point(0, 0);
            this.groupBoxControls.Name = "groupBoxControls";
            this.groupBoxControls.Size = new System.Drawing.Size(200, 439);
            this.groupBoxControls.TabIndex = 1;
            this.groupBoxControls.TabStop = false;
            this.groupBoxControls.Text = "Controls";
            // 
            // groupBoxMoveTo
            // 
            this.groupBoxMoveTo.Controls.Add(this.btnGoto);
            this.groupBoxMoveTo.Controls.Add(this.txtTiltPercent);
            this.groupBoxMoveTo.Controls.Add(this.txtPanPercent);
            this.groupBoxMoveTo.Controls.Add(this.labelTilt);
            this.groupBoxMoveTo.Controls.Add(this.labelPan);
            this.groupBoxMoveTo.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxMoveTo.Location = new System.Drawing.Point(3, 16);
            this.groupBoxMoveTo.Name = "groupBoxMoveTo";
            this.groupBoxMoveTo.Size = new System.Drawing.Size(194, 100);
            this.groupBoxMoveTo.TabIndex = 4;
            this.groupBoxMoveTo.TabStop = false;
            this.groupBoxMoveTo.Text = "Goto";
            // 
            // btnGoto
            // 
            this.btnGoto.Location = new System.Drawing.Point(45, 70);
            this.btnGoto.Name = "btnGoto";
            this.btnGoto.Size = new System.Drawing.Size(84, 23);
            this.btnGoto.TabIndex = 8;
            this.btnGoto.Text = "Go";
            this.btnGoto.UseVisualStyleBackColor = true;
            this.btnGoto.Click += new System.EventHandler(this.btnGoto_Click);
            // 
            // txtTiltPercent
            // 
            this.txtTiltPercent.Location = new System.Drawing.Point(45, 44);
            this.txtTiltPercent.Name = "txtTiltPercent";
            this.txtTiltPercent.Size = new System.Drawing.Size(84, 20);
            this.txtTiltPercent.TabIndex = 7;
            // 
            // txtPanPercent
            // 
            this.txtPanPercent.Location = new System.Drawing.Point(45, 19);
            this.txtPanPercent.Name = "txtPanPercent";
            this.txtPanPercent.Size = new System.Drawing.Size(84, 20);
            this.txtPanPercent.TabIndex = 6;
            // 
            // labelTilt
            // 
            this.labelTilt.AutoSize = true;
            this.labelTilt.Location = new System.Drawing.Point(5, 47);
            this.labelTilt.Name = "labelTilt";
            this.labelTilt.Size = new System.Drawing.Size(21, 13);
            this.labelTilt.TabIndex = 5;
            this.labelTilt.Text = "Tilt";
            // 
            // labelPan
            // 
            this.labelPan.AutoSize = true;
            this.labelPan.Location = new System.Drawing.Point(5, 22);
            this.labelPan.Name = "labelPan";
            this.labelPan.Size = new System.Drawing.Size(26, 13);
            this.labelPan.TabIndex = 4;
            this.labelPan.Text = "Pan";
            // 
            // groupBoxReticle
            // 
            this.groupBoxReticle.Controls.Add(this.btnPaintReticle);
            this.groupBoxReticle.Controls.Add(this.txtReticleY);
            this.groupBoxReticle.Controls.Add(this.txtReticleX);
            this.groupBoxReticle.Controls.Add(this.labelReticleX);
            this.groupBoxReticle.Controls.Add(this.labelReticleY);
            this.groupBoxReticle.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxReticle.Location = new System.Drawing.Point(3, 116);
            this.groupBoxReticle.Name = "groupBoxReticle";
            this.groupBoxReticle.Size = new System.Drawing.Size(194, 115);
            this.groupBoxReticle.TabIndex = 5;
            this.groupBoxReticle.TabStop = false;
            this.groupBoxReticle.Text = "Reticle";
            // 
            // txtReticleY
            // 
            this.txtReticleY.Location = new System.Drawing.Point(45, 48);
            this.txtReticleY.Name = "txtReticleY";
            this.txtReticleY.Size = new System.Drawing.Size(84, 20);
            this.txtReticleY.TabIndex = 12;
            // 
            // txtReticleX
            // 
            this.txtReticleX.Location = new System.Drawing.Point(45, 23);
            this.txtReticleX.Name = "txtReticleX";
            this.txtReticleX.Size = new System.Drawing.Size(84, 20);
            this.txtReticleX.TabIndex = 11;
            // 
            // labelReticleY
            // 
            this.labelReticleY.AutoSize = true;
            this.labelReticleY.Location = new System.Drawing.Point(5, 51);
            this.labelReticleY.Name = "labelReticleY";
            this.labelReticleY.Size = new System.Drawing.Size(12, 13);
            this.labelReticleY.TabIndex = 10;
            this.labelReticleY.Text = "y";
            // 
            // labelReticleX
            // 
            this.labelReticleX.AutoSize = true;
            this.labelReticleX.Location = new System.Drawing.Point(5, 26);
            this.labelReticleX.Name = "labelReticleX";
            this.labelReticleX.Size = new System.Drawing.Size(12, 13);
            this.labelReticleX.TabIndex = 9;
            this.labelReticleX.Text = "x";
            // 
            // btnPaintReticle
            // 
            this.btnPaintReticle.Location = new System.Drawing.Point(45, 71);
            this.btnPaintReticle.Name = "btnPaintReticle";
            this.btnPaintReticle.Size = new System.Drawing.Size(84, 23);
            this.btnPaintReticle.TabIndex = 13;
            this.btnPaintReticle.Text = "Paint";
            this.btnPaintReticle.UseVisualStyleBackColor = true;
            this.btnPaintReticle.Click += new System.EventHandler(this.btnPaintReticle_Click);
            // 
            // PanTiltCalibrationControl
            // 
            this.Controls.Add(this.groupBoxCaptured);
            this.Controls.Add(this.groupBoxControls);
            this.Name = "PanTiltCalibrationControl";
            this.Size = new System.Drawing.Size(628, 439);
            this.groupBoxCaptured.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCaptured)).EndInit();
            this.groupBoxControls.ResumeLayout(false);
            this.groupBoxMoveTo.ResumeLayout(false);
            this.groupBoxMoveTo.PerformLayout();
            this.groupBoxReticle.ResumeLayout(false);
            this.groupBoxReticle.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.GroupBox groupBoxReticle;
        private System.Windows.Forms.Button btnPaintReticle;
        private System.Windows.Forms.TextBox txtReticleY;
        private System.Windows.Forms.TextBox txtReticleX;
        private System.Windows.Forms.Label labelReticleX;
        private System.Windows.Forms.Label labelReticleY;
    }
}
