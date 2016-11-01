namespace PiCamCV.WinForms.CameraConsumers
{
    partial class TrackingControl
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
            this.groupBoxTrackMethod = new System.Windows.Forms.GroupBox();
            this.radCamshift = new System.Windows.Forms.RadioButton();
            this.radTrackingApi = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBoxMain = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBoxOriginal = new System.Windows.Forms.GroupBox();
            this.imageBoxTracking = new Emgu.CV.UI.ImageBox();
            this.groupBoxProcessed = new System.Windows.Forms.GroupBox();
            this.imageBoxProcessed = new Emgu.CV.UI.ImageBox();
            this.panelLeft.SuspendLayout();
            this.groupBoxTrackMethod.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBoxMain.SuspendLayout();
            this.flowLayoutPanel.SuspendLayout();
            this.groupBoxOriginal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxTracking)).BeginInit();
            this.groupBoxProcessed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxProcessed)).BeginInit();
            this.SuspendLayout();
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.groupBoxTrackMethod);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(123, 724);
            this.panelLeft.TabIndex = 0;
            // 
            // groupBoxTrackMethod
            // 
            this.groupBoxTrackMethod.Controls.Add(this.radCamshift);
            this.groupBoxTrackMethod.Controls.Add(this.radTrackingApi);
            this.groupBoxTrackMethod.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxTrackMethod.Location = new System.Drawing.Point(0, 0);
            this.groupBoxTrackMethod.Name = "groupBoxTrackMethod";
            this.groupBoxTrackMethod.Size = new System.Drawing.Size(123, 185);
            this.groupBoxTrackMethod.TabIndex = 1;
            this.groupBoxTrackMethod.TabStop = false;
            this.groupBoxTrackMethod.Text = "Tracking Method";
            // 
            // radCamshift
            // 
            this.radCamshift.AutoSize = true;
            this.radCamshift.Location = new System.Drawing.Point(6, 42);
            this.radCamshift.Name = "radCamshift";
            this.radCamshift.Size = new System.Drawing.Size(65, 17);
            this.radCamshift.TabIndex = 2;
            this.radCamshift.TabStop = true;
            this.radCamshift.Text = "Camshift";
            this.radCamshift.UseVisualStyleBackColor = true;
            // 
            // radTrackingApi
            // 
            this.radTrackingApi.AutoSize = true;
            this.radTrackingApi.Location = new System.Drawing.Point(6, 19);
            this.radTrackingApi.Name = "radTrackingApi";
            this.radTrackingApi.Size = new System.Drawing.Size(87, 17);
            this.radTrackingApi.TabIndex = 1;
            this.radTrackingApi.TabStop = true;
            this.radTrackingApi.Text = "Tracking API";
            this.radTrackingApi.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBoxMain);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(123, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(985, 724);
            this.panel1.TabIndex = 1;
            // 
            // groupBoxMain
            // 
            this.groupBoxMain.Controls.Add(this.flowLayoutPanel);
            this.groupBoxMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxMain.Location = new System.Drawing.Point(0, 0);
            this.groupBoxMain.Name = "groupBoxMain";
            this.groupBoxMain.Size = new System.Drawing.Size(985, 724);
            this.groupBoxMain.TabIndex = 0;
            this.groupBoxMain.TabStop = false;
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Controls.Add(this.groupBoxOriginal);
            this.flowLayoutPanel.Controls.Add(this.groupBoxProcessed);
            this.flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(979, 705);
            this.flowLayoutPanel.TabIndex = 6;
            // 
            // groupBoxOriginal
            // 
            this.groupBoxOriginal.Controls.Add(this.imageBoxTracking);
            this.groupBoxOriginal.Location = new System.Drawing.Point(3, 3);
            this.groupBoxOriginal.Name = "groupBoxOriginal";
            this.groupBoxOriginal.Size = new System.Drawing.Size(577, 452);
            this.groupBoxOriginal.TabIndex = 8;
            this.groupBoxOriginal.TabStop = false;
            this.groupBoxOriginal.Text = "Raw";
            // 
            // imageBoxTracking
            // 
            this.imageBoxTracking.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBoxTracking.Location = new System.Drawing.Point(3, 16);
            this.imageBoxTracking.Name = "imageBoxTracking";
            this.imageBoxTracking.Size = new System.Drawing.Size(571, 433);
            this.imageBoxTracking.TabIndex = 7;
            this.imageBoxTracking.TabStop = false;
            // 
            // groupBoxProcessed
            // 
            this.groupBoxProcessed.Controls.Add(this.imageBoxProcessed);
            this.groupBoxProcessed.Location = new System.Drawing.Point(3, 461);
            this.groupBoxProcessed.Name = "groupBoxProcessed";
            this.groupBoxProcessed.Size = new System.Drawing.Size(392, 327);
            this.groupBoxProcessed.TabIndex = 9;
            this.groupBoxProcessed.TabStop = false;
            this.groupBoxProcessed.Text = "Processed";
            // 
            // imageBoxProcessed
            // 
            this.imageBoxProcessed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBoxProcessed.Location = new System.Drawing.Point(3, 16);
            this.imageBoxProcessed.Name = "imageBoxProcessed";
            this.imageBoxProcessed.Size = new System.Drawing.Size(386, 308);
            this.imageBoxProcessed.TabIndex = 8;
            this.imageBoxProcessed.TabStop = false;
            // 
            // TrackingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelLeft);
            this.Name = "TrackingControl";
            this.Size = new System.Drawing.Size(1108, 724);
            this.panelLeft.ResumeLayout(false);
            this.groupBoxTrackMethod.ResumeLayout(false);
            this.groupBoxTrackMethod.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBoxMain.ResumeLayout(false);
            this.flowLayoutPanel.ResumeLayout(false);
            this.groupBoxOriginal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxTracking)).EndInit();
            this.groupBoxProcessed.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxProcessed)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBoxMain;
        private System.Windows.Forms.GroupBox groupBoxTrackMethod;
        private System.Windows.Forms.RadioButton radCamshift;
        private System.Windows.Forms.RadioButton radTrackingApi;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.GroupBox groupBoxOriginal;
        private Emgu.CV.UI.ImageBox imageBoxTracking;
        private System.Windows.Forms.GroupBox groupBoxProcessed;
        private Emgu.CV.UI.ImageBox imageBoxProcessed;
    }
}
