namespace PiCamCV.WinForms.UserControls
{
    partial class FaceDetectionControl
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
            this.groupBoxOverlays = new System.Windows.Forms.GroupBox();
            this.chkSunnies = new System.Windows.Forms.CheckBox();
            this.chkRectangles = new System.Windows.Forms.CheckBox();
            this.groupBoxSettings = new System.Windows.Forms.GroupBox();
            this.chkDetectEyes = new System.Windows.Forms.CheckBox();
            this.groupBoxCaptured = new System.Windows.Forms.GroupBox();
            this.imageBox = new Emgu.CV.UI.ImageBox();
            this.chkHat = new System.Windows.Forms.CheckBox();
            this.panelLeft.SuspendLayout();
            this.groupBoxOverlays.SuspendLayout();
            this.groupBoxSettings.SuspendLayout();
            this.groupBoxCaptured.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.groupBoxOverlays);
            this.panelLeft.Controls.Add(this.groupBoxSettings);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(138, 464);
            this.panelLeft.TabIndex = 6;
            // 
            // groupBoxOverlays
            // 
            this.groupBoxOverlays.Controls.Add(this.chkHat);
            this.groupBoxOverlays.Controls.Add(this.chkSunnies);
            this.groupBoxOverlays.Controls.Add(this.chkRectangles);
            this.groupBoxOverlays.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxOverlays.Location = new System.Drawing.Point(0, 136);
            this.groupBoxOverlays.Name = "groupBoxOverlays";
            this.groupBoxOverlays.Size = new System.Drawing.Size(138, 101);
            this.groupBoxOverlays.TabIndex = 2;
            this.groupBoxOverlays.TabStop = false;
            this.groupBoxOverlays.Text = "Overlays";
            // 
            // chkSunnies
            // 
            this.chkSunnies.AutoSize = true;
            this.chkSunnies.Location = new System.Drawing.Point(6, 42);
            this.chkSunnies.Name = "chkSunnies";
            this.chkSunnies.Size = new System.Drawing.Size(84, 17);
            this.chkSunnies.TabIndex = 1;
            this.chkSunnies.Text = "Accessory 1";
            this.chkSunnies.UseVisualStyleBackColor = true;
            // 
            // chkRectangles
            // 
            this.chkRectangles.AutoSize = true;
            this.chkRectangles.Checked = true;
            this.chkRectangles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRectangles.Location = new System.Drawing.Point(6, 19);
            this.chkRectangles.Name = "chkRectangles";
            this.chkRectangles.Size = new System.Drawing.Size(80, 17);
            this.chkRectangles.TabIndex = 0;
            this.chkRectangles.Text = "Rectangles";
            this.chkRectangles.UseVisualStyleBackColor = true;
            // 
            // groupBoxSettings
            // 
            this.groupBoxSettings.Controls.Add(this.chkDetectEyes);
            this.groupBoxSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxSettings.Location = new System.Drawing.Point(0, 0);
            this.groupBoxSettings.Name = "groupBoxSettings";
            this.groupBoxSettings.Size = new System.Drawing.Size(138, 136);
            this.groupBoxSettings.TabIndex = 1;
            this.groupBoxSettings.TabStop = false;
            this.groupBoxSettings.Text = "Settings";
            // 
            // chkDetectEyes
            // 
            this.chkDetectEyes.AutoSize = true;
            this.chkDetectEyes.Checked = true;
            this.chkDetectEyes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDetectEyes.Location = new System.Drawing.Point(7, 20);
            this.chkDetectEyes.Name = "chkDetectEyes";
            this.chkDetectEyes.Size = new System.Drawing.Size(84, 17);
            this.chkDetectEyes.TabIndex = 0;
            this.chkDetectEyes.Text = "Detect Eyes";
            this.chkDetectEyes.UseVisualStyleBackColor = true;
            // 
            // groupBoxCaptured
            // 
            this.groupBoxCaptured.Controls.Add(this.imageBox);
            this.groupBoxCaptured.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxCaptured.Location = new System.Drawing.Point(138, 0);
            this.groupBoxCaptured.Name = "groupBoxCaptured";
            this.groupBoxCaptured.Size = new System.Drawing.Size(459, 464);
            this.groupBoxCaptured.TabIndex = 7;
            this.groupBoxCaptured.TabStop = false;
            this.groupBoxCaptured.Text = "Image";
            // 
            // imageBox
            // 
            this.imageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBox.Location = new System.Drawing.Point(3, 16);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(453, 445);
            this.imageBox.TabIndex = 6;
            this.imageBox.TabStop = false;
            // 
            // chkHat
            // 
            this.chkHat.AutoSize = true;
            this.chkHat.Location = new System.Drawing.Point(7, 65);
            this.chkHat.Name = "chkHat";
            this.chkHat.Size = new System.Drawing.Size(84, 17);
            this.chkHat.TabIndex = 2;
            this.chkHat.Text = "Accessory 2";
            this.chkHat.UseVisualStyleBackColor = true;
            // 
            // FaceDetectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxCaptured);
            this.Controls.Add(this.panelLeft);
            this.Name = "FaceDetectionControl";
            this.Size = new System.Drawing.Size(597, 464);
            this.Load += new System.EventHandler(this.ControlLoad);
            this.panelLeft.ResumeLayout(false);
            this.groupBoxOverlays.ResumeLayout(false);
            this.groupBoxOverlays.PerformLayout();
            this.groupBoxSettings.ResumeLayout(false);
            this.groupBoxSettings.PerformLayout();
            this.groupBoxCaptured.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.GroupBox groupBoxCaptured;
        private Emgu.CV.UI.ImageBox imageBox;
        private System.Windows.Forms.GroupBox groupBoxOverlays;
        private System.Windows.Forms.CheckBox chkSunnies;
        private System.Windows.Forms.CheckBox chkRectangles;
        private System.Windows.Forms.GroupBox groupBoxSettings;
        private System.Windows.Forms.CheckBox chkDetectEyes;
        private System.Windows.Forms.CheckBox chkHat;


    }
}
