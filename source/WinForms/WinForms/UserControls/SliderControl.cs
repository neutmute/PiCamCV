﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PiCamCV.WinForms.UserControls
{
    public partial class SliderControl : UserControl
    {
        private string _label;
        public event EventHandler ValueChanged;
        private int _value;

        public string Label
        {
            get { return _label; }
            set
            {
                _label = value;
                SetLabel();
            }
        }

        public int Value
        {
            get { return _value; }  // cross thread from image grabbed event fix
            set { trackBar.Value = value; }
        }

        public SliderControl()
        {
            InitializeComponent();
        }

        public void SetLabel()
        {
            groupBox.Text = string.Format("{0} ({1})", _label, trackBar.Value);
        }

        private void trackBar_ValueChanged(object sender, EventArgs e)
        {
            SetLabel();
            if (ValueChanged != null)
            {
                _value = trackBar.Value;
                ValueChanged(this, e);
            }
        }
    }
}