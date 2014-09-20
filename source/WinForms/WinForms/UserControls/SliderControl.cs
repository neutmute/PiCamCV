using System;
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

        public Orientation Orientation
        {
            get { return trackBar.Orientation; }
            set { trackBar.Orientation = value; }
        }

        public int TickFrequency
        {
            get { return trackBar.TickFrequency; }
            set { trackBar.TickFrequency = value; }
        }

        public int LargeChange
        {
            get { return trackBar.LargeChange; }
            set { trackBar.LargeChange = value; }
        }

        public int SmallChange
        {
            get { return trackBar.SmallChange; }
            set { trackBar.SmallChange = value; }
        }

        public int Maximum
        {
            get { return trackBar.Maximum; }
            set { trackBar.Maximum = value; }
        }

        public int Minimum
        {
            get { return trackBar.Minimum; }
            set { trackBar.Minimum = value; }
        }

        public int Value
        {
            get { return _value; }  // cross thread from image grabbed event fix
            set
            {
                trackBar.Value = value;
                _value = value;
            }
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
            _value = trackBar.Value;
            if (ValueChanged != null)
            {
                ValueChanged(this, e);
            }
        }
    }
}
