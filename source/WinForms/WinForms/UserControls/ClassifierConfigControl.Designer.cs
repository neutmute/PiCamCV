namespace PiCamCV.WinForms.UserControls
{
    partial class ClassifierConfigControl
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
            this.groupBoxDetectionParams = new System.Windows.Forms.GroupBox();
            this.btnSetDetectionParams = new System.Windows.Forms.Button();
            this.txtMaxSize = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMinSize = new System.Windows.Forms.TextBox();
            this.lblMinSize = new System.Windows.Forms.Label();
            this.txtMinNeigh = new System.Windows.Forms.TextBox();
            this.lblMinNeigh = new System.Windows.Forms.Label();
            this.txtScale = new System.Windows.Forms.TextBox();
            this.lblScale = new System.Windows.Forms.Label();
            this.groupBoxDetectionParams.SuspendLayout();
            this.SuspendLayout();
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
            this.groupBoxDetectionParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxDetectionParams.Location = new System.Drawing.Point(0, 0);
            this.groupBoxDetectionParams.Name = "groupBoxDetectionParams";
            this.groupBoxDetectionParams.Size = new System.Drawing.Size(150, 150);
            this.groupBoxDetectionParams.TabIndex = 4;
            this.groupBoxDetectionParams.TabStop = false;
            this.groupBoxDetectionParams.Text = "Detection Parameters";
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
            // ClassifierConfigControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxDetectionParams);
            this.Name = "ClassifierConfigControl";
            this.Load += new System.EventHandler(this.ClassifierConfigControl_Load);
            this.groupBoxDetectionParams.ResumeLayout(false);
            this.groupBoxDetectionParams.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

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
