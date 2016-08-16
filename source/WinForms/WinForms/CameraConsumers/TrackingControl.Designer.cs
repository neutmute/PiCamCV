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
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBoxMain = new System.Windows.Forms.GroupBox();
            this.imageBoxTracking = new Emgu.CV.UI.ImageBox();
            this.panel1.SuspendLayout();
            this.groupBoxMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxTracking)).BeginInit();
            this.SuspendLayout();
            // 
            // panelLeft
            // 
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(200, 474);
            this.panelLeft.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBoxMain);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(200, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(565, 474);
            this.panel1.TabIndex = 1;
            // 
            // groupBoxMain
            // 
            this.groupBoxMain.Controls.Add(this.imageBoxTracking);
            this.groupBoxMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxMain.Location = new System.Drawing.Point(0, 0);
            this.groupBoxMain.Name = "groupBoxMain";
            this.groupBoxMain.Size = new System.Drawing.Size(565, 474);
            this.groupBoxMain.TabIndex = 0;
            this.groupBoxMain.TabStop = false;
            // 
            // imageBoxTracking
            // 
            this.imageBoxTracking.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBoxTracking.Location = new System.Drawing.Point(3, 16);
            this.imageBoxTracking.Name = "imageBoxTracking";
            this.imageBoxTracking.Size = new System.Drawing.Size(559, 455);
            this.imageBoxTracking.TabIndex = 5;
            this.imageBoxTracking.TabStop = false;
            this.imageBoxTracking.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imageBoxTracking_MouseDown);
            this.imageBoxTracking.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imageBoxTracking_MouseMove);
            this.imageBoxTracking.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imageBoxTracking_MouseUp);
            // 
            // TrackingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelLeft);
            this.Name = "TrackingControl";
            this.Size = new System.Drawing.Size(765, 474);
            this.panel1.ResumeLayout(false);
            this.groupBoxMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxTracking)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBoxMain;
        private Emgu.CV.UI.ImageBox imageBoxTracking;
    }
}
