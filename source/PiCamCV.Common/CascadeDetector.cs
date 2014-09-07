using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;
using PiCamCV.Common.Interfaces;

namespace PiCamCV.Common
{
    public class CascadeDetectorInput : CameraProcessInput
    {
        
    }

    public class CascadeDetectorProcessOutput : CameraProcessOutput
    {
        public List<Rectangle> Objects { get; set; }
        public bool IsDetected { get {return  Objects.Count > 0; } }

        public override string ToString()
        {
            return string.Format("IsDetected={0}, Objects.Count={1}", IsDetected, Objects.Count);
        }

        public CascadeDetectorProcessOutput()
        {
            Objects = new List<Rectangle>();
        }
    }
    public class CascadeDetector : CameraProcessor<CascadeDetectorInput, CascadeDetectorProcessOutput>
    {
        private readonly string _cascadeFilename;
        public CascadeDetector(string cascadeXmlContent)
        {
            _cascadeFilename = Path.GetTempFileName();
            File.WriteAllText(_cascadeFilename, cascadeXmlContent);
        }

        protected override CascadeDetectorProcessOutput DoProcess(CascadeDetectorInput input)
        {
            var result = new CascadeDetectorProcessOutput();
            using (var objectClassifier = new CascadeClassifier(_cascadeFilename))
            {
                using (UMat ugray = new UMat())
                {
                    CvInvoke.CvtColor(input.Captured, ugray, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);

                    //normalizes brightness and increases contrast of the image
                    CvInvoke.EqualizeHist(ugray, ugray);

                    //Detect the faces  from the gray scale image and store the locations as rectangle
                    //The first dimensional is the channel
                    //The second dimension is the index of the rectangle in the specific channel
                    Rectangle[] objectsDetected = objectClassifier.DetectMultiScale(
                       ugray,
                       1.1,
                       10,
                       Size.Empty);

                    result.Objects = objectsDetected.ToList();
                }
            }
            return result;  
        }
    }
}
