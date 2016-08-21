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
        const bool Accumulate = true;
        const int BinSize = 24;

        public CamshiftDetector()
        {
            TermCriteria = new MCvTermCriteria { Epsilon = 100 * double.Epsilon, MaxIter = 50 };
        }

        protected override CamshiftOutput DoProcess(TrackingInput input)
        {
            var output = new CamshiftOutput();
            var rectTrackinRegion = input.ObjectOfInterest;

            Mat matBackProjectionMask = new Mat();
            
            Image<Gray, byte> imgTrackingImage = input.Captured.ToImage<Gray, byte>(); 
            output.BackProjection = imgTrackingImage.Copy();

            _rectangleSearchWindow = new Rectangle(
                rectTrackinRegion.X - IncreaseRegion
                ,rectTrackinRegion.Y - IncreaseRegion
                ,rectTrackinRegion.Width + IncreaseRegion
                ,rectTrackinRegion.Height + IncreaseRegion);

            imgTrackingImage.ROI = rectTrackinRegion;
            var imgTrackingImageRoi = imgTrackingImage.Copy();

            //clear the roi
            imgTrackingImage.ROI = Rectangle.Empty;

            var channelsVec = new [] { 0 };
            var histSizeVec = new [] { BinSize };
            var rangesVec = new float[] { 0, BinSize - 1 };
            Mat oHist = new DenseHistogram(BinSize, new RangeF(0, BinSize));
            
            //ImageViewer.Show(imgTrackingImage);

            using (VectorOfMat vmTrackingImage = new VectorOfMat(imgTrackingImage.Mat))
            using (VectorOfMat vmTrackingImageRoi = new VectorOfMat(imgTrackingImageRoi.Mat))
            {
                CvInvoke.CalcHist(vmTrackingImageRoi, channelsVec, matBackProjectionMask, oHist, histSizeVec, rangesVec, Accumulate);
                CvInvoke.Normalize(oHist, oHist, 0, 255, NormType.MinMax);
                CvInvoke.CalcBackProject(vmTrackingImage, channelsVec, oHist, output.BackProjection, rangesVec, 1);
            }

            //ImageViewer.Show(imgTrackingImage);
            //ImageViewer.Show(imgBackProjection);

            output.ObjectOfInterest = CvInvoke.CamShift(output.BackProjection, ref _rectangleSearchWindow, TermCriteria);
            return output;
        }
    }
}
