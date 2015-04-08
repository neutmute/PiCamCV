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
            this.groupBoxSettings = new System.Windows.Forms.GroupBox();
            this.chkDetectEyes = new System.Windows.Forms.CheckBox();
            this.groupBoxCaptured = new System.Windows.Forms.GroupBox();
            this.imageBox = new Emgu.CV.UI.ImageBox();
            this.groupBoxDetectionParams = new System.Windows.Forms.GroupBox();
            this.groupBoxOverlays = new System.Windows.Forms.GroupBox();
            this.chkShowRectDimensions = new System.Windows.Forms.CheckBox();
            this.chkHat = new System.Windows.Forms.CheckBox();
            this.chkSunnies = new System.Windows.Forms.CheckBox();
            this.chkRectangles = new System.Windows.Forms.CheckBox();
            this.txtMaxSize = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMinSize = new System.Windows.Forms.TextBox();
            this.lblMinSize = new System.Windows.Forms.Label();
            this.txtMinNeigh = new System.Windows.Forms.TextBox();
            this.lblMinNeigh = new System.Windows.Forms.Label();
            this.txtScale = new System.Windows.Forms.TextBox();
            this.lblScale = new System.Windows.Forms.Label();
            this.btnSetDetectionParams = new System.Windows.Forms.Button();
            this.panelLeft.SuspendLayout();
            this.groupBoxSettings.SuspendLayout();
            this.groupBoxCaptured.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            this.groupBoxDetectionParams.SuspendLayout();
            this.groupBoxOverlays.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.groupBoxOverlays);
            this.panelLeft.Controls.Add(this.groupBoxDetectionParams);
            this.panelLeft.Controls.Add(this.groupBoxSettings);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(138, 464);
            this.panelLeft.TabIndex = 6;
            // 
            // groupBoxSettings
            // 
            this.groupBoxSettings.Controls.Add(this.chkDetectEyes);
            this.groupBoxSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxSettings.Location = new System.Drawing.Point(0, 0);
            this.groupBoxSettings.Name = "groupBoxSettings";
            this.groupBoxSettings.Size = new System.Drawing.Size(138, 66);
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
            // groupBoxDetectionParams
            // 
            this.groupBoxDetectionParams.Controls.Add(this.btnSetDetectionParams);
            this.groupBoxDetectionParams.Controls.Add(this.txtMaxSize);
            this.groupBoxDetectionParams.Controls.Add(this.label2);
            this.groupBoxDetectionParams.Controls.Add(this.txtMinSize);
            this.groupBoxDetectionParams.Controls.Add(this.lblMinSize);
            this.groupBoxDetectionParams.Controls.Add(this.txtMinNeigh);
            this.groupBoxDetectionParams.Controls.Add(this.lblMinNeigh);
            this.groupBoxDetectionParams.Controls.Add(this.txtScale);
            this.groupBoxDetectionParams.Controls.Add(this.lblScale);
            this.groupBoxDetectionParams.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxDetectionParams.Location = new System.Drawing.Point(0, 66);
            this.groupBoxDetectionParams.Name = "groupBoxDetectionParams";
            this.groupBoxDetectionParams.Size = new System.Drawing.Size(138, 157);
            this.groupBoxDetectionParams.TabIndex = 3;
            this.groupBoxDetectionParams.TabStop = false;
            this.groupBoxDetectionParams.Text = "Detection Parameters";
            // 
            // groupBoxOverlays
            // 
            this.groupBoxOverlays.Controls.Add(this.chkShowRectDimensions);
            this.groupBoxOverlays.Controls.Add(this.chkHat);
            this.groupBoxOverlays.Controls.Add(this.chkSunnies);
            this.groupBoxOverlays.Controls.Add(this.chkRectangles);
            this.groupBoxOverlays.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxOverlays.Location = new System.Drawing.Point(0, 223);
            this.groupBoxOverlays.Name = "groupBoxOverlays";
            this.groupBoxOverlays.Size = new System.Drawing.Size(138, 111);
            this.groupBoxOverlays.TabIndex = 4;
            this.groupBoxOverlays.TabStop = false;
            this.groupBoxOverlays.Text = "Overlays";
            // 
            // chkShowRectDimensions
            // 
            this.chkShowRectDimensions.AutoSize = true;
            this.chkShowRectDimensions.Location = new System.Drawing.Point(6, 42);
            this.chkShowRectDimensions.Name = "chkShowRectDimensions";
            this.chkShowRectDimensions.Size = new System.Drawing.Size(80, 17);
            this.chkShowRectDimensions.TabIndex = 3;
            this.chkShowRectDimensions.Text = "Dimensions";
            this.chkShowRectDimensions.UseVisualStyleBackColor = true;
            // 
            // chkHat
            // 
            this.chkHat.AutoSize = true;
            this.chkHat.Location = new System.Drawing.Point(7, 88);
            this.chkHat.Name = "chkHat";
            this.chkHat.Size = new System.Drawing.Size(84, 17);
            this.chkHat.TabIndex = 2;
            this.chkHat.Text = "Accessory 2";
            this.chkHat.UseVisualStyleBackColor = true;
            // 
            // chkSunnies
            // 
            this.chkSunnies.AutoSize = true;
            this.chkSunnies.Location = new System.Drawing.Point(6, 65);
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
            // txtMaxSize
            // 
            this.txtMaxSize.Location = new System.Drawing.Point(76, 98);
            this.txtMaxSize.Name = "txtMaxSize";
            this.txtMaxSize.Size = new System.Drawing.Size(47, 20);
            this.txtMaxSize.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Max Size";
            // 
            // txtMinSize
            // 
            this.txtMinSize.Location = new System.Drawing.Point(76, 71);
            this.txtMinSize.Name = "txtMinSize";
            this.txtMinSize.Size = new System.Drawing.Size(47, 20);
            this.txtMinSize.TabIndex = 14;
            // 
            // lblMinSize
            // 
            this.lblMinSize.AutoSize = true;
            this.lblMinSize.Location = new System.Drawing.Point(9, 74);
            this.lblMinSize.Name = "lblMinSize";
            this.lblMinSize.Size = new System.Drawing.Size(47, 13);
            this.lblMinSize.TabIndex = 13;
            this.lblMinSize.Text = "Min Size";
            // 
            // txtMinNeigh
            // 
            this.txtMinNeigh.Location = new System.Drawing.Point(76, 45);
            this.txtMinNeigh.Name = "txtMinNeigh";
            this.txtMinNeigh.Size = new System.Drawing.Size(47, 20);
            this.txtMinNeigh.TabIndex = 12;
            // 
            // lblMinNeigh
            // 
            this.lblMinNeigh.AutoSize = true;
            this.lblMinNeigh.Location = new System.Drawing.Point(9, 48);
            this.lblMinNeigh.Name = "lblMinNeigh";
            this.lblMinNeigh.Size = new System.Drawing.Size(61, 13);
            this.lblMinNeigh.TabIndex = 11;
            this.lblMinNeigh.Text = "Min. Neigh.";
            // 
            // txtScale
            // 
            this.txtScale.Location = new System.Drawing.Point(76, 19);
            this.txtScale.Name = "txtScale";
            this.txtScale.Size = new System.Drawing.Size(47, 20);
            this.txtScale.TabIndex = 10;
            // 
            // lblScale
            // 
            this.lblScale.AutoSize = true;
            this.lblScale.Location = new System.Drawing.Point(8, 22);
            this.lblScale.Name = "lblScale";
            this.lblScale.Size = new System.Drawing.Size(34, 13);
            this.lblScale.TabIndex = 9;
            this.lblScale.Text = "Scale";
            // 
            // btnSetDetectionParams
            // 
            this.btnSetDetectionParams.Location = new System.Drawing.Point(11, 124);
            this.btnSetDetectionParams.Name = "btnSetDetectionParams";
            this.btnSetDetectionParams.Size = new System.Drawing.Size(112, 23);
            this.btnSetDetectionParams.TabIndex = 17;
            this.btnSetDetectionParams.Text = "Set Parameters";
            this.btnSetDetectionParams.UseVisualStyleBackColor = true;
            this.btnSetDetectionParams.Click += new System.EventHandler(this.btnSetDetectionParams_Click);
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
            this.groupBoxSettings.ResumeLayout(false);
            this.groupBoxSettings.PerformLayout();
            this.groupBoxCaptured.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            this.groupBoxDetectionParams.ResumeLayout(false);
            this.groupBoxDetectionParams.PerformLayout();
            this.groupBoxOverlays.ResumeLayout(false);
            this.groupBoxOverlays.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.GroupBox groupBoxCaptured;
        private Emgu.CV.UI.ImageBox imageBox;
        private System.Windows.Forms.GroupBox groupBoxSettings;
        private System.Windows.Forms.CheckBox chkDetectEyes;
        private System.Windows.Forms.GroupBox groupBoxOverlays;
        private System.Windows.Forms.CheckBox chkShowRectDimensions;
        private System.Windows.Forms.CheckBox chkHat;
        private System.Windows.Forms.CheckBox chkSunnies;
        private System.Windows.Forms.CheckBox chkRectangles;
        private System.Windows.Forms.GroupBox groupBoxDetectionParams;
        private System.Windows.Forms.Button btnSetDetectionParams;
        private System.Windows.Forms.TextBox txtMaxSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMinSize;
        private System.Windows.Forms.Label lblMinSize;
        private System.Windows.Forms.TextBox txtMinNeigh;
        private System.Windows.Forms.Label lblMinNeigh;
        private System.Windows.Forms.TextBox txtScale;
        private System.Windows.Forms.Label lblScale;


    }
}
