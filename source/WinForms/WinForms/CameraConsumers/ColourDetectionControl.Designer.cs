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
            this.sliderValueMin = new PiCamCV.WinForms.UserControls.SliderControl();
            this.sliderValueMax = new PiCamCV.WinForms.UserControls.SliderControl();
            this.sliderSaturationMin = new PiCamCV.WinForms.UserControls.SliderControl();
            this.sliderSaturationMax = new PiCamCV.WinForms.UserControls.SliderControl();
            this.sliderHueMin = new PiCamCV.WinForms.UserControls.SliderControl();
            this.sliderHueMax = new PiCamCV.WinForms.UserControls.SliderControl();
            this.groupBoxCaptured = new System.Windows.Forms.GroupBox();
            this.imageBoxCaptured = new Emgu.CV.UI.ImageBox();
            this.groupBoxFiltered = new System.Windows.Forms.GroupBox();
            this.imageBoxFiltered = new Emgu.CV.UI.ImageBox();
            this.panelLeft.SuspendLayout();
            this.groupBoxCaptured.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCaptured)).BeginInit();
            this.groupBoxFiltered.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxFiltered)).BeginInit();
            this.SuspendLayout();
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.sliderValueMin);
            this.panelLeft.Controls.Add(this.sliderValueMax);
            this.panelLeft.Controls.Add(this.sliderSaturationMin);
            this.panelLeft.Controls.Add(this.sliderSaturationMax);
            this.panelLeft.Controls.Add(this.sliderHueMin);
            this.panelLeft.Controls.Add(this.sliderHueMax);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(216, 600);
            this.panelLeft.TabIndex = 5;
            // 
            // sliderValueMin
            // 
            this.sliderValueMin.Dock = System.Windows.Forms.DockStyle.Top;
            this.sliderValueMin.Label = "Value Min";
            this.sliderValueMin.Location = new System.Drawing.Point(0, 278);
            this.sliderValueMin.Name = "sliderValueMin";
            this.sliderValueMin.Size = new System.Drawing.Size(216, 56);
            this.sliderValueMin.TabIndex = 5;
            this.sliderValueMin.Value = 0;
            // 
            // sliderValueMax
            // 
            this.sliderValueMax.Dock = System.Windows.Forms.DockStyle.Top;
            this.sliderValueMax.Label = "Value Max";
            this.sliderValueMax.Location = new System.Drawing.Point(0, 222);
            this.sliderValueMax.Name = "sliderValueMax";
            this.sliderValueMax.Size = new System.Drawing.Size(216, 56);
            this.sliderValueMax.TabIndex = 4;
            this.sliderValueMax.Value = 0;
            // 
            // sliderSaturationMin
            // 
            this.sliderSaturationMin.Dock = System.Windows.Forms.DockStyle.Top;
            this.sliderSaturationMin.Label = "Saturation Min";
            this.sliderSaturationMin.Location = new System.Drawing.Point(0, 166);
            this.sliderSaturationMin.Name = "sliderSaturationMin";
            this.sliderSaturationMin.Size = new System.Drawing.Size(216, 56);
            this.sliderSaturationMin.TabIndex = 3;
            this.sliderSaturationMin.Value = 0;
            // 
            // sliderSaturationMax
            // 
            this.sliderSaturationMax.Dock = System.Windows.Forms.DockStyle.Top;
            this.sliderSaturationMax.Label = "Saturation Max";
            this.sliderSaturationMax.Location = new System.Drawing.Point(0, 110);
            this.sliderSaturationMax.Name = "sliderSaturationMax";
            this.sliderSaturationMax.Size = new System.Drawing.Size(216, 56);
            this.sliderSaturationMax.TabIndex = 2;
            this.sliderSaturationMax.Value = 0;
            // 
            // sliderHueMin
            // 
            this.sliderHueMin.Dock = System.Windows.Forms.DockStyle.Top;
            this.sliderHueMin.Label = "Hue Min";
            this.sliderHueMin.Location = new System.Drawing.Point(0, 54);
            this.sliderHueMin.Name = "sliderHueMin";
            this.sliderHueMin.Size = new System.Drawing.Size(216, 56);
            this.sliderHueMin.TabIndex = 1;
            this.sliderHueMin.Value = 0;
            // 
            // sliderHueMax
            // 
            this.sliderHueMax.Dock = System.Windows.Forms.DockStyle.Top;
            this.sliderHueMax.Label = "Hue Max";
            this.sliderHueMax.Location = new System.Drawing.Point(0, 0);
            this.sliderHueMax.Name = "sliderHueMax";
            this.sliderHueMax.Size = new System.Drawing.Size(216, 54);
            this.sliderHueMax.TabIndex = 0;
            this.sliderHueMax.Value = 0;
            // 
            // groupBoxCaptured
            // 
            this.groupBoxCaptured.Controls.Add(this.imageBoxCaptured);
            this.groupBoxCaptured.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxCaptured.Location = new System.Drawing.Point(216, 0);
            this.groupBoxCaptured.Name = "groupBoxCaptured";
            this.groupBoxCaptured.Size = new System.Drawing.Size(591, 403);
            this.groupBoxCaptured.TabIndex = 6;
            this.groupBoxCaptured.TabStop = false;
            this.groupBoxCaptured.Text = "Captured";
            // 
            // imageBoxCaptured
            // 
            this.imageBoxCaptured.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBoxCaptured.Location = new System.Drawing.Point(3, 16);
            this.imageBoxCaptured.Name = "imageBoxCaptured";
            this.imageBoxCaptured.Size = new System.Drawing.Size(585, 384);
            this.imageBoxCaptured.TabIndex = 5;
            this.imageBoxCaptured.TabStop = false;
            // 
            // groupBoxFiltered
            // 
            this.groupBoxFiltered.Controls.Add(this.imageBoxFiltered);
            this.groupBoxFiltered.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxFiltered.Location = new System.Drawing.Point(216, 403);
            this.groupBoxFiltered.Name = "groupBoxFiltered";
            this.groupBoxFiltered.Size = new System.Drawing.Size(591, 197);
            this.groupBoxFiltered.TabIndex = 7;
            this.groupBoxFiltered.TabStop = false;
            this.groupBoxFiltered.Text = "Filtered";
            // 
            // imageBoxFiltered
            // 
            this.imageBoxFiltered.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBoxFiltered.Location = new System.Drawing.Point(3, 16);
            this.imageBoxFiltered.Name = "imageBoxFiltered";
            this.imageBoxFiltered.Size = new System.Drawing.Size(585, 178);
            this.imageBoxFiltered.TabIndex = 6;
            this.imageBoxFiltered.TabStop = false;
            // 
            // ColourDetectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxFiltered);
            this.Controls.Add(this.groupBoxCaptured);
            this.Controls.Add(this.panelLeft);
            this.Name = "ColourDetectionControl";
            this.Size = new System.Drawing.Size(807, 600);
            this.Load += new System.EventHandler(this.ColourDetectionControl_Load);
            this.panelLeft.ResumeLayout(false);
            this.groupBoxCaptured.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCaptured)).EndInit();
            this.groupBoxFiltered.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxFiltered)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelLeft;
        private UserControls.SliderControl sliderHueMin;
        private UserControls.SliderControl sliderHueMax;
        private System.Windows.Forms.GroupBox groupBoxCaptured;
        private Emgu.CV.UI.ImageBox imageBoxCaptured;
        private System.Windows.Forms.GroupBox groupBoxFiltered;
        private Emgu.CV.UI.ImageBox imageBoxFiltered;
        private UserControls.SliderControl sliderValueMin;
        private UserControls.SliderControl sliderValueMax;
        private UserControls.SliderControl sliderSaturationMin;
        private UserControls.SliderControl sliderSaturationMax;
    }
}
