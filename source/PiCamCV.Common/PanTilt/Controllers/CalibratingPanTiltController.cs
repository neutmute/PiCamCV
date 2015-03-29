using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using Kraken.Core;
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
        private readonly CalibrationReadingsRepository _readingsRepo;
        private readonly IScreen _screen;
        private AxesCalibrationReadings _currentResolutionReadings;
        private readonly TimeSpan _ServoSettleTime;

        private readonly ColourDetector _colourDetector;
        public ColourDetectSettings Settings { get; set; }
        public Func<Image<Bgr, byte>> GetCameraCapture { get; set; }

        public Action<string> WaitStep { get; set; }

        public event EventHandler<ColourDetectorProcessOutput> ColourCaptured;

        public CalibratingPanTiltController(IPanTiltMechanism panTiltMech, CalibrationReadingsRepository readingsRepo, IScreen screen)
            : base(panTiltMech)
        {
            _colourDetector = new ColourDetector();
            _readingsRepo = readingsRepo;
            _screen = screen;

            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                _ServoSettleTime = TimeSpan.FromMilliseconds(5000);
            }
            else
            {
                _ServoSettleTime = TimeSpan.FromMilliseconds(0);
            }
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
            _readingsRepo.ToCsv(allReadings);

            ResetToCenter();
            _screen.Clear();
            _screen.WriteLine("Calibration written to disk");
        }

        private void WaitServo(string reasonFormat, params object[] reasonArgs)
        {
            DoStep(reasonFormat, reasonArgs);
            Thread.Sleep((int)_ServoSettleTime.TotalMilliseconds);
        }

        private void DoStep(string reasonFormat, params object[] reasonArgs)
        {
            var reason = DateTime.Now.ToString("HH:mm:ss fff") + " " + string.Format(reasonFormat, reasonArgs);
            _screen.WriteLine("Waiting: {0}", reason);
            //WaitStep(reason);
            
            //Task.Delay((int)_ServoSettleTime.TotalMilliseconds).Wait();
            //
            //_screen.WriteLine("...done waiting", _ServoSettleTime.ToHumanReadable());
        }

        private void CalibrateHalfAxis(int signMovement, PanTiltAxis axis)
        {
            const decimal saccadePercentIncrement = 0.1m;
            decimal accumulatedDeviation = 0;
            bool foundColour;
            do
            {
                _screen.BeginRepaint();
                ResetToCenter();
                WaitServo("Relocated to center position. About to capture first detection");
                var firstDetection = LocateColour();

                DoStep("Center detection @ {0} is {1}", CurrentSetting, firstDetection.CentralPoint);

                accumulatedDeviation += signMovement * saccadePercentIncrement;
                
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
                WaitServo("Settle time after moving to new detection location {0}. About to capture new detection.", movementRequired);
                
                var newDetection = LocateColour();
                DoStep("New detection @ {0} is {1}", CurrentSetting, newDetection.CentralPoint);

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

                _screen.WriteLine("First={0}", firstDetection.CentralPoint);
                _screen.WriteLine("New={0}", newDetection.CentralPoint);
            }
            while (foundColour && Math.Abs(accumulatedDeviation) < 60);
        }

        private void ResetToCenter()
        {
            MoveAbsolute(new PanTiltSetting(50m, 50m)); // start center
        }

        private ColourDetectorProcessOutput LocateColour()
        {
            var output = new ColourDetectorProcessOutput();
            const int captureBufferBurn = 2;    // first image is stale, need to capture the second one
            Image<Bgr, byte> capturedImage = null;
            for (int i = 0; i < captureBufferBurn; i++)
            {
                capturedImage = GetCameraCapture();
                //DoStep("Captured {0} image", i);
            }
            
            
            using (var capturedMat = capturedImage.Mat)
            {
                var colourDetectorInput = new ColourDetectorInput();
                colourDetectorInput.Captured = capturedMat;
                colourDetectorInput.SetCapturedImage = false;
                colourDetectorInput.Settings = Settings;

                output = _colourDetector.Process(colourDetectorInput);
            }
         
            if (ColourCaptured != null)
            {
                output.CapturedImage = capturedImage;
                ColourCaptured(this, output);
            }

            return output;
        }

    }
}
