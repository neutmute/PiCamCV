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

        /// <summary>
        /// TODO: Investigate
        /// Published on Apr 23, 2013
        /// 
        /*https://www.youtube.com/watch?v=Eux6BTQ4GSA
This is a little demo of an experiment I ran with a Raspberry Pi and a webcam. The device uses a Logitech C210 webcam, a powered USB hub (may or may not be required, depending on the webcam and RPi power source you use) and the OpenCV python bindings for facial recognition. The software takes screenshots from the webcam at regular intervals, and uses the Haar Cascade facial detection built into OpenCV.

The results aren't perfect but they're pretty good. The algorithm usually detects my face when present. It occasionally detects multiple faces (despite there being just one), but this is because I have it setup to detect both frontal and profile faces. This is by design.

I had to tune the face detection parameters to run smoothly on the RPi. The initial parameter set I used (on my i5 laptop) ran very slowly on the RPi. I ended up using:

HAAR_SCALE_FACTOR = 1.2
HAAR_MIN_NEIGHBORS = 2
HAAR_FLAGS = cv.CV_HAAR_DO_CANNY_PRUNING
HAAR_MIN_SIZE = (60, 60)
CAPTURE_IMAGE_WIDTH = 320
CAPTURE_IMAGE_HEIGHT = 240
*/
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override CascadeDetectorProcessOutput DoProcess(CascadeDetectorInput input)
        {
            var result = new CascadeDetectorProcessOutput();

            using (var objectClassifier = new CascadeClassifier(_cascadeFilename))
            using (var ugray = new UMat())
            {
                CvInvoke.CvtColor(input.Captured, ugray, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);

                //normalizes brightness and increases contrast of the image
                CvInvoke.EqualizeHist(ugray, ugray);

                //Rectangle[] objectsDetected = objectClassifier.DetectMultiScale(
                //   ugray,
                //   1.1,
                //   10,
                //   Size.Empty);

                Rectangle[] objectsDetected = objectClassifier.DetectMultiScale(ugray);

                result.Objects = objectsDetected.ToList();
            }

            return result;  
        }
    }
}
