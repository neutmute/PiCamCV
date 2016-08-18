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
            this.radColourTracking = new System.Windows.Forms.RadioButton();
            this.radTrackingApi = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBoxMain = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.imageBoxTracking = new Emgu.CV.UI.ImageBox();
            this.imageBoxProcessed = new Emgu.CV.UI.ImageBox();
            this.panelLeft.SuspendLayout();
            this.groupBoxTrackMethod.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBoxMain.SuspendLayout();
            this.flowLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxTracking)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxProcessed)).BeginInit();
            this.SuspendLayout();
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.groupBoxTrackMethod);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(123, 474);
            this.panelLeft.TabIndex = 0;
            // 
            // groupBoxTrackMethod
            // 
            this.groupBoxTrackMethod.Controls.Add(this.radColourTracking);
            this.groupBoxTrackMethod.Controls.Add(this.radTrackingApi);
            this.groupBoxTrackMethod.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxTrackMethod.Location = new System.Drawing.Point(0, 0);
            this.groupBoxTrackMethod.Name = "groupBoxTrackMethod";
            this.groupBoxTrackMethod.Size = new System.Drawing.Size(123, 185);
            this.groupBoxTrackMethod.TabIndex = 1;
            this.groupBoxTrackMethod.TabStop = false;
            this.groupBoxTrackMethod.Text = "Tracking Method";
            // 
            // radColourTracking
            // 
            this.radColourTracking.AutoSize = true;
            this.radColourTracking.Location = new System.Drawing.Point(6, 42);
            this.radColourTracking.Name = "radColourTracking";
            this.radColourTracking.Size = new System.Drawing.Size(55, 17);
            this.radColourTracking.TabIndex = 2;
            this.radColourTracking.TabStop = true;
            this.radColourTracking.Text = "Colour";
            this.radColourTracking.UseVisualStyleBackColor = true;
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
            this.panel1.Size = new System.Drawing.Size(642, 474);
            this.panel1.TabIndex = 1;
            // 
            // groupBoxMain
            // 
            this.groupBoxMain.Controls.Add(this.flowLayoutPanel);
            this.groupBoxMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxMain.Location = new System.Drawing.Point(0, 0);
            this.groupBoxMain.Name = "groupBoxMain";
            this.groupBoxMain.Size = new System.Drawing.Size(642, 474);
            this.groupBoxMain.TabIndex = 0;
            this.groupBoxMain.TabStop = false;
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Controls.Add(this.imageBoxTracking);
            this.flowLayoutPanel.Controls.Add(this.imageBoxProcessed);
            this.flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(636, 455);
            this.flowLayoutPanel.TabIndex = 6;
            // 
            // imageBoxTracking
            // 
            this.imageBoxTracking.Location = new System.Drawing.Point(3, 3);
            this.imageBoxTracking.Name = "imageBoxTracking";
            this.imageBoxTracking.Size = new System.Drawing.Size(322, 216);
            this.imageBoxTracking.TabIndex = 6;
            this.imageBoxTracking.TabStop = false;
            // 
            // imageBoxProcessed
            // 
            this.imageBoxProcessed.Location = new System.Drawing.Point(3, 225);
            this.imageBoxProcessed.Name = "imageBoxProcessed";
            this.imageBoxProcessed.Size = new System.Drawing.Size(322, 229);
            this.imageBoxProcessed.TabIndex = 7;
            this.imageBoxProcessed.TabStop = false;
            // 
            // TrackingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelLeft);
            this.Name = "TrackingControl";
            this.Size = new System.Drawing.Size(765, 474);
            this.panelLeft.ResumeLayout(false);
            this.groupBoxTrackMethod.ResumeLayout(false);
            this.groupBoxTrackMethod.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBoxMain.ResumeLayout(false);
            this.flowLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxTracking)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxProcessed)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBoxMain;
        private System.Windows.Forms.GroupBox groupBoxTrackMethod;
        private System.Windows.Forms.RadioButton radColourTracking;
        private System.Windows.Forms.RadioButton radTrackingApi;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private Emgu.CV.UI.ImageBox imageBoxTracking;
        private Emgu.CV.UI.ImageBox imageBoxProcessed;
    }
}
