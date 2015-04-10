using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Kraken.Core;
using PiCamCV.Common;

namespace PiCamCV.WinForms.CameraConsumers
{
    public partial class HaarCascadeControl : CameraConsumerUserControl
    {
        private CascadeDetector _detector;
        private bool _comboBinding;

        private ClassifierParameters _classiferParams;

        public HaarCascadeControl()
        {
            InitializeComponent();
            
            var environmentService = new EnvironmentService();
            var cascadeFileInfo = new FileInfo(environmentService.GetAbsolutePathFromAssemblyRelative("haarcascades/haarcascade_lego_batman5.xml"));
            if (cascadeFileInfo.Exists)
            {
                
            }
            else
            {
                Log.Error(m => m("Failed to load cascade {0}", cascadeFileInfo.FullName));
            }
        }

        public override void ImageGrabbedHandler(object sender, EventArgs e)
        {
            if (_detector == null)
            {
                return;
            }
            using (var matCaptured = new Mat())
            {
                CameraCapture.Retrieve(matCaptured);
                var input = new CascadeDetectorInput {Captured = matCaptured};
                input.ClassifierParams = _classiferParams;
                var result = _detector.Process(input);
                var image = matCaptured.ToImage<Bgr, byte>();

                foreach (Rectangle item in result.Objects)
                {
                    image.Draw(item, new Bgr(Color.Blue), 2);
                }

                imageBoxCaptured.Image = image;
            }
        }

        private void HaarCascadeControl_Load(object sender, EventArgs e)
        {

            _classiferParams = new ClassifierParameters();
            classifierConfigControl.ConfigChanged += classifierConfigControl_ConfigChanged;
            SetupComboBox();
        }

        void classifierConfigControl_ConfigChanged(object sender, EventArgs e)
        {
            _classiferParams = classifierConfigControl.GetConfig();

            
        }

        private void SetupComboBox()
        {
            var environmentService = new EnvironmentService();
            var dummyFile =
                new FileInfo(environmentService.GetAbsolutePathFromAssemblyRelative("haarcascades/thisdoesnotexist.xml"));
            var files = dummyFile.Directory.GetFiles("*.xml");

            _comboBinding = true;            
            comboBoxCascade.DataSource = new BindingSource(files, null);
            comboBoxCascade.DisplayMember = "Name";
            comboBoxCascade.ValueMember = "FullName";
            _comboBinding = false;
        }

        private void comboBoxCascade_SelectedValueChanged(object sender, EventArgs e)
        {
            if (_comboBinding)
            {
                return;
            }

            if (_detector != null)
            {
                _detector.Dispose();
                _detector = null;
            }

            var cascadeFileInfo = (FileInfo)comboBoxCascade.SelectedItem;

            var xmlContent = File.ReadAllText(cascadeFileInfo.FullName);
            _detector = new CascadeDetector(xmlContent);
        }
    }
}
