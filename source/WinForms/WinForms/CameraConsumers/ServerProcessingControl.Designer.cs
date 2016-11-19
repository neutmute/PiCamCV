namespace PiCamCV.WinForms.CameraConsumers
{
    partial class ServerProcessingControl
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
            this.panelRest = new System.Windows.Forms.Panel();
            this.groupBoxCaptured = new System.Windows.Forms.GroupBox();
            this.imageBoxCaptured = new Emgu.CV.UI.ImageBox();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.groupBoxTransmission = new System.Windows.Forms.GroupBox();
            this.radBsonJpeg = new System.Windows.Forms.RadioButton();
            this.radBsonImage = new System.Windows.Forms.RadioButton();
            this.lblHost = new System.Windows.Forms.Label();
            this.panelRest.SuspendLayout();
            this.groupBoxCaptured.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCaptured)).BeginInit();
            this.panelLeft.SuspendLayout();
            this.groupBoxTransmission.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelRest
            // 
            this.panelRest.Controls.Add(this.groupBoxCaptured);
            this.panelRest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRest.Location = new System.Drawing.Point(230, 0);
            this.panelRest.Name = "panelRest";
            this.panelRest.Size = new System.Drawing.Size(614, 652);
            this.panelRest.TabIndex = 3;
            // 
            // groupBoxCaptured
            // 
            this.groupBoxCaptured.Controls.Add(this.imageBoxCaptured);
            this.groupBoxCaptured.Location = new System.Drawing.Point(3, 12);
            this.groupBoxCaptured.Name = "groupBoxCaptured";
            this.groupBoxCaptured.Size = new System.Drawing.Size(366, 231);
            this.groupBoxCaptured.TabIndex = 1;
            this.groupBoxCaptured.TabStop = false;
            this.groupBoxCaptured.Text = "Captured";
            // 
            // imageBoxCaptured
            // 
            this.imageBoxCaptured.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBoxCaptured.Location = new System.Drawing.Point(3, 16);
            this.imageBoxCaptured.Name = "imageBoxCaptured";
            this.imageBoxCaptured.Size = new System.Drawing.Size(360, 212);
            this.imageBoxCaptured.TabIndex = 4;
            this.imageBoxCaptured.TabStop = false;
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.groupBoxTransmission);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(230, 652);
            this.panelLeft.TabIndex = 2;
            // 
            // groupBoxTransmission
            // 
            this.groupBoxTransmission.Controls.Add(this.lblHost);
            this.groupBoxTransmission.Controls.Add(this.radBsonJpeg);
            this.groupBoxTransmission.Controls.Add(this.radBsonImage);
            this.groupBoxTransmission.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxTransmission.Location = new System.Drawing.Point(0, 0);
            this.groupBoxTransmission.Name = "groupBoxTransmission";
            this.groupBoxTransmission.Size = new System.Drawing.Size(230, 165);
            this.groupBoxTransmission.TabIndex = 0;
            this.groupBoxTransmission.TabStop = false;
            this.groupBoxTransmission.Text = "Transmission Method";
            // 
            // radBsonJpeg
            // 
            this.radBsonJpeg.AutoSize = true;
            this.radBsonJpeg.Location = new System.Drawing.Point(8, 77);
            this.radBsonJpeg.Name = "radBsonJpeg";
            this.radBsonJpeg.Size = new System.Drawing.Size(85, 17);
            this.radBsonJpeg.TabIndex = 1;
            this.radBsonJpeg.TabStop = true;
            this.radBsonJpeg.Text = "BSON JPEG";
            this.radBsonJpeg.UseVisualStyleBackColor = true;
            // 
            // radBsonImage
            // 
            this.radBsonImage.AutoSize = true;
            this.radBsonImage.Location = new System.Drawing.Point(8, 54);
            this.radBsonImage.Name = "radBsonImage";
            this.radBsonImage.Size = new System.Drawing.Size(87, 17);
            this.radBsonImage.TabIndex = 0;
            this.radBsonImage.TabStop = true;
            this.radBsonImage.Text = "BSON Image";
            this.radBsonImage.UseVisualStyleBackColor = true;
            // 
            // lblHost
            // 
            this.lblHost.AutoSize = true;
            this.lblHost.Location = new System.Drawing.Point(8, 28);
            this.lblHost.Name = "lblHost";
            this.lblHost.Size = new System.Drawing.Size(39, 13);
            this.lblHost.TabIndex = 2;
            this.lblHost.Text = "lblHost";
            // 
            // ServerProcessingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelRest);
            this.Controls.Add(this.panelLeft);
            this.Name = "ServerProcessingControl";
            this.Size = new System.Drawing.Size(844, 652);
            this.panelRest.ResumeLayout(false);
            this.groupBoxCaptured.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCaptured)).EndInit();
            this.panelLeft.ResumeLayout(false);
            this.groupBoxTransmission.ResumeLayout(false);
            this.groupBoxTransmission.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelRest;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.GroupBox groupBoxTransmission;
        private System.Windows.Forms.RadioButton radBsonJpeg;
        private System.Windows.Forms.RadioButton radBsonImage;
        private System.Windows.Forms.GroupBox groupBoxCaptured;
        private Emgu.CV.UI.ImageBox imageBoxCaptured;
        private System.Windows.Forms.Label lblHost;
    }
}
