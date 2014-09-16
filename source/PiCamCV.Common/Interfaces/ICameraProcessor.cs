using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Common.Logging;
using Emgu.CV;

namespace PiCamCV.Common.Interfaces
{
    public class CameraProcessOutput
    {
        public TimeSpan Elapsed { get; internal set; }
    }

    public class CameraProcessInput
    {
        public Mat Captured { get; set; }
    }


    public abstract class CameraProcessor<TInput, TResult> 
        where TInput:CameraProcessInput 
        where TResult:CameraProcessOutput
    {
        private readonly static ILog _log = LogManager.GetCurrentClassLogger();
        protected ILog Log { get { return _log; } }

        public TResult Process(TInput input)
        {
            var stopWatch = Stopwatch.StartNew();
            var result = DoProcess(input);
            result.Elapsed = stopWatch.Elapsed;
            return result;
        }
        protected abstract TResult DoProcess(TInput input);
    }
}
