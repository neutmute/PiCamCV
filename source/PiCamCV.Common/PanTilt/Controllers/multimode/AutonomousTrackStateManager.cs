using System;
using System.Diagnostics;
using PiCamCV.Common.Interfaces;
using PiCamCV.ConsoleApp.Runners.PanTilt;

namespace PiCamCV.Common.PanTilt.Controllers.multimode
{
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
        private TimeTarget _timeTarget;
        private IScreen _screen;

        private Random _random;

        private AutonomousState _internalState;

        public Func<CameraProcessInput, bool> IsFaceFound { get; set; }

        private IPanTiltController _panTiltController;

        //public Func<PanTiltSetting> GetCurrentPosition { get; set; }

        //public Func<A> 

        enum AutonomousState
        {
            Waiting
            ,SmoothPursuit
        }

        public AutonomousTrackStateManager(
            IPanTiltController panTiltController
            ,IScreen screen) : base(screen)
        {
            _screen = screen;
            _panTiltController = panTiltController;
            Reset();
        }

        public void Reset()
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
            _timeTarget = new TimeTarget();
            _timeTarget.Original = _panTiltController.CurrentSetting;
            
            var nextPan = Convert.ToDecimal(_random.Next(0, 100));
            var nextTilt = Convert.ToDecimal(_random.Next(0, 100));

            _timeTarget.Target = new PanTiltSetting(nextPan, nextTilt);

        }

        public ProcessingMode AcceptInput(CameraProcessInput input)
        {
            if (_timeSinceLastFaceSample.Elapsed > _sampleFaceEvery)
            {
                if (IsFaceFound(input))
                {
                    return ProcessingMode.FaceDetection;
                }
                _timeSinceLastFaceSample.Restart();
            }

            if (_timeSinceLastColourSample.Elapsed > _sampleColourEvery)
            {
                //_screen.WriteLine("TODO: colour sample");
                _timeSinceLastColourSample.Restart();
            }

            if (_timeSinceLastSmoothPursuit.Elapsed > _nextSmoothPursuit)
            {
                _screen.WriteLine($"Starting smooth pursuit to {_timeTarget.Target}");
                _internalState = AutonomousState.SmoothPursuit;
            }

            if (_internalState == AutonomousState.SmoothPursuit)
            {
                var nextPosition = _timeTarget.GetNextPosition();
                _panTiltController.MoveAbsolute(nextPosition);

                if (_timeTarget.TimeTargetReached)
                {
                    DecideNextSmoothPursuit();
                    _screen.WriteLine("Smooth pursuit target reached");
                    _internalState = AutonomousState.Waiting;
                }
            }

            return ProcessingMode.Autonomous;
        }
    }
}