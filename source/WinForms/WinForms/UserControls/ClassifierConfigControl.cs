using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.Logging;
using PiCamCV.Common;

namespace PiCamCV.WinForms.UserControls
{
    public partial class ClassifierConfigControl : UserControl
    {
        static ILog Log = LogManager.GetCurrentClassLogger();
        private ClassifierParameters _currentParams;

        public event EventHandler ConfigChanged;

        public ClassifierConfigControl()
        {
            InitializeComponent();
        }

        public ClassifierParameters GetConfig()
        {

            var classifierParameters = new ClassifierParameters();

            try
            {
                var minSizeDimension = Int32.Parse(txtMinSize.Text);
                var maxSizeDimension = Int32.Parse(txtMaxSize.Text);

                classifierParameters.MinSize = new Size(minSizeDimension, minSizeDimension);
                classifierParameters.MaxSize = new Size(maxSizeDimension, maxSizeDimension);
                classifierParameters.ScaleFactor = Double.Parse(txtScale.Text);
                classifierParameters.MinNeighbors = Int32.Parse(txtMinNeigh.Text);
            }
            catch (Exception e)
            {
                classifierParameters = _currentParams;
                SetConfig(classifierParameters);
                Log.Info(e.Message);
            }

            return classifierParameters;
        }

        public void SetConfig(ClassifierParameters value)
        {
            txtMinSize.Text = value.MinSize.Width.ToString();
            txtMaxSize.Text = value.MaxSize.Width.ToString();
            txtScale.Text = value.ScaleFactor.ToString();
            txtMinNeigh.Text = value.MinNeighbors.ToString();
        }
        private void ClassifierConfigControl_Load(object sender, EventArgs e)
        {
            _currentParams = new ClassifierParameters();
            SetConfig(_currentParams);
        }

        private void btnSetDetectionParams_Click(object sender, EventArgs e)
        {
            if (ConfigChanged != null)
            {
                ConfigChanged(this, e);
            }
        }

    }
}
