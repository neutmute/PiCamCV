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
using FaceDetection;
using Kraken.Core;

namespace PiCamCV.WinForms.UserControls
{
    public partial class FaceDetectionControl : CameraConsumerUserControl
    {
        private FileInfo haarEyeFile;
        private FileInfo haarFaceFile;
        
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
        }

        public override void ImageGrabbedHandler(object sender, EventArgs e)
        {
            var frame = new Mat();
            CameraCapture.Retrieve(frame);
            var image = frame.ToImage<Bgr, byte>();

            var result = DetectFace.Detect(image, haarFaceFile.FullName, haarEyeFile.FullName);
            
            foreach (Rectangle face in result.Faces)
            {
                image.Draw(face, new Bgr(Color.Red), 2);
            }

            var imageSunnies = WearSunnies(image, result.Eyes);
            var eyeCount = 0;
            foreach (Rectangle eye in result.Eyes)
            {
                eyeCount++;
                image.Draw(eye, new Bgr(Color.Blue), 2);
                image.Draw(eyeCount.ToString(), eye.Location, FontFace.HersheyComplexSmall, 2, new Bgr(Color.Blue));
            }

            imageBox.Image = imageSunnies;
            
            NotifyStatus("Face detection took {0}", result.DetectionTime.ToHumanReadable());
        }

        public Image<Bgra, byte> Overlay3(Image<Bgra, byte> background, Image<Bgra, byte> foreground)
        {
           // var overlayMask = new Image<Gray, Byte>(overlay.Width, overlay.Height);
          
            return background.AddWeighted(foreground, 0.5, 0.5, 0.5);
        }

        //public Image<Bgra, byte> WearSunniesMaskMethod(Image<Bgr, byte> inputBgr, List<Rectangle> eyes)
        //{
        //    // detect which pixels in the overlay have something in them
        //    // and make a binary mask out of it
            
        //        var sunnyFile = @"D:\Downloads\sunnies\sunglasses.png";
        //        var overlayBgra = new Image<Bgra, byte>(sunnyFile);// overlayMat.ToImage<Bgra, byte>();
        //     var overlayMask = new Image<Gray, Byte>(overlayBgra.Width, overlayBgra.Height);
        //    CvInvoke.CvtColor(overlayBgra, overlayMask, ColorConversion.Bgr2Gray);

        //    overlayMask = overlayMask.ThresholdBinaryInv(new Gray(1), new Gray(10));

        //    // expand the mask from 1-channel to 3-channel
        //    overlayMask.Mat.Reshape(2);
        //    //overlayMask.Mat.CopyTo(overlay);
        //    var overlayMaskBgr  = overlayMask.Mat.ToImage<Bgr, byte>();

        //    //inputBgr *= overlayMaskBgr;

        //    ////var resizedMask = overlayMask.Resize(background.Width, background.Height, Inter.Linear);


        //    ////var bgraOverlay = resizedMask.Mat.ToImage<Bgra, byte>();
        //    ////var bgraBackground = background.ToImage<Bgra, byte>();


            

        //    // background = overlay.Log
        //    // backgroundbackground.Cross(overlayMask);
        //    //// # here's where the work gets done :

        //    // # mask out the pixels that you want to overlay
        //    // img *= overlayMask

        //    // # put the overlay on
        //    // img += overlay

        //    // # Show the image.
        //    // cv2.imshow(WINDOW_NAME, img)
        //}

        public Image<Bgra, byte> WearSunnies(Image<Bgr,byte> inputBgr, List<Rectangle> eyes)
        {
            var inputBgra = inputBgr.Mat.ToImage<Bgra, byte>();
            if (eyes.Count == 2)
            {
                var sunnyFile = @"D:\Downloads\sunnies\sunglasses.png";
             
                var overlayBgra = new Image<Bgra, byte>(sunnyFile);// overlayMat.ToImage<Bgra, byte>();
             
                eyes.Sort((e1, e2) => e1.Left.CompareTo(e2.Left));
                var leftEye = eyes[0];
                var rightEye = eyes[1];

                var width = rightEye.X - leftEye.X + rightEye.Width;
                var height = leftEye.Height;

                var sunglassRect = new Rectangle(leftEye.X, leftEye.Y, width, height);
                
                var resizeOverlayBgra = overlayBgra.Resize(sunglassRect.Width, sunglassRect.Height, Inter.Linear);

                var overlayTargetBgra = new Image<Bgra, byte>(inputBgr.Width, inputBgr.Height, new Bgra(0, 0, 0, 0));
                overlayTargetBgra.ROI = sunglassRect;
                resizeOverlayBgra.CopyTo(overlayTargetBgra);
                overlayTargetBgra.ROI = Rectangle.Empty;

                var outputBgra = inputBgra.AddWeighted(overlayTargetBgra, 0.9, 1, -0.5);
                inputBgra.ROI = Rectangle.Empty;
                outputBgra.ROI = Rectangle.Empty;

                return outputBgra;
            }
            return inputBgra;

           
        }

        /// <summary>
        /// http://jepsonsblog.blogspot.in/2012/10/overlay-transparent-image-in-opencv.html
        /// </summary>
        public void OverlayImage(Mat background, Mat foreground, Mat output, Point location)
        {
            background.CopyTo(output);
            var fgImage = foreground.ToImage<Bgra, byte>();

            // start at the row indicated by location, or at row 0 if location.y is negative.
            for (int y = Math.Max(location.Y, 0); y < background.Rows; ++y)
            {
                int fY = y - location.Y; // because of the translation

                // we are done of we have processed all rows of the foreground image.
                if (fY >= foreground.Rows)
                    break;

                // start at the column indicated by location, 

                // or at column 0 if location.x is negative.
                for (int x = Math.Max(location.X, 0); x < background.Cols; ++x)
                {
                    int fX = x - location.X; // because of the translation.

                    // we are done with this row if the column is outside of the foreground image.
                    if (fX >= foreground.Cols)
                        break;

                    // determine the opacity of the foregrond pixel, using its fourth (alpha) channel.
                    var opacity =
                        (double)fgImage.Data.GetValue(fY * foreground.Step + fX * foreground.NumberOfChannels + 3) / 255;


                    // and now combine the background and foreground pixel, using the opacity, 

                    // but only if opacity > 0.
                    for (int c = 0; opacity > 0 && c < output.NumberOfChannels; ++c)
                    {
                        byte foregroundPx =
                            (byte) foreground.Data.GetValue(fY*foreground.Step + fX*foreground.NumberOfChannels + c);
                        byte backgroundPx =
                            (byte) background.Data.GetValue(y*background.Step + x*background.NumberOfChannels + c);

                        var outIndex = y*output.Step + output.NumberOfChannels*x + c;
                        var outValue = backgroundPx*(1 - opacity) + foregroundPx*opacity;

                        output.Data.SetValue(outValue, outIndex);
                    }
                }
            }
}
        
    }
}

