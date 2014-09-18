using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using Emgu.CV;
using PiCamCV.Common.Interfaces;

namespace PiCamCV.Common
{
    public class FaceDetectorInput : CameraProcessInput
    {
        


        public FaceDetectorInput()
        {
            
        }
    }

    public class FaceDetectorOutput : CameraProcessOutput
    {
        public bool IsDetected
        {
            get { return Faces.Count > 0; }
        }
        public List<Rectangle> Faces { get; private set; }
        public List<Rectangle> Eyes { get; private set; }
        public FaceDetectorOutput()
        {
            Faces = new List<Rectangle>();
            Eyes = new List<Rectangle>();
        }

        public override string ToString()
        {
            return string.Format("Faces.Count={0}, Eyes.Count={1}", Faces.Count, Eyes.Count);
        }
    }

    /// <summary>
    /// my version of Emgu.CV.Example\FaceDetection\DetectFace.cs
    /// </summary>
    public class FaceDetector : CameraProcessor<FaceDetectorInput, FaceDetectorOutput>
    {
        private readonly CascadeClassifier _faceClassifier;
        private readonly CascadeClassifier _eyeClassifier;
        public FaceDetector(string faceCascadeFilename, string eyeCascadeFilename)
        {
            _faceClassifier = new CascadeClassifier(faceCascadeFilename);
            _eyeClassifier = new CascadeClassifier(eyeCascadeFilename);
        }
        protected override FaceDetectorOutput DoProcess(FaceDetectorInput input)
        {
            var result = new FaceDetectorOutput();
            using (UMat ugray = new UMat())
            {
                CvInvoke.CvtColor(input.Captured, ugray, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);

                //normalizes brightness and increases contrast of the image
                CvInvoke.EqualizeHist(ugray, ugray);

                //Detect the faces  from the gray scale image and store the locations as rectangle
                //The first dimensional is the channel
                //The second dimension is the index of the rectangle in the specific channel
                Rectangle[] facesDetected = _faceClassifier.DetectMultiScale(
                   ugray,
                   1.1,
                   10,
                   new Size(20, 20));

                result.Faces.AddRange(facesDetected);

                foreach (Rectangle f in facesDetected)
                {
                    //Get the region of interest on the faces
                    using (UMat faceRegion = new UMat(ugray, f))
                    {
                        Rectangle[] eyesDetected = _eyeClassifier.DetectMultiScale(
                           faceRegion,
                           1.1,
                           10,
                           new Size(20, 20));

                        foreach (Rectangle e in eyesDetected)
                        {
                            Rectangle eyeRect = e;
                            eyeRect.Offset(f.X, f.Y);
                            result.Eyes.Add(eyeRect);
                        }
                    }
                }
            }
            return result;
        }

        protected override void DisposeObject()
        {
            _faceClassifier.Dispose();
            _eyeClassifier.Dispose();
        }
    }
}
