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
            this.imageBox = new Emgu.CV.UI.ImageBox();
            this.labelDetectionTime = new System.Windows.Forms.Label();
            this.panelTop = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageBox
            // 
            this.imageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBox.Location = new System.Drawing.Point(0, 35);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(597, 429);
            this.imageBox.TabIndex = 4;
            this.imageBox.TabStop = false;
            // 
            // labelDetectionTime
            // 
            this.labelDetectionTime.AutoSize = true;
            this.labelDetectionTime.Location = new System.Drawing.Point(3, 10);
            this.labelDetectionTime.Name = "labelDetectionTime";
            this.labelDetectionTime.Size = new System.Drawing.Size(79, 13);
            this.labelDetectionTime.TabIndex = 2;
            this.labelDetectionTime.Text = "Detection Time";
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.labelDetectionTime);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(597, 35);
            this.panelTop.TabIndex = 3;
            // 
            // FaceDetectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.imageBox);
            this.Controls.Add(this.panelTop);
            this.Name = "FaceDetectionControl";
            this.Size = new System.Drawing.Size(597, 464);
            this.Load += new System.EventHandler(this.ControlLoad);
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Emgu.CV.UI.ImageBox imageBox;
        private System.Windows.Forms.Label labelDetectionTime;
        private System.Windows.Forms.Panel panelTop;

    }
}
