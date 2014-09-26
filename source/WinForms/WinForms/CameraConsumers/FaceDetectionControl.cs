using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Kraken.Core;
using PiCamCV.Common;
using PiCamCV.WinForms.ExtensionMethods;

namespace PiCamCV.WinForms.UserControls
{
    public class AccessoryOverlay
    {
        private Rectangle _calculatedRectangle;
        public Image<Bgra, byte> Overlay { get; private set; }
        public FileInfo File { get; set; }

        public Rectangle CalculatedRectangle
        {
            get { return _calculatedRectangle; }
            set
            {
                _calculatedRectangle = value;
                if (value != Rectangle.Empty)
                {
                    LastGoodRectangle = value;
                }
            }
        }

        public Rectangle LastGoodRectangle { get; set; }

        public Rectangle FinalRectangle
        {
            get
            {
                if (CalculatedRectangle == Rectangle.Empty)
                {
                    return LastGoodRectangle;
                }
                return CalculatedRectangle;
            }
            
        }

        public bool IsWearable { get; set; }

        public AccessoryOverlay(string filename)
        {
            Overlay = new Image<Bgra, byte>(filename);
        }
    }

    public partial class FaceDetectionControl : CameraConsumerUserControl
    {
        private FileInfo haarEyeFile;
        private FileInfo haarFaceFile;
        private FaceDetector _faceDetector;
        private Rectangle _lastGoodSunnies;
        private AccessoryOverlay _sunnies2;
        
        public FaceDetectionControl()
        {
            InitializeComponent();
        }
        public void ControlLoad(object sender, EventArgs e)
        {
            var assemblyPath = Assembly.GetExecutingAssembly().Location;
            var haarCascadePath = Path.Combine(new FileInfo(assemblyPath).DirectoryName, "haarcascades");
            haarEyeFile = new FileInfo(Path.Combine(haarCascadePath, "haarcascade_eye.xml"));
            haarFaceFile = new FileInfo(Path.Combine(haarCascadePath, "haarcascade_frontalface_default.xml"));

            _faceDetector = new FaceDetector(haarFaceFile.FullName, haarEyeFile.FullName);
            _lastGoodSunnies = Rectangle.Empty;

            _sunnies2 = new AccessoryOverlay(@"D:\Downloads\sunnies\sunglasses2.png");
        }

        public override void ImageGrabbedHandler(object sender, EventArgs e)
        {
            using (var frame = new Mat())
            {
                CameraCapture.Retrieve(frame);
                
                var input = new FaceDetectorInput();
                input.Captured = frame;
                input.DetectEyes = chkDetectEyes.Checked;

                var result = _faceDetector.Process(input);
                var imageBgr = result.CapturedImage;
                IImage imageOut = imageBgr;

                if (chkRectangles.Checked)
                {
                    foreach (var face in result.Faces)
                    {
                        imageBgr.Draw(face.Region, new Bgr(Color.Red), 2);

                        var eyeCount = 0;
                        foreach (Rectangle eye in face.Eyes)
                        {
                            eyeCount++;
                            imageBgr.Draw(eye, new Bgr(Color.Blue), 2);
                            imageBgr.Draw(eyeCount.ToString(), eye.Location, FontFace.HersheyComplexSmall, 2, new Bgr(Color.Blue));
                        }
                    }
                }

                if (chkSunnies.Checked && result.Faces.Count > 0)
                {

                    imageOut = WearSunnies2(imageBgr, result.Faces[0].Eyes);
                   // var imageOut2 = WearSunnies2(imageBgr, result.Faces[0].Eyes);
                }
                
                imageBox.Image = imageOut;

                NotifyStatus("Face detection took {0}", result.Elapsed.ToHumanReadable());
            }
        }


        public Image<Bgra, byte> WearSunnies2(Image<Bgr, byte> inputBgr, List<Rectangle> eyes)
        {
            _sunnies2.CalculatedRectangle = GetSunglassRectangle(eyes);
            return WearObject(inputBgr, _sunnies2);
        }

        public Image<Bgra, byte> WearObject(Image<Bgr, byte> inputBgr, AccessoryOverlay accessory)
        {

            var inputBgra = inputBgr.Mat.ToImage<Bgra, byte>();
            var bgraBlack = new Bgra(0, 0, 0, 0);

            if (accessory.FinalRectangle == Rectangle.Empty)
            {
                return inputBgra;
            }

            var overlayRect = accessory.FinalRectangle;
            var resizeOverlayBgra = accessory.Overlay.Resize(overlayRect.Width, overlayRect.Height, Inter.Linear);

            var overlayTargetBgra = new Image<Bgra, byte>(inputBgr.Width, inputBgr.Height, bgraBlack);
            overlayTargetBgra.ROI = overlayRect;
            resizeOverlayBgra.CopyTo(overlayTargetBgra);
            overlayTargetBgra.ROI = Rectangle.Empty;

            const bool useMask = true;
            if (useMask)
            {
                var overlayMask = new Image<Gray, Byte>(inputBgr.Width, inputBgr.Height);
                CvInvoke.CvtColor(overlayTargetBgra, overlayMask, ColorConversion.Bgr2Gray);
                overlayMask = overlayMask.ThresholdBinary(new Gray(1), new Gray(1));
                inputBgra.SetValue(bgraBlack, overlayMask);
            }

            var outputBgra = inputBgra.AddWeighted(overlayTargetBgra, 1, 1, 1);

            inputBgra.ROI = Rectangle.Empty;
            outputBgra.ROI = Rectangle.Empty;

            return outputBgra;
        }

        //public Image<Bgra, byte> WearSunnies(Image<Bgr,byte> inputBgr, List<Rectangle> eyes)
        //{
        //    var inputBgra = inputBgr.Mat.ToImage<Bgra, byte>();
        //    var bgraBlack = new Bgra(0, 0, 0, 0);
        //  //  if (eyes.Count == 2)
        //    {
        //        var sunnyFile = @"D:\Downloads\sunnies\sunglasses2.png";
             
        //        var overlayBgra = new Image<Bgra, byte>(sunnyFile);// overlayMat.ToImage<Bgra, byte>();

        //        Rectangle sunglassRect;
        //        if (eyes.Count == 2)
        //        {
        //            sunglassRect = GetSunglassRectangle(eyes);
        //        }
        //        else if (_lastGoodSunnies!= Rectangle.Empty)
        //        {
        //            sunglassRect = _lastGoodSunnies;
        //        }
        //        else
        //        {
        //            return inputBgra;
        //        }

        //        Log.Info(m => m("Painting sunnies {0}", sunglassRect));
        //        var resizeOverlayBgra = overlayBgra.Resize(sunglassRect.Width, sunglassRect.Height, Inter.Linear);

        //        var overlayTargetBgra = new Image<Bgra, byte>(inputBgr.Width, inputBgr.Height, bgraBlack);
        //        overlayTargetBgra.ROI = sunglassRect;
        //        resizeOverlayBgra.CopyTo(overlayTargetBgra);
        //        overlayTargetBgra.ROI = Rectangle.Empty;

        //        const bool useMask = true;
        //        if (useMask)
        //        {
        //            var overlayMask = new Image<Gray, Byte>(inputBgr.Width, inputBgr.Height);
        //            CvInvoke.CvtColor(overlayTargetBgra, overlayMask, ColorConversion.Bgr2Gray);
        //            overlayMask = overlayMask.ThresholdBinary(new Gray(1), new Gray(1));
        //            //inputBgra.ROI = sunglassRect;
        //            inputBgra.SetValue(bgraBlack, overlayMask);
        //        }
        //        else
        //        {
                    
        //        }

        //        var outputBgra = inputBgra.AddWeighted(overlayTargetBgra, 1, 1, 1);
                
        //        inputBgra.ROI = Rectangle.Empty;
        //        outputBgra.ROI = Rectangle.Empty;

        //        return outputBgra;
        //    }
        //    return inputBgra;

           
        //}

        private Rectangle GetSunglassRectangle(List<Rectangle> eyes)
        {
            if (eyes.Count == 2)
            {
                eyes.Sort((e1, e2) => e1.Left.CompareTo(e2.Left));
                var leftEye = eyes[0];
                var rightEye = eyes[1];

                var eyeWidth = (rightEye.X - leftEye.X) + rightEye.Width;
                var eyeHeight = (int) (leftEye.Height*1.5);

                var widthFactor = (int) (eyeWidth*0.25);
                var heightFactor = (int) (eyeHeight*1.1);

                var width = eyeWidth + 2*widthFactor;
                var height = eyeHeight + heightFactor;

                var left = leftEye.X - widthFactor;
                var minEyeHeight = Math.Min(leftEye.Y, rightEye.Y);
                var maxEyeHeight = Math.Max(leftEye.Y, rightEye.Y);
                var top = (int) (minEyeHeight - (maxEyeHeight*0.1));

                var sunglassRect = new Rectangle(left, top, width, height);

                return sunglassRect;
            }
            else
            {
                return Rectangle.Empty;
            }
        }
        
    }
}

