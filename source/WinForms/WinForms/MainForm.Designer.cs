namespace WinForms
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelTop = new System.Windows.Forms.Panel();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageCameraCapture = new System.Windows.Forms.TabPage();
            this.tabControlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(873, 100);
            this.panelTop.TabIndex = 1;
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageCameraCapture);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 100);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(873, 440);
            this.tabControlMain.TabIndex = 2;
            // 
            // tabPageCameraCapture
            // 
            this.tabPageCameraCapture.Location = new System.Drawing.Point(4, 22);
            this.tabPageCameraCapture.Name = "tabPageCameraCapture";
            this.tabPageCameraCapture.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCameraCapture.Size = new System.Drawing.Size(865, 414);
            this.tabPageCameraCapture.TabIndex = 0;
            this.tabPageCameraCapture.Text = "Camera Capture";
            this.tabPageCameraCapture.UseVisualStyleBackColor = true;
            this.tabPageCameraCapture.Click += new System.EventHandler(this.tabPageCameraCapture_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 540);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.panelTop);
            this.Name = "MainForm";
            this.Text = "PiCamCV WinForms";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageCameraCapture;

    }
}

