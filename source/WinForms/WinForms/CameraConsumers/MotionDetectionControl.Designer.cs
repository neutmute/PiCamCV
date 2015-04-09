namespace PiCamCV.WinForms.CameraConsumers
{
    partial class MotionDetectionControl
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
            this.panelControlOwner = new System.Windows.Forms.Panel();
            this.sliderSize = new PiCamCV.WinForms.UserControls.SliderControl();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBoxCaptured = new System.Windows.Forms.GroupBox();
            this.imageBoxCaptured = new Emgu.CV.UI.ImageBox();
            this.groupBoxMasked = new System.Windows.Forms.GroupBox();
            this.imageBoxMasked = new Emgu.CV.UI.ImageBox();
            this.groupBoxMotion = new System.Windows.Forms.GroupBox();
            this.imageBoxMotion = new Emgu.CV.UI.ImageBox();
            this.groupBoxSettings = new System.Windows.Forms.GroupBox();
            this.sliderMinimumArea = new PiCamCV.WinForms.UserControls.SliderControl();
            this.panelControlOwner.SuspendLayout();
            this.flowLayoutPanel.SuspendLayout();
            this.groupBoxCaptured.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCaptured)).BeginInit();
            this.groupBoxMasked.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxMasked)).BeginInit();
            this.groupBoxMotion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxMotion)).BeginInit();
            this.groupBoxSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControlOwner
            // 
            this.panelControlOwner.Controls.Add(this.groupBoxSettings);
            this.panelControlOwner.Controls.Add(this.sliderSize);
            this.panelControlOwner.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControlOwner.Location = new System.Drawing.Point(0, 0);
            this.panelControlOwner.Name = "panelControlOwner";
            this.panelControlOwner.Size = new System.Drawing.Size(200, 564);
            this.panelControlOwner.TabIndex = 0;
            // 
            // sliderSize
            // 
            this.sliderSize.Dock = System.Windows.Forms.DockStyle.Top;
            this.sliderSize.Label = "Image Scale";
            this.sliderSize.LargeChange = 5;
            this.sliderSize.Location = new System.Drawing.Point(0, 0);
            this.sliderSize.Maximum = 255;
            this.sliderSize.Minimum = 50;
            this.sliderSize.Name = "sliderSize";
            this.sliderSize.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.sliderSize.Size = new System.Drawing.Size(200, 117);
            this.sliderSize.SmallChange = 1;
            this.sliderSize.TabIndex = 0;
            this.sliderSize.TickFrequency = 1;
            this.sliderSize.Value = 100;
            this.sliderSize.ValueChanged += new System.EventHandler(this.sliderControl1_ValueChanged);
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Controls.Add(this.groupBoxCaptured);
            this.flowLayoutPanel.Controls.Add(this.groupBoxMasked);
            this.flowLayoutPanel.Controls.Add(this.groupBoxMotion);
            this.flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel.Location = new System.Drawing.Point(200, 0);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(703, 564);
            this.flowLayoutPanel.TabIndex = 1;
            // 
            // groupBoxCaptured
            // 
            this.groupBoxCaptured.Controls.Add(this.imageBoxCaptured);
            this.groupBoxCaptured.Location = new System.Drawing.Point(3, 3);
            this.groupBoxCaptured.Name = "groupBoxCaptured";
            this.groupBoxCaptured.Size = new System.Drawing.Size(407, 311);
            this.groupBoxCaptured.TabIndex = 0;
            this.groupBoxCaptured.TabStop = false;
            this.groupBoxCaptured.Text = "Captured";
            // 
            // imageBoxCaptured
            // 
            this.imageBoxCaptured.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBoxCaptured.Location = new System.Drawing.Point(3, 16);
            this.imageBoxCaptured.Name = "imageBoxCaptured";
            this.imageBoxCaptured.Size = new System.Drawing.Size(401, 292);
            this.imageBoxCaptured.TabIndex = 4;
            this.imageBoxCaptured.TabStop = false;
            // 
            // groupBoxMasked
            // 
            this.groupBoxMasked.Controls.Add(this.imageBoxMasked);
            this.groupBoxMasked.Location = new System.Drawing.Point(416, 3);
            this.groupBoxMasked.Name = "groupBoxMasked";
            this.groupBoxMasked.Size = new System.Drawing.Size(226, 221);
            this.groupBoxMasked.TabIndex = 1;
            this.groupBoxMasked.TabStop = false;
            this.groupBoxMasked.Text = "Masked";
            // 
            // imageBoxMasked
            // 
            this.imageBoxMasked.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBoxMasked.Location = new System.Drawing.Point(3, 16);
            this.imageBoxMasked.Name = "imageBoxMasked";
            this.imageBoxMasked.Size = new System.Drawing.Size(220, 202);
            this.imageBoxMasked.TabIndex = 4;
            this.imageBoxMasked.TabStop = false;
            // 
            // groupBoxMotion
            // 
            this.groupBoxMotion.Controls.Add(this.imageBoxMotion);
            this.groupBoxMotion.Location = new System.Drawing.Point(3, 320);
            this.groupBoxMotion.Name = "groupBoxMotion";
            this.groupBoxMotion.Size = new System.Drawing.Size(274, 250);
            this.groupBoxMotion.TabIndex = 2;
            this.groupBoxMotion.TabStop = false;
            this.groupBoxMotion.Text = "Motion";
            // 
            // imageBoxMotion
            // 
            this.imageBoxMotion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBoxMotion.Location = new System.Drawing.Point(3, 16);
            this.imageBoxMotion.Name = "imageBoxMotion";
            this.imageBoxMotion.Size = new System.Drawing.Size(268, 231);
            this.imageBoxMotion.TabIndex = 4;
            this.imageBoxMotion.TabStop = false;
            // 
            // groupBoxSettings
            // 
            this.groupBoxSettings.Controls.Add(this.sliderMinimumArea);
            this.groupBoxSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxSettings.Location = new System.Drawing.Point(0, 117);
            this.groupBoxSettings.Name = "groupBoxSettings";
            this.groupBoxSettings.Size = new System.Drawing.Size(200, 181);
            this.groupBoxSettings.TabIndex = 1;
            this.groupBoxSettings.TabStop = false;
            this.groupBoxSettings.Text = "Settings";
            // 
            // sliderMinimumArea
            // 
            this.sliderMinimumArea.Dock = System.Windows.Forms.DockStyle.Top;
            this.sliderMinimumArea.Label = "Minimum Area";
            this.sliderMinimumArea.LargeChange = 1000;
            this.sliderMinimumArea.Location = new System.Drawing.Point(3, 16);
            this.sliderMinimumArea.Maximum = 100000;
            this.sliderMinimumArea.Minimum = 50;
            this.sliderMinimumArea.Name = "sliderMinimumArea";
            this.sliderMinimumArea.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.sliderMinimumArea.Size = new System.Drawing.Size(194, 117);
            this.sliderMinimumArea.SmallChange = 100;
            this.sliderMinimumArea.TabIndex = 0;
            this.sliderMinimumArea.TickFrequency = 1;
            this.sliderMinimumArea.Value = 100;
            // 
            // MotionDetectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel);
            this.Controls.Add(this.panelControlOwner);
            this.Name = "MotionDetectionControl";
            this.Size = new System.Drawing.Size(903, 564);
            this.Load += new System.EventHandler(this.MotionDetectionControl_Load);
            this.panelControlOwner.ResumeLayout(false);
            this.flowLayoutPanel.ResumeLayout(false);
            this.groupBoxCaptured.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCaptured)).EndInit();
            this.groupBoxMasked.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxMasked)).EndInit();
            this.groupBoxMotion.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxMotion)).EndInit();
            this.groupBoxSettings.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelControlOwner;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.GroupBox groupBoxCaptured;
        private System.Windows.Forms.GroupBox groupBoxMasked;
        private System.Windows.Forms.GroupBox groupBoxMotion;
        private Emgu.CV.UI.ImageBox imageBoxCaptured;
        private Emgu.CV.UI.ImageBox imageBoxMasked;
        private Emgu.CV.UI.ImageBox imageBoxMotion;
        private UserControls.SliderControl sliderSize;
        private System.Windows.Forms.GroupBox groupBoxSettings;
        private UserControls.SliderControl sliderMinimumArea;
    }
}
