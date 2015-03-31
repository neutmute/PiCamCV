using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kraken.Core;
using PiCamCV.Common.PanTilt.Controllers;

namespace PiCamCV.Common.PanTilt.MoveStrategies
{
    public class LinearRegressorFactory
    {
        public LinearRegressorPair GetAutoCalibrated(Resolution resolution)
        {
            switch (resolution.Width)
            {
                // case 320: return new LinearRegressor(0.0925m, 0.0963m);
                case 160:
                    return new LinearRegressorPair(0.189m, 0.056m, 0.1784m, 0.0465m);

                default:
                    return null;
            }
        }

        /// <summary>
        /// Linear regression calculated from observation 
        /// https://github.com/neutmute/PiCamCV/blob/master/docs/panTiltCalibration.xlsx
        /// </summary>
        public LinearRegressorPair GetHandMeasured320x240()
        {
            return new LinearRegressorPair(0.0933m, 0.0864m, 0.0812m, 0.2091m);
        }
    }

    public class LinearRegressorPair
    {
        public LinearRegressor PanAxis { get; set; }
        public LinearRegressor TiltAxis { get; set; }

        public LinearRegressorPair(Decimal xCoefficientPan, Decimal interceptPan, Decimal xCoefficientTilt, Decimal interceptTilt)
        {
            PanAxis = new LinearRegressor(xCoefficientPan, interceptPan);
            TiltAxis = new LinearRegressor(xCoefficientTilt, interceptTilt);
        }

        public LinearRegressor this[PanTiltAxis axis]
        {
            get
            {
                switch (axis)
                {
                    case PanTiltAxis.Horizontal: return PanAxis;
                    case PanTiltAxis.Vertical: return TiltAxis;
                    default: throw new NotSupportedException();
                }
            }
        }
    }

    public class LinearRegressor
    {
        public Decimal Intercept {get;protected set;}

        public Decimal XCoefficient {get;protected set;}

        internal LinearRegressor(Decimal xCoefficient, Decimal intercept)
        {
            XCoefficient = xCoefficient;
            Intercept = intercept;
        }

        public Decimal Calculate(int pixelDeviation)
        {
            return (pixelDeviation*XCoefficient) + Intercept;
        }
    }

}
