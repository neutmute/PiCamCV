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
    public enum BiggestMotionType
    {
        Unspecified = 0,
        Area = 1,
        Pixels = 2,
    }

    public class MotionDetectSettings
    {
         public SubtractorConfig SubtractorConfig { get; set; }
        /// <summary>
        /// Threshold to define a motion area, reduce the value to detect smaller motion
        /// </summary>
        public int MinimumArea { get; set; }

        public BiggestMotionType BiggestMotionType { get; set; }

        /// <summary>
        /// This doesnt' do what i thought it did at time of naming/use
        /// </summary>
        public decimal MinimumPercentMotionInArea { get; set; }

        public MotionDetectSettings()
        {
            MinimumArea = 100;
            MinimumPercentMotionInArea = 0.05m;
            BiggestMotionType = BiggestMotionType.Area;
            SubtractorConfig = new SubtractorConfig();
        }
    }

    public class MotionSection
    {
        public Rectangle Region { get; set; }

        public int Area { get; set; }

        public double Angle { get; set; }

        public int PixelsInMotionCount { get; set; }
    }

    public class SubtractorConfig
    {
        public int History { get; set; }
        public float Threshold { get; set; }
        public bool ShadowDetection { get;set;}

        public SubtractorConfig()
        {
            History = 500;
            Threshold = 16f;
            ShadowDetection = true;
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                // Suitable nullity checks etc, of course :)
                hash = hash * 23 + History.GetHashCode();
                hash = hash * 23 + Threshold.GetHashCode();
                hash = hash * 23 + ShadowDetection.GetHashCode();
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            var otherAsThisType = obj as SubtractorConfig;
            return otherAsThisType != null && otherAsThisType.GetHashCode() == GetHashCode();
        }
    }

    public class MotionDetectorInput : CameraProcessInput
    {
        public MotionDetectSettings Settings { get; set; }

        public MotionDetectorInput()
        {
            Settings = new MotionDetectSettings();
        }
    }

    public class MotionDetectorOutput : CameraProcessOutput
    {
        public int OverallMotionPixelCount { get; set; }
        public double OverallAngle { get; set; }

        public List<MotionSection> MotionSections { get; private set; }

        public MotionSection BiggestMotion { get; set; }

        public bool IsDetected
        {
            get { return MotionSections.Count > 0; }
        }

        public Image<Bgr, byte> ForegroundImage { get; set; }
        public Image<Bgr, byte> MotionImage { get; set; }

        public MotionDetectorOutput()
        {
            MotionSections = new List<MotionSection>();
        }
    }

    /// <summary>
    /// derived from $\emgucv\Emgu.CV.Example\MotionDetection\Form1.cs
    /// </summary>
    public class MotionDetector : CameraProcessor<MotionDetectorInput, MotionDetectorOutput>
    {
        private SubtractorConfig _currentSubtractorConfig;

        private readonly Mat _segMask = new Mat();
        private readonly Mat _forgroundMask = new Mat();
        private MotionHistory _motionHistory;
        private BackgroundSubtractor _foregroundDetector;

        public MotionDetector()
        {
            Reset();
        }

        public void Reset()
        {
            if (_motionHistory != null)
            {
                _motionHistory.Dispose();
                _motionHistory = null;
            }

            _motionHistory = new MotionHistory(
                1, //in second, the duration of motion history you wants to keep
                0.05, //in second, maxDelta for cvCalcMotionGradient
                0.5); //in second, minDelta for cvCalcMotionGradient
        }

        protected override void DisposeObject()
        {
            base.DisposeObject();

            _segMask.Dispose();
            _forgroundMask.Dispose();
            _motionHistory.Dispose();
            _foregroundDetector.Dispose();
        }

        protected override MotionDetectorOutput DoProcess(MotionDetectorInput input)
        {
            var output = new MotionDetectorOutput();

            var subtractorConfig = input.Settings.SubtractorConfig;

            if (_foregroundDetector == null || !_currentSubtractorConfig.Equals(subtractorConfig))
            {
                if (_foregroundDetector != null)
                {
                    _foregroundDetector.Dispose();
                }

                _foregroundDetector = new BackgroundSubtractorMOG2(
                    subtractorConfig.History
                    , subtractorConfig.Threshold
                    , subtractorConfig.ShadowDetection);

                _currentSubtractorConfig = subtractorConfig;
            }

            _foregroundDetector.Apply(input.Captured, _forgroundMask);
            
            _motionHistory.Update(_forgroundMask);

            #region get a copy of the motion mask and enhance its color
            double[] minValues, maxValues;
            Point[] minLoc, maxLoc;
            
            _motionHistory.Mask.MinMax(out minValues, out maxValues, out minLoc, out maxLoc);

            var motionMask = new Mat();
            using (var sa = new ScalarArray(255.0/maxValues[0]))
            {
                CvInvoke.Multiply(_motionHistory.Mask, sa, motionMask, 1, DepthType.Cv8U);
            }
            #endregion

            if (input.SetCapturedImage)
            {
                output.ForegroundImage = _forgroundMask.ToImage<Bgr, byte>();
                output.MotionImage = new Image<Bgr, byte>(motionMask.Size);
                CvInvoke.InsertChannel(motionMask, output.MotionImage, 0);
            }
            
            Rectangle[] motionComponents;
            using (var boundingRect = new VectorOfRect())
            {
                _motionHistory.GetMotionComponents(_segMask, boundingRect);
                motionComponents = boundingRect.ToArray();
            }                           

            foreach (Rectangle motionComponent in motionComponents)
            {
                int area = motionComponent.Width * motionComponent.Height;

                //reject the components that have small area;
                if (area < input.Settings.MinimumArea) continue;

                // find the angle and motion pixel count of the specific area
                double angle, motionPixelCountDouble;
                _motionHistory.MotionInfo(_forgroundMask, motionComponent, out angle, out motionPixelCountDouble);

                int motionPixelCount = (int) motionPixelCountDouble;

                //reject the area that contains too few motion
                if (motionPixelCount < area*input.Settings.MinimumPercentMotionInArea)
                {
                    continue;
                }

                var motionSection = new MotionSection();
                motionSection.Area = area;
                motionSection.Region = motionComponent;
                motionSection.Angle = angle;
                motionSection.PixelsInMotionCount = motionPixelCount;

                output.MotionSections.Add(motionSection);
            }

            if (output.IsDetected)
            {
                switch (input.Settings.BiggestMotionType)
                {
                    case BiggestMotionType.Unspecified:
                        break;
                    case BiggestMotionType.Area:
                        output.MotionSections.Sort((x, y) => y.Area.CompareTo(x.Area));
                        break;
                    case BiggestMotionType.Pixels:
                        output.MotionSections.Sort((x, y) => y.PixelsInMotionCount.CompareTo(x.PixelsInMotionCount));
                        break;
                }
                output.BiggestMotion = output.MotionSections.FirstOrDefault();
            }

            double overallAngle, overallMotionPixelCount;

            _motionHistory.MotionInfo(_forgroundMask, new Rectangle(Point.Empty, motionMask.Size), out overallAngle, out overallMotionPixelCount);
          
            output.OverallAngle = overallAngle;
            output.OverallMotionPixelCount = Convert.ToInt32(overallMotionPixelCount);

            return output;
        }
    }
}
