using System;
using System.Collections.Generic;
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
        private readonly static ILog _Log = LogManager.GetCurrentClassLogger();
        protected ILog Log { get { return _Log; } }
        public abstract TResult Process(TInput input);
    }
}
