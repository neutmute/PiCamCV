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

    public class PanTiltCalibrationReadings : SerializableDictionary<Resolution, AxesCalibrationReadings>
    {
        
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
        public override string ToString()
        {
            return string.Format("Accepted={0}, AllReaddings.Count={1}", Accepted, AllReadings);
        }
    }

    public class AxesCalibrationReadings
    {
        public AxisCalibrationReadings this[PanTiltAxis axis]
        {
            get
            {
                switch (axis)
                {
                    case PanTiltAxis.Horizontal: return Horizontal;
                    case PanTiltAxis.Vertical: return Vertical;
                    default:throw new NotSupportedException();
                }
            }
        }

        public AxisCalibrationReadings Vertical { get;  set; }
        public AxisCalibrationReadings Horizontal { get;  set; }

        public AxesCalibrationReadings()
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

    /// <summary>
    /// int = pixel deviation, ReadingSet is the set of servo percentages that move that pixel deviation
    /// </summary>
    public class AxisCalibrationReadings : SerializableDictionary<int, ReadingSet>
    {
        public override string ToString()
        {
            return string.Format("{0} pixel readings", Keys.Count);
        }
    }

    public class CalibratingPanTiltController : PanTiltController
    {
        private readonly IFileBasedRepository<PanTiltCalibrationReadings> _readingsRepo;
        private readonly IScreen _screen;
        private AxesCalibrationReadings _currentResolutionReadings;

        private readonly ColourDetector _colourDetector;
        public ColourDetectSettings Settings { get; set; }
        public Func<Image<Bgr, byte>> GetCameraCapture { get; set; }
        public CalibratingPanTiltController(IPanTiltMechanism panTiltMech, IFileBasedRepository<PanTiltCalibrationReadings> readingsRepo, IScreen screen)
            : base(panTiltMech)
        {
            _colourDetector = new ColourDetector();
            _readingsRepo = readingsRepo;
            _screen = screen;
        }

        public void Calibrate(Resolution resolution)
        {
            PanTiltCalibrationReadings allReadings;
            if (_readingsRepo.IsPresent)
            {
                allReadings = _readingsRepo.Read();
            }
            else
            {
                allReadings = new PanTiltCalibrationReadings();
            }

            if (allReadings.ContainsKey(resolution))
            {
                allReadings.Remove(resolution);
            }

            allReadings.Add(resolution, new AxesCalibrationReadings());
            _currentResolutionReadings = allReadings[resolution];
            
            _screen.Clear();

            CalibrateHalfAxis(1, PanTiltAxis.Horizontal);
            CalibrateHalfAxis(-1, PanTiltAxis.Horizontal);
            CalibrateHalfAxis(1, PanTiltAxis.Vertical);
            CalibrateHalfAxis(-1, PanTiltAxis.Vertical);

            _currentResolutionReadings.CalculateAcceptedReadings();

            _readingsRepo.Write(allReadings);
            _screen.Clear();
            _screen.WriteLine("Calibration written to disk");
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

                    if (axis == PanTiltAxis.Horizontal)
                    {
                        getAxisValue = p => p.X;
                    }
                    else
                    {
                        getAxisValue = p => p.Y;
                    }

                    var pixelDeviation = Convert.ToInt32(getAxisValue(firstDetection.CentralPoint) - getAxisValue(newDetection.CentralPoint));
                    var axisReadings = _currentResolutionReadings[axis];
                    if (axisReadings.ContainsKey(pixelDeviation))
                    {
                        axisReadings[pixelDeviation].AllReadings.Add(accumulatedDeviation);    
                    }
                    else
                    {
                        axisReadings.Add(pixelDeviation, new ReadingSet(accumulatedDeviation));    
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
