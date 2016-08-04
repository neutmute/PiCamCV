using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Common.Logging;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;
using Kraken.Core;

namespace PiCamCV.Common.Interfaces
{
    public class CameraProcessOutput
    {
        public Image<Bgr, byte> CapturedImage { get; set; }
        public TimeSpan Elapsed { get; internal set; }

        public override string ToString()
        {
            return string.Format("Elapsed={0}", Elapsed.ToHumanReadable());
        }
    }

    public class CameraProcessInput
    {
        /// <summary>
        /// Turn off for console perf tweak
        /// </summary>
        public bool SetCapturedImage { get; set; }

        public Mat Captured { get; set; }
        
        public CameraProcessInput()
        {
            SetCapturedImage = true;
        }
    }


    public interface ICameraProcessor<TInput, TResult> where TInput : CameraProcessInput where TResult : CameraProcessOutput
    {
        TResult Process(TInput input);
    }

    public abstract class CameraProcessor<TInput, TResult> : DisposableObject, ICameraProcessor<TInput, TResult> where TInput:CameraProcessInput 
        where TResult:CameraProcessOutput
    {
        private readonly static ILog _log = LogManager.GetLogger("CameraProcessor");
        protected ILog Log { get { return _log; } }

        public TResult Process(TInput input)
        {
            var stopWatch = Stopwatch.StartNew();
            var result = DoProcess(input);

            if (result.CapturedImage == null && input.SetCapturedImage)
            {
                result.CapturedImage = input.Captured.ToImage<Bgr, byte>();
            }

            result.Elapsed = stopWatch.Elapsed;
            return result;
        }
        protected abstract TResult DoProcess(TInput input);

        protected override void DisposeObject()
        {
            
        } 
    }
}
