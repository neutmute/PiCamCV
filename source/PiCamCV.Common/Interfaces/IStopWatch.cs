using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PiCamCV.Common.Interfaces
{
    public interface IStopwatch
    {
        long ElapsedMilliseconds { get; }

        TimeSpan Elapsed { get; }

        void Restart();
    }

    
}
