using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.VideoSurveillance;
using PiCamCV.Common.Interfaces;

namespace PiCamCV.Common
{
    public class MotionSection
    {
        public Rectangle Region { get; set; }

        public int Area { get; set; }

        public double Angle { get; set; }

    }
    public class MotionDetectorInput : CameraProcessInput
    {

        /// <summary>
        /// Threshold to define a motion area, reduce the value to detect smaller motion
        /// </summary>
        public int MinimumArea { get; set; }

        public MotionDetectorInput()
        {
            MinimumArea = 100;
        }
    }

    public class MotionDetetorOutput : CameraProcessOutput
    {
        public int OverallMotionPixelCount { get; set; }
        public double OverallAngle { get; set; }

        public List<MotionSection> MotionSections { get; private set; }


        public Image<Bgr, byte> ForegroundImage { get; set; }
        public Image<Bgr, byte> MotionImage { get; set; }
        public MotionDetetorOutput()
        {
            MotionSections = new List<MotionSection>();
        }
    }

    public class MotionDetector : CameraProcessor<MotionDetectorInput, MotionDetetorOutput>
    {

        private Mat _segMask = new Mat();
        private Mat _forgroundMask = new Mat();
        private MotionHistory _motionHistory;
        private BackgroundSubtractor _forgroundDetector;

        public MotionDetector()
        {
            _motionHistory = new MotionHistory(
                1.0, //in second, the duration of motion history you wants to keep
                0.05, //in second, maxDelta for cvCalcMotionGradient
                0.5); //in second, minDelta for cvCalcMotionGradient
        }

        protected override void DisposeObject()
        {
            base.DisposeObject();

            _segMask.Dispose();
            _forgroundMask.Dispose();
            _motionHistory.Dispose();
            _forgroundDetector.Dispose();
        }

        protected override MotionDetetorOutput DoProcess(MotionDetectorInput input)
        {
            var output = new MotionDetetorOutput();
            if (_forgroundDetector == null)
            {
                _forgroundDetector = new BackgroundSubtractorMOG2();
            }

            _forgroundDetector.Apply(input.Captured, _forgroundMask);
            
            //update the motion history
            _motionHistory.Update(_forgroundMask);

            output.ForegroundImage = _forgroundMask.ToImage<Bgr,byte>();

            #region get a copy of the motion mask and enhance its color
            double[] minValues, maxValues;
            Point[] minLoc, maxLoc;
            
            _motionHistory.Mask.MinMax(out minValues, out maxValues, out minLoc, out maxLoc);

            var motionMask = new Mat();
            using (var sa = new ScalarArray(255.0/maxValues[0]))
            {
                CvInvoke.Multiply(_motionHistory.Mask, sa, motionMask, 1, DepthType.Cv8U);
                //Image<Gray, Byte> motionMask = _motionHistory.Mask.Mul(255.0 / maxValues[0]);
            }
            #endregion

            //create the motion image 
            output.MotionImage = new Image<Bgr, byte>(motionMask.Size);
            //display the motion pixels in blue (first channel)
            //motionImage[0] = motionMask;
            CvInvoke.InsertChannel(motionMask, output.MotionImage, 0);

            
            Rectangle[] rects;
            using (var boundingRect = new VectorOfRect())
            {
                _motionHistory.GetMotionComponents(_segMask, boundingRect);
                rects = boundingRect.ToArray();
            }                           

            //iterate through each of the motion component
            foreach (Rectangle comp in rects)
            {
                int area = comp.Width * comp.Height;
                //reject the components that have small area;
                if (area < input.MinimumArea) continue;

                // find the angle and motion pixel count of the specific area
                double angle, motionPixelCount;
                _motionHistory.MotionInfo(_forgroundMask, comp, out angle, out motionPixelCount);

                //reject the area that contains too few motion
                if (motionPixelCount < area * 0.05) continue;

                var motionSection = new MotionSection();
                motionSection.Area = area;
                motionSection.Region = comp;
                motionSection.Angle = angle;

                output.MotionSections.Add(motionSection);
            }

            double overallAngle, overallMotionPixelCount;

            _motionHistory.MotionInfo(_forgroundMask, new Rectangle(Point.Empty, motionMask.Size), out overallAngle, out overallMotionPixelCount);
          
            output.OverallAngle = overallAngle;
            output.OverallMotionPixelCount = Convert.ToInt32(overallMotionPixelCount);

            return output;
        }
    }
}
