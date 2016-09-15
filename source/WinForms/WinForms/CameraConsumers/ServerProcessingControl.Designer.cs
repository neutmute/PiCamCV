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
            this.panelLeft = new System.Windows.Forms.Panel();
            this.groupBoxTransmission = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radBson = new System.Windows.Forms.RadioButton();
            this.groupBoxCaptured = new System.Windows.Forms.GroupBox();
            this.imageBoxCaptured = new Emgu.CV.UI.ImageBox();
            this.panelRest.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.groupBoxTransmission.SuspendLayout();
            this.groupBoxCaptured.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCaptured)).BeginInit();
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
            this.groupBoxTransmission.Controls.Add(this.radioButton2);
            this.groupBoxTransmission.Controls.Add(this.radBson);
            this.groupBoxTransmission.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxTransmission.Location = new System.Drawing.Point(0, 0);
            this.groupBoxTransmission.Name = "groupBoxTransmission";
            this.groupBoxTransmission.Size = new System.Drawing.Size(230, 165);
            this.groupBoxTransmission.TabIndex = 0;
            this.groupBoxTransmission.TabStop = false;
            this.groupBoxTransmission.Text = "Transmission Method";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(26, 64);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(85, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "radioButton2";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radBson
            // 
            this.radBson.AutoSize = true;
            this.radBson.Location = new System.Drawing.Point(26, 41);
            this.radBson.Name = "radBson";
            this.radBson.Size = new System.Drawing.Size(79, 17);
            this.radBson.TabIndex = 0;
            this.radBson.TabStop = true;
            this.radBson.Text = "BSON Post";
            this.radBson.UseVisualStyleBackColor = true;
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
            // ServerProcessingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelRest);
            this.Controls.Add(this.panelLeft);
            this.Name = "ServerProcessingControl";
            this.Size = new System.Drawing.Size(844, 652);
            this.panelRest.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.groupBoxTransmission.ResumeLayout(false);
            this.groupBoxTransmission.PerformLayout();
            this.groupBoxCaptured.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCaptured)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelRest;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.GroupBox groupBoxTransmission;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radBson;
        private System.Windows.Forms.GroupBox groupBoxCaptured;
        private Emgu.CV.UI.ImageBox imageBoxCaptured;
    }
}
