using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using PiCamCV.Common.Interfaces;

namespace PiCamCV.Common
{
    public class CamshiftOutput : CameraProcessOutput
    {
        public RotatedRect ObjectOfInterest { get; set; }

        public Image<Gray, byte> BackProjection { get; set; }
    }

    public class CamshiftDetector : CameraProcessor<TrackingInput, CamshiftOutput>
    {
        MCvTermCriteria TermCriteria { get;}

        private const int IncreaseRegion = 10;
        private Rectangle _rectangleSearchWindow;
        readonly int[] _channels;
        private readonly int[] _histogramSize;
        private readonly float[] _ranges;
        const bool Accumulate = true;
        const int BinSize = 24;
        private Mat _histogram;
        private bool _trackStarted;
        private Mat _matBackProjectionMask ;
        private Image<Gray, byte> _backProjection;

        public CamshiftDetector()
        {
            _channels = new[] {0};
            _ranges = new float[] { 0, BinSize - 1 };
            _histogramSize = new[] { BinSize };
            TermCriteria = new MCvTermCriteria { Epsilon = 100 * double.Epsilon, MaxIter = 10 };
            _trackStarted = false;
        }

        protected override CamshiftOutput DoProcess(TrackingInput input)
        {
            var output = new CamshiftOutput();
            Image<Gray, byte> grayscaleInput = input.Captured.ToImage<Gray, byte>();
            
            if (input.StartNewTrack)
            {
                _trackStarted = false;
                   _backProjection = grayscaleInput.Copy();
                var trackingRegion = input.ObjectOfInterest;

                grayscaleInput.ROI = trackingRegion;
                var inputRoiImage = grayscaleInput.Copy();
                grayscaleInput.ROI = Rectangle.Empty;       //clear the roi

                StartNewTrack(input.ObjectOfInterest, grayscaleInput, inputRoiImage, output);
            }

            if (_trackStarted)
            { 
                using (VectorOfMat vectorMatGrayscaleInput = new VectorOfMat(grayscaleInput.Mat))
                {
                    CvInvoke.CalcBackProject(vectorMatGrayscaleInput, _channels, _histogram, _backProjection, _ranges, 1);
                }
                
                output.ObjectOfInterest = CvInvoke.CamShift(_backProjection, ref _rectangleSearchWindow, TermCriteria);
            }

            output.BackProjection = _backProjection;
            return output;
        }
        
        private void StartNewTrack(Rectangle toTrack, Image<Gray, byte> imgTrackingImage, Image<Gray, byte> imgRoi, CamshiftOutput output)
        {
            _matBackProjectionMask = new Mat();
            _rectangleSearchWindow = toTrack;// GetIncreasedRectangle(toTrack, IncreaseRegion);
            _histogram = new DenseHistogram(BinSize, new RangeF(0, BinSize));

            using (VectorOfMat vmTrackingImageRoi = new VectorOfMat(imgRoi.Mat))
            {
                CvInvoke.CalcHist(vmTrackingImageRoi, _channels, _matBackProjectionMask, _histogram, _histogramSize, _ranges, Accumulate);
                CvInvoke.Normalize(_histogram, _histogram, 0, 255, NormType.MinMax);
            }
            _trackStarted = true;
        }
        
        protected override void DisposeObject()
        {
            _histogram.Dispose();
            _matBackProjectionMask.Dispose();
            base.DisposeObject();
        }

        //private Rectangle GetIncreasedRectangle(Rectangle original, int increase)
        //{
        //    var increasedRect = new Rectangle(
        //         original.X - increase
        //       , original.Y - increase
        //       , original.Width + increase
        //       , original.Height + increase);

        //    return increasedRect;
        //}
    }
}
