namespace PiCamCV.WinForms.CameraConsumers
{
    partial class PanTiltControl
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
            this.panelScreen = new System.Windows.Forms.Panel();
            this.txtScreen = new System.Windows.Forms.TextBox();
            this.groupBoxControllers = new System.Windows.Forms.GroupBox();
            this.btnCalibrate = new System.Windows.Forms.Button();
            this.chkBoxColourTracking = new System.Windows.Forms.CheckBox();
            this.chkBoxFaceTracker = new System.Windows.Forms.CheckBox();
            this.groupBoxReticle = new System.Windows.Forms.GroupBox();
            this.btnPaintReticle = new System.Windows.Forms.Button();
            this.txtReticleY = new System.Windows.Forms.TextBox();
            this.txtReticleX = new System.Windows.Forms.TextBox();
            this.labelReticleX = new System.Windows.Forms.Label();
            this.labelReticleY = new System.Windows.Forms.Label();
            this.groupBoxMoveTo = new System.Windows.Forms.GroupBox();
            this.btnGoto = new System.Windows.Forms.Button();
            this.txtTiltPercent = new System.Windows.Forms.TextBox();
            this.txtPanPercent = new System.Windows.Forms.TextBox();
            this.labelTilt = new System.Windows.Forms.Label();
            this.labelPan = new System.Windows.Forms.Label();
            this.groupBoxCaptured.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCaptured)).BeginInit();
            this.groupBoxControls.SuspendLayout();
            this.panelScreen.SuspendLayout();
            this.groupBoxControllers.SuspendLayout();
            this.groupBoxReticle.SuspendLayout();
            this.groupBoxMoveTo.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxCaptured
            // 
            this.groupBoxCaptured.Controls.Add(this.imageBoxCaptured);
            this.groupBoxCaptured.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxCaptured.Location = new System.Drawing.Point(271, 0);
            this.groupBoxCaptured.Name = "groupBoxCaptured";
            this.groupBoxCaptured.Size = new System.Drawing.Size(357, 439);
            this.groupBoxCaptured.TabIndex = 2;
            this.groupBoxCaptured.TabStop = false;
            this.groupBoxCaptured.Text = "Captured";
            // 
            // imageBoxCaptured
            // 
            this.imageBoxCaptured.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBoxCaptured.Location = new System.Drawing.Point(3, 16);
            this.imageBoxCaptured.Name = "imageBoxCaptured";
            this.imageBoxCaptured.Size = new System.Drawing.Size(351, 420);
            this.imageBoxCaptured.TabIndex = 4;
            this.imageBoxCaptured.TabStop = false;
            // 
            // groupBoxControls
            // 
            this.groupBoxControls.Controls.Add(this.panelScreen);
            this.groupBoxControls.Controls.Add(this.groupBoxControllers);
            this.groupBoxControls.Controls.Add(this.groupBoxReticle);
            this.groupBoxControls.Controls.Add(this.groupBoxMoveTo);
            this.groupBoxControls.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBoxControls.Location = new System.Drawing.Point(0, 0);
            this.groupBoxControls.Name = "groupBoxControls";
            this.groupBoxControls.Size = new System.Drawing.Size(271, 439);
            this.groupBoxControls.TabIndex = 1;
            this.groupBoxControls.TabStop = false;
            // 
            // panelScreen
            // 
            this.panelScreen.Controls.Add(this.txtScreen);
            this.panelScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelScreen.Location = new System.Drawing.Point(3, 306);
            this.panelScreen.Name = "panelScreen";
            this.panelScreen.Size = new System.Drawing.Size(265, 130);
            this.panelScreen.TabIndex = 7;
            // 
            // txtScreen
            // 
            this.txtScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtScreen.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtScreen.Location = new System.Drawing.Point(0, 0);
            this.txtScreen.Multiline = true;
            this.txtScreen.Name = "txtScreen";
            this.txtScreen.Size = new System.Drawing.Size(265, 130);
            this.txtScreen.TabIndex = 0;
            // 
            // groupBoxControllers
            // 
            this.groupBoxControllers.Controls.Add(this.btnCalibrate);
            this.groupBoxControllers.Controls.Add(this.chkBoxColourTracking);
            this.groupBoxControllers.Controls.Add(this.chkBoxFaceTracker);
            this.groupBoxControllers.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxControllers.Location = new System.Drawing.Point(3, 225);
            this.groupBoxControllers.Name = "groupBoxControllers";
            this.groupBoxControllers.Size = new System.Drawing.Size(265, 81);
            this.groupBoxControllers.TabIndex = 6;
            this.groupBoxControllers.TabStop = false;
            this.groupBoxControllers.Text = "Controllers";
            // 
            // btnCalibrate
            // 
            this.btnCalibrate.Location = new System.Drawing.Point(115, 15);
            this.btnCalibrate.Name = "btnCalibrate";
            this.btnCalibrate.Size = new System.Drawing.Size(75, 23);
            this.btnCalibrate.TabIndex = 2;
            this.btnCalibrate.Text = "Calibrate";
            this.btnCalibrate.UseVisualStyleBackColor = true;
            this.btnCalibrate.Click += new System.EventHandler(this.btnCalibrate_Click);
            // 
            // chkBoxColourTracking
            // 
            this.chkBoxColourTracking.AutoSize = true;
            this.chkBoxColourTracking.Location = new System.Drawing.Point(8, 19);
            this.chkBoxColourTracking.Name = "chkBoxColourTracking";
            this.chkBoxColourTracking.Size = new System.Drawing.Size(101, 17);
            this.chkBoxColourTracking.TabIndex = 1;
            this.chkBoxColourTracking.Text = "Colour Tracking";
            this.chkBoxColourTracking.UseVisualStyleBackColor = true;
            // 
            // chkBoxFaceTracker
            // 
            this.chkBoxFaceTracker.AutoSize = true;
            this.chkBoxFaceTracker.Location = new System.Drawing.Point(8, 42);
            this.chkBoxFaceTracker.Name = "chkBoxFaceTracker";
            this.chkBoxFaceTracker.Size = new System.Drawing.Size(95, 17);
            this.chkBoxFaceTracker.TabIndex = 0;
            this.chkBoxFaceTracker.Text = "Face Tracking";
            this.chkBoxFaceTracker.UseVisualStyleBackColor = true;
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
            this.groupBoxReticle.Size = new System.Drawing.Size(265, 109);
            this.groupBoxReticle.TabIndex = 5;
            this.groupBoxReticle.TabStop = false;
            this.groupBoxReticle.Text = "Calibration Reticle";
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
            // labelReticleX
            // 
            this.labelReticleX.AutoSize = true;
            this.labelReticleX.Location = new System.Drawing.Point(5, 26);
            this.labelReticleX.Name = "labelReticleX";
            this.labelReticleX.Size = new System.Drawing.Size(12, 13);
            this.labelReticleX.TabIndex = 9;
            this.labelReticleX.Text = "x";
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
            this.groupBoxMoveTo.Size = new System.Drawing.Size(265, 100);
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
            // PanTiltControl
            // 
            this.Controls.Add(this.groupBoxCaptured);
            this.Controls.Add(this.groupBoxControls);
            this.Name = "PanTiltControl";
            this.Size = new System.Drawing.Size(628, 439);
            this.groupBoxCaptured.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCaptured)).EndInit();
            this.groupBoxControls.ResumeLayout(false);
            this.panelScreen.ResumeLayout(false);
            this.panelScreen.PerformLayout();
            this.groupBoxControllers.ResumeLayout(false);
            this.groupBoxControllers.PerformLayout();
            this.groupBoxReticle.ResumeLayout(false);
            this.groupBoxReticle.PerformLayout();
            this.groupBoxMoveTo.ResumeLayout(false);
            this.groupBoxMoveTo.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.GroupBox groupBoxReticle;
        private System.Windows.Forms.Button btnPaintReticle;
        private System.Windows.Forms.TextBox txtReticleY;
        private System.Windows.Forms.TextBox txtReticleX;
        private System.Windows.Forms.Label labelReticleX;
        private System.Windows.Forms.Label labelReticleY;
        private System.Windows.Forms.GroupBox groupBoxControllers;
        private System.Windows.Forms.CheckBox chkBoxFaceTracker;
        private System.Windows.Forms.Panel panelScreen;
        private System.Windows.Forms.TextBox txtScreen;
        private System.Windows.Forms.CheckBox chkBoxColourTracking;
        private System.Windows.Forms.Button btnCalibrate;
    }
}
