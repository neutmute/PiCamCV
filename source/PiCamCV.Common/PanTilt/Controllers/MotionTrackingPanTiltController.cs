using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Common.Logging;
using Emgu.CV;
using Emgu.CV.Structure;
using Kraken.Core;
using PiCamCV.Common.ExtensionMethods;
using PiCamCV.Common.Interfaces;
using PiCamCV.ConsoleApp.Runners.PanTilt;
using PiCamCV.ExtensionMethods;

namespace PiCamCV.Common.PanTilt.Controllers
{
    public class MotionTrackingPanTiltOutput : TrackingCameraPanTiltProcessOutput
    {
        public List<MotionSection> MotionSections { get; private set; }

        public MotionSection TargetedMotion { get;set;}

        public Image<Bgr, byte> ForegroundImage { get; set; }

        public override bool IsDetected => MotionSections.Count > 0;

        public MotionTrackingPanTiltOutput()
        {
            MotionSections = new List<MotionSection>();
        }
    }

    public class MotionTrackingPanTiltController : CameraBasedPanTiltController<MotionTrackingPanTiltOutput>
    {
        private static readonly ILog _log = LogManager.GetLogger< PanTiltController>();

        private readonly IScreen _screen;

        private readonly Timer _timerUntilMotionSettled;

        private Stopwatch _timeToZeroMotion;
        
        public MotionDetectSettings Settings { get; set; }

        private readonly MotionDetector _motionDetector;

        public TimeSpan MotionSettleTime
        {
            get { return TimeSpan.FromMilliseconds(_timerUntilMotionSettled.Interval); }
            set { _timerUntilMotionSettled.Interval = value.TotalMilliseconds; }
            
        }

        public MotionTrackingPanTiltController(IPanTiltMechanism panTiltMech, CaptureConfig captureConfig, IScreen screen)
            : base(panTiltMech, captureConfig)
        {
            _screen = screen;
            _motionDetector = new MotionDetector();

            ServoSettleTime = TimeSpan.FromMilliseconds(200);

            _timerUntilMotionSettled = new Timer(200);
            _timerUntilMotionSettled.AutoReset = false;
            _timerUntilMotionSettled.Elapsed += (o, a) =>
            {
                _screen.WriteLine("Motion settled");
                IsServoInMotion = false;
            };

            ServoSettleTimeChanged += (o, a) =>
            {
                _screen.WriteLine("Servo settle={0}", ServoSettleTime.ToHumanReadable());
                _timerUntilMotionSettled.Interval = ServoSettleTime.TotalMilliseconds;
            };
        }

        protected override MotionTrackingPanTiltOutput DoProcess(CameraProcessInput input)
        {
            var detectorInput = new MotionDetectorInput();
            detectorInput.Settings = Settings;
            detectorInput.SetCapturedImage = input.SetCapturedImage;
            detectorInput.Captured = input.Captured;

            var motionOutput = _motionDetector.Process(detectorInput);

            var targetPoint = CentrePoint;
            MotionSection biggestMotion = null;
            
            if (motionOutput.IsDetected)
            {
                _screen.BeginRepaint();
                biggestMotion = motionOutput.BiggestMotion;
                targetPoint = biggestMotion.Region.Center();
            }

            var output = ReactToTarget(targetPoint);
            if (IsServoInMotion)
            {
                _screen.WriteLine("Reacting to target {0}, size {1}", targetPoint, biggestMotion.Region.Area());

                if (_timeToZeroMotion != null && !motionOutput.IsDetected)
                {
                    _timeToZeroMotion.Stop();
                    _log.InfoFormat("Time to zero motion was {0:F}ms", _timeToZeroMotion.ElapsedMilliseconds);
                    _timeToZeroMotion = null;
                }
            }
            output.MotionSections.AddRange(motionOutput.MotionSections);

            if (biggestMotion != null)
            {
                output.TargetedMotion = motionOutput.BiggestMotion;
            }

            output.ForegroundImage = motionOutput.ForegroundImage;

            return output;
        }

        protected override void PostServoSettle()
        {
            IsServoInMotion = true;
            _screen.WriteLine("Servo moved, waiting {0:F}ms for motion settle", _timerUntilMotionSettled.Interval);
            _motionDetector.Reset();
            _timeToZeroMotion = new Stopwatch();
            _timeToZeroMotion.Start();
            _timerUntilMotionSettled.Start();
        }
    }
}
