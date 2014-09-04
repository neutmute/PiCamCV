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
            var elapsed = stopWatch.Elapsed;
            Log.Trace(m => result.ToString());
            return result;
        }
        protected abstract TResult DoProcess(TInput input);
    }
}
