using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using Emgu.CV;
using Kraken.Core;
using PiCamCV.Common.Interfaces;

namespace PiCamCV.Common
{
    public class ClassifierParameters
    {
        public double ScaleFactor { get; set; }
        public int MinNeighbors { get; set; }

        public Size MinSize { get; set; }

        public Size MaxSize { get; set; }

        public ClassifierParameters()
        {
            ScaleFactor = 1.1;
            MinNeighbors = 3;
            MinSize = new Size(20, 20);
            MaxSize = new Size();
        }
        
    }

    public class FaceDetectorInput : CascadeDetectorInput
    {
        public bool DetectEyes { get; set; }
        public FaceDetectorInput()
        {
            DetectEyes = true;
        }
    }

    public class Face
    {
        public Rectangle Region { get; set; }

        public List<Rectangle> Eyes { get; private set; }

        public Face()
        {
            Eyes = new List<Rectangle>();
        }

        public Face(Rectangle region) :this()
        {
            Region = region;
        }

        public override string ToString()
        {
            return string.Format("Region={0}, Eyes.Count={1}", Region, Eyes.Count);
        }
    }

    public class FaceDetectorOutput : CameraProcessOutput
    {
        public bool IsDetected
        {
            get { return Faces.Count > 0; }
        }
        public List<Face> Faces { get; private set; }
        public FaceDetectorOutput()
        {
            Faces = new List<Face>();
        }

        public override string ToString()
        {
            return string.Format("Faces.Count={0}, Eyes.Count={1}", Faces.Count, Faces.Sum(f=>f.Eyes.Count));
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
            Guard.NullArgument("input", input);
            var result = new FaceDetectorOutput();
            using (var ugray = new UMat())
            {
                if (input.Captured.NumberOfChannels == 3)
                {
                    CvInvoke.CvtColor(input.Captured, ugray, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
                }
                else
                {
                    input.Captured.CopyTo(ugray);
                }

                //normalizes brightness and increases contrast of the image
                CvInvoke.EqualizeHist(ugray, ugray);

                //Detect the faces  from the gray scale image and store the locations as rectangle
                //The first dimensional is the channel
                //The second dimension is the index of the rectangle in the specific channel
                Rectangle[] facesDetected = _faceClassifier.DetectMultiScale(
                   ugray,
                   input.ClassifierParams.ScaleFactor,
                   input.ClassifierParams.MinNeighbors,
                   input.ClassifierParams.MinSize,
                   input.ClassifierParams.MaxSize
                   );

                result.Faces.AddRange(facesDetected.Select(f => new Face(f)));

                if (input.DetectEyes)
                {
                    foreach (var face in result.Faces)
                    {
                        var faceRect = face.Region;
                        //Get the region of interest on the faces
                        using (var faceRegion = new UMat(ugray, faceRect))
                        {
                            Rectangle[] eyesDetected = _eyeClassifier.DetectMultiScale(
                                faceRegion,
                                1.1,
                                10,
                                new Size(20, 20));

                            foreach (Rectangle e in eyesDetected)
                            {
                                Rectangle eyeRect = e;
                                eyeRect.Offset(faceRect.X, faceRect.Y);
                                face.Eyes.Add(eyeRect);
                            }
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
