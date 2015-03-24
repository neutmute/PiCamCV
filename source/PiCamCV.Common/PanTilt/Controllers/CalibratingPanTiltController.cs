using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using PiCamCV.ConsoleApp.Runners.PanTilt;

namespace PiCamCV.Common.PanTilt.Controllers
{
    public enum PanTiltAxis
    {
        Unspecified = 0,
        Horizontal,
        Vertical
    }

    public class ReadingSet
    {
        public List<Decimal> AllReadings { get; private set; }

        public Decimal Accepted { get; set; }
        public ReadingSet(Decimal firstReading)
        {
            AllReadings = new List<decimal> {firstReading};
        }
        public ReadingSet()
        {
            AllReadings = new List<decimal>();
        }
    }

    public class PanTiltCalbrationReadings
    {
        public AxisCalibrationReadings Vertical { get;private set; }
        public AxisCalibrationReadings Horizontal { get; private set; }

        public PanTiltCalbrationReadings()
        {
            Vertical = new AxisCalibrationReadings();
            Horizontal = new AxisCalibrationReadings();
        }
        
        public void CalculateAcceptedReadings()
        {
            CalculateAcceptedReading(Horizontal);
            CalculateAcceptedReading(Vertical);
        }

        private void CalculateAcceptedReading(AxisCalibrationReadings axisReadings)
        {
            foreach (var key in axisReadings.Keys)
            {
                axisReadings[key].Accepted = axisReadings[key].AllReadings.Average();
            }            
        }
    }

    public class AxisCalibrationReadings : Dictionary<int, ReadingSet>
    {
        
    }

    public class CalibratingPanTiltController : PanTiltController
    {
        private readonly PanTiltCalbrationReadings _readings;
        private readonly IScreen _screen;

        private readonly ColourDetector _colourDetector;
        public ColourDetectSettings Settings { get; set; }
        public Func<Image<Bgr, byte>> GetCameraCapture { get; set; }
        public CalibratingPanTiltController(IPanTiltMechanism panTiltMech, IScreen screen) : base(panTiltMech)
        {
            _colourDetector = new ColourDetector();
            _readings = new PanTiltCalbrationReadings();
            _screen = screen;
        }

        public void Calibrate()
        {
            _screen.Clear();
            CalibrateHalfAxis(1, PanTiltAxis.Horizontal);
            CalibrateHalfAxis(-1, PanTiltAxis.Horizontal);
            CalibrateHalfAxis(1, PanTiltAxis.Vertical);
            CalibrateHalfAxis(-1, PanTiltAxis.Vertical);

            _readings.CalculateAcceptedReadings();
        }

        private void CalibrateHalfAxis(int signMovement, PanTiltAxis axis)
        {
            const decimal saccadePercentIncrement = 2m;
            decimal accumulatedDeviation = 0;
            bool foundColour;
            do
            {
                ResetToCenter();
                var firstDetection = LocateColour();

                accumulatedDeviation += signMovement * saccadePercentIncrement;

                _screen.BeginRepaint();

                var movementRequired = new PanTiltSetting();
                if (axis == PanTiltAxis.Horizontal)
                {
                    movementRequired.PanPercent = accumulatedDeviation;
                }
                else
                {
                    movementRequired.TiltPercent = accumulatedDeviation;
                }
                _screen.WriteLine("Sign={0}", signMovement);
                _screen.WriteLine("Axis={0}", axis);
                _screen.WriteLine("Saccade={0}", accumulatedDeviation);
                MoveRelative(movementRequired);
                
                Task.Delay(250).Wait();
               // Thread.Sleep(100);
                
                _screen.WriteLine("Locating...");
                var newDetection = LocateColour();
                foundColour = newDetection.IsDetected;

                if (foundColour)
                {
                    Func<PointF, float> getAxisValue;
                    AxisCalibrationReadings targetAxis;

                    if (axis == PanTiltAxis.Horizontal)
                    {
                        getAxisValue = p => p.X;
                        targetAxis = _readings.Horizontal;
                    }
                    else
                    {
                        getAxisValue = p => p.Y;
                        targetAxis = _readings.Vertical;
                    }

                    var pixelDeviation = Convert.ToInt32(getAxisValue(firstDetection.CentralPoint) - getAxisValue(newDetection.CentralPoint));

                    if (targetAxis.ContainsKey(pixelDeviation))
                    {
                        targetAxis[pixelDeviation].AllReadings.Add(accumulatedDeviation);    
                    }
                    else
                    {
                        targetAxis.Add(pixelDeviation, new ReadingSet(accumulatedDeviation));    
                    }

                    _screen.WriteLine("Deviation={0}", pixelDeviation);
                }
            }
            while (foundColour && Math.Abs(accumulatedDeviation) < 60);
        }

        private void ResetToCenter()
        {
            MoveAbsolute(new PanTiltSetting(50m, 50m)); // start center
        }

        private ColourDetectorProcessOutput LocateColour()
        {
            var colourDetectorInput = new ColourDetectorInput();
            colourDetectorInput.Captured = GetCameraCapture().Mat;
            colourDetectorInput.SetCapturedImage = false;
            colourDetectorInput.Settings = Settings;

            var colourDetectorOutput = _colourDetector.Process(colourDetectorInput);
            return colourDetectorOutput;
        }

    }
}
