using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PiCamCV.Common.Interfaces;
using PiCamCV.ConsoleApp.Runners.PanTilt;

namespace PiCamCV.Common.PanTilt.Controllers.multimode
{
    public abstract class StateManager
    {
        private readonly Stopwatch _stopWatch;

        public virtual TimeSpan AbandonDetectionAfterMissing => TimeSpan.FromSeconds(5);

        public TimeSpan TimeSinceLastDetection => _stopWatch.Elapsed;

        protected IScreen Screen { get; }

        protected StateManager(IScreen screen)
        {
            _stopWatch = new Stopwatch();
            Screen = screen;
        }

        public void RegisterDetection()
        {
            _stopWatch.Restart();
        }
    }

    public class FaceTrackStateManager : StateManager
    {
        public Point LastDetection { get; set; }

        public FaceTrackStateManager(IScreen screen) :base(screen)
        {
                
        }

        public ProcessingMode AcceptScan(FaceTrackingPanTiltOutput output)
        {
            if (output.IsDetected)
            {
                LastDetection = output.Target;
                RegisterDetection();
                
                if (LastDetection == Point.Empty)
                {
                    Screen.WriteLine("Face detected");
                }
            }
            
            if (LastDetection != Point.Empty && TimeSinceLastDetection > AbandonDetectionAfterMissing)
            {
                Screen.WriteLine("Face lost. Resuming autonomous.");
                return ProcessingMode.Autonomous;
            }

            // Keep tracking
            return ProcessingMode.FaceDetection;
        }
    }

    public class AutonomousTrackStateManager : StateManager
    {
        private Stopwatch _timeSinceLastPursuit;
        private Stopwatch _timeSinceLastFaceSample;
        private Stopwatch _timeSinceLastColourSample;
        private Stopwatch _timeSinceLastSmoothPursuit;

        private TimeSpan _sampleFaceEvery = TimeSpan.FromMilliseconds(1000);
        private TimeSpan _sampleColourEvery = TimeSpan.FromMilliseconds(2000);

        private TimeSpan _nextSmoothPursuit;
        private int _nextSmoothPursuitSpeed;

        private Random _random;

        private AutonomousState _internalState;

        enum AutonomousState
        {
            Waiting
            ,SmoothPursuit
        }

        public AutonomousTrackStateManager(IScreen screen) : base(screen)
        {
            _timeSinceLastColourSample = Stopwatch.StartNew();
            _timeSinceLastFaceSample = Stopwatch.StartNew();
            _timeSinceLastColourSample = Stopwatch.StartNew();
            _timeSinceLastSmoothPursuit = Stopwatch.StartNew();

            _internalState = AutonomousState.Waiting;

            _random = new Random();

            DecideNextSmoothPursuit();
        }

        private void DecideNextSmoothPursuit()
        {
            _nextSmoothPursuit = TimeSpan.FromSeconds(_random.Next(3, 20));
            _nextSmoothPursuitSpeed = _random.Next(3, 10);
            _timeSinceLastSmoothPursuit.Restart();
        }

        public ProcessingMode AcceptInput(CameraProcessInput input)
        {
            if (_timeSinceLastFaceSample.Elapsed > _sampleFaceEvery)
            {
                // do sample and test
                _timeSinceLastFaceSample.Restart();
            }

            if (_timeSinceLastColourSample.Elapsed > _sampleColourEvery)
            {
                // do sample and test
                _timeSinceLastColourSample.Restart();
            }

            if (_timeSinceLastSmoothPursuit.Elapsed > _nextSmoothPursuit)
            {
                // init smooth pursuit
                DecideNextSmoothPursuit();
            }

            return ProcessingMode.Autonomous;
        }
    }
}
