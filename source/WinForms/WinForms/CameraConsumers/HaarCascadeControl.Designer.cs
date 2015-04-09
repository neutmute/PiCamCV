namespace PiCamCV.WinForms.CameraConsumers
{
    partial class HaarCascadeControl
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
            this.classifierConfigControl = new PiCamCV.WinForms.UserControls.ClassifierConfigControl();
            this.panel = new System.Windows.Forms.Panel();
            this.labelCascade = new System.Windows.Forms.Label();
            this.comboBoxCascade = new System.Windows.Forms.ComboBox();
            this.imageBoxCaptured = new Emgu.CV.UI.ImageBox();
            this.panelLeft.SuspendLayout();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCaptured)).BeginInit();
            this.SuspendLayout();
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.classifierConfigControl);
            this.panelLeft.Controls.Add(this.panel);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(187, 479);
            this.panelLeft.TabIndex = 0;
            // 
            // classifierConfigControl
            // 
            this.classifierConfigControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.classifierConfigControl.Location = new System.Drawing.Point(0, 62);
            this.classifierConfigControl.Name = "classifierConfigControl";
            this.classifierConfigControl.Size = new System.Drawing.Size(187, 150);
            this.classifierConfigControl.TabIndex = 2;
            // 
            // panel
            // 
            this.panel.Controls.Add(this.labelCascade);
            this.panel.Controls.Add(this.comboBoxCascade);
            this.panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(187, 62);
            this.panel.TabIndex = 1;
            // 
            // labelCascade
            // 
            this.labelCascade.AutoSize = true;
            this.labelCascade.Location = new System.Drawing.Point(3, 10);
            this.labelCascade.Name = "labelCascade";
            this.labelCascade.Size = new System.Drawing.Size(49, 13);
            this.labelCascade.TabIndex = 1;
            this.labelCascade.Text = "Cascade";
            // 
            // comboBoxCascade
            // 
            this.comboBoxCascade.FormattingEnabled = true;
            this.comboBoxCascade.Location = new System.Drawing.Point(6, 26);
            this.comboBoxCascade.Name = "comboBoxCascade";
            this.comboBoxCascade.Size = new System.Drawing.Size(178, 21);
            this.comboBoxCascade.TabIndex = 0;
            this.comboBoxCascade.SelectedValueChanged += new System.EventHandler(this.comboBoxCascade_SelectedValueChanged);
            // 
            // imageBoxCaptured
            // 
            this.imageBoxCaptured.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBoxCaptured.Location = new System.Drawing.Point(187, 0);
            this.imageBoxCaptured.Name = "imageBoxCaptured";
            this.imageBoxCaptured.Size = new System.Drawing.Size(436, 479);
            this.imageBoxCaptured.TabIndex = 3;
            this.imageBoxCaptured.TabStop = false;
            // 
            // HaarCascadeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.imageBoxCaptured);
            this.Controls.Add(this.panelLeft);
            this.Name = "HaarCascadeControl";
            this.Size = new System.Drawing.Size(623, 479);
            this.Load += new System.EventHandler(this.HaarCascadeControl_Load);
            this.panelLeft.ResumeLayout(false);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCaptured)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelLeft;
        private Emgu.CV.UI.ImageBox imageBoxCaptured;
        private UserControls.ClassifierConfigControl classifierConfigControl;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Label labelCascade;
        private System.Windows.Forms.ComboBox comboBoxCascade;
    }
}
