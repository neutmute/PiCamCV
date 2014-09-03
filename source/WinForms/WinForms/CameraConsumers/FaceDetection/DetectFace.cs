/*
 * Stripped version of Emgu.CV.Example\FaceDetection\DetectFace.cs
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace FaceDetection
{
    public class FaceDetectResult
    {
        public List<Rectangle> Faces {get;private set;}
        public List<Rectangle> Eyes {get;private set;}

        public TimeSpan DetectionTime {get;set;}

        public FaceDetectResult()
        {
            Faces = new List<Rectangle>();
            Eyes = new List<Rectangle>();
        }
    }
    public static class DetectFace
    {
        public static FaceDetectResult Detect(
            Image<Bgr, Byte> image
            , String faceFileName
            , String eyeFileName)
        {
                Stopwatch watch;
                var result = new FaceDetectResult();
            
                //Read the HaarCascade objects
                using (var faceClassifier = new CascadeClassifier(faceFileName))
                using (var eyeClassifier = new CascadeClassifier(eyeFileName))
                {
                    watch = Stopwatch.StartNew();
                    using (UMat ugray = new UMat())
                    {
                        CvInvoke.CvtColor(image, ugray, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);

                        //normalizes brightness and increases contrast of the image
                        CvInvoke.EqualizeHist(ugray, ugray);

                        //Detect the faces  from the gray scale image and store the locations as rectangle
                        //The first dimensional is the channel
                        //The second dimension is the index of the rectangle in the specific channel
                        Rectangle[] facesDetected = faceClassifier.DetectMultiScale(
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
                                Rectangle[] eyesDetected = eyeClassifier.DetectMultiScale(
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
                    watch.Stop();
                }
            result.DetectionTime = watch.Elapsed;
            return result;
        }
    }
}
