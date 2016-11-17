using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
using Kraken.Core;
using PiCamCV.Common.Interfaces;

namespace PiCamCV.Common
{
    public class ColourDetectorOutput : CameraProcessOutput
    {
        /// <summary>
        /// The image that has had the filters applied before moments detected
        /// </summary>
        public Image<Gray, byte> ThresholdImage { get;  set; }

        public bool IsDetected { get; set; }

        public PointF CentralPoint { get; set; }

        public double MomentArea { get; set; }
        
        public override string ToString()
        {
            return string.Format(
                "Processed colour in {3}, IsDetected={0}, MomentArea={2}, CentralPoint={1}"
                , IsDetected
                , CentralPoint
                , MomentArea
                , Elapsed.ToHumanReadable(HumanReadableTimeSpanOptions.Abbreviated));
        }
    }
}