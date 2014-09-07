namespace PiCamCV.WinForms
{
    partial class BasicCaptureControl
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxCanny = new System.Windows.Forms.GroupBox();
            this.imageBoxCanny = new Emgu.CV.UI.ImageBox();
            this.groupBoxSmoothed = new System.Windows.Forms.GroupBox();
            this.imageBoxSmoothedGray = new Emgu.CV.UI.ImageBox();
            this.groupBoxGray = new System.Windows.Forms.GroupBox();
            this.imageBoxGray = new Emgu.CV.UI.ImageBox();
            this.groupBoxCaptured = new System.Windows.Forms.GroupBox();
            this.imageBoxCaptured = new Emgu.CV.UI.ImageBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBoxCanny.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCanny)).BeginInit();
            this.groupBoxSmoothed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxSmoothedGray)).BeginInit();
            this.groupBoxGray.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxGray)).BeginInit();
            this.groupBoxCaptured.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCaptured)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBoxCanny, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxSmoothed, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxGray, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxCaptured, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(843, 603);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // groupBoxCanny
            // 
            this.groupBoxCanny.Controls.Add(this.imageBoxCanny);
            this.groupBoxCanny.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxCanny.Location = new System.Drawing.Point(424, 304);
            this.groupBoxCanny.Name = "groupBoxCanny";
            this.groupBoxCanny.Size = new System.Drawing.Size(416, 296);
            this.groupBoxCanny.TabIndex = 6;
            this.groupBoxCanny.TabStop = false;
            this.groupBoxCanny.Text = "Canny Edges";
            // 
            // imageBoxCanny
            // 
            this.imageBoxCanny.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBoxCanny.Location = new System.Drawing.Point(3, 16);
            this.imageBoxCanny.Name = "imageBoxCanny";
            this.imageBoxCanny.Size = new System.Drawing.Size(410, 277);
            this.imageBoxCanny.TabIndex = 2;
            this.imageBoxCanny.TabStop = false;
            // 
            // groupBoxSmoothed
            // 
            this.groupBoxSmoothed.Controls.Add(this.imageBoxSmoothedGray);
            this.groupBoxSmoothed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxSmoothed.Location = new System.Drawing.Point(3, 304);
            this.groupBoxSmoothed.Name = "groupBoxSmoothed";
            this.groupBoxSmoothed.Size = new System.Drawing.Size(415, 296);
            this.groupBoxSmoothed.TabIndex = 5;
            this.groupBoxSmoothed.TabStop = false;
            this.groupBoxSmoothed.Text = "Smoothed Grayscale";
            // 
            // imageBoxSmoothedGray
            // 
            this.imageBoxSmoothedGray.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBoxSmoothedGray.Location = new System.Drawing.Point(3, 16);
            this.imageBoxSmoothedGray.Name = "imageBoxSmoothedGray";
            this.imageBoxSmoothedGray.Size = new System.Drawing.Size(409, 277);
            this.imageBoxSmoothedGray.TabIndex = 3;
            this.imageBoxSmoothedGray.TabStop = false;
            // 
            // groupBoxGray
            // 
            this.groupBoxGray.Controls.Add(this.imageBoxGray);
            this.groupBoxGray.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxGray.Location = new System.Drawing.Point(424, 3);
            this.groupBoxGray.Name = "groupBoxGray";
            this.groupBoxGray.Size = new System.Drawing.Size(416, 295);
            this.groupBoxGray.TabIndex = 4;
            this.groupBoxGray.TabStop = false;
            this.groupBoxGray.Text = "Grayscale";
            // 
            // imageBoxGray
            // 
            this.imageBoxGray.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBoxGray.Location = new System.Drawing.Point(3, 16);
            this.imageBoxGray.Name = "imageBoxGray";
            this.imageBoxGray.Size = new System.Drawing.Size(410, 276);
            this.imageBoxGray.TabIndex = 5;
            this.imageBoxGray.TabStop = false;
            // 
            // groupBoxCaptured
            // 
            this.groupBoxCaptured.Controls.Add(this.imageBoxCaptured);
            this.groupBoxCaptured.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxCaptured.Location = new System.Drawing.Point(3, 3);
            this.groupBoxCaptured.Name = "groupBoxCaptured";
            this.groupBoxCaptured.Size = new System.Drawing.Size(415, 295);
            this.groupBoxCaptured.TabIndex = 1;
            this.groupBoxCaptured.TabStop = false;
            this.groupBoxCaptured.Text = "Captured Image";
            // 
            // imageBoxCaptured
            // 
            this.imageBoxCaptured.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBoxCaptured.Location = new System.Drawing.Point(3, 16);
            this.imageBoxCaptured.Name = "imageBoxCaptured";
            this.imageBoxCaptured.Size = new System.Drawing.Size(409, 276);
            this.imageBoxCaptured.TabIndex = 2;
            this.imageBoxCaptured.TabStop = false;
            // 
            // BasicCaptureControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "BasicCaptureControl";
            this.Size = new System.Drawing.Size(843, 603);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBoxCanny.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCanny)).EndInit();
            this.groupBoxSmoothed.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxSmoothedGray)).EndInit();
            this.groupBoxGray.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxGray)).EndInit();
            this.groupBoxCaptured.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCaptured)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBoxCaptured;
        private Emgu.CV.UI.ImageBox imageBoxCaptured;
        private System.Windows.Forms.GroupBox groupBoxCanny;
        private Emgu.CV.UI.ImageBox imageBoxCanny;
        private System.Windows.Forms.GroupBox groupBoxSmoothed;
        private Emgu.CV.UI.ImageBox imageBoxSmoothedGray;
        private System.Windows.Forms.GroupBox groupBoxGray;
        private Emgu.CV.UI.ImageBox imageBoxGray;
    }
}
