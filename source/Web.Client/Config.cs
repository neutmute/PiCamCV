using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Client
{
    /// <summary>
    /// # WINDOWS POWERSHELL. Restart visual studio afterward
    /// [Environment]::SetEnvironmentVariable("PiCamCv:Web:Server", "localhost", "User") 
    /// [Environment]::SetEnvironmentVariable("PiCamCv:Web:Port", "4091", "User") 
    /// </summary>
    public static class Config
    {
        public static string ServerHost { get; private set; }

        public static string ServerPort { get; private set; }

        
        static Config()
        {
            ServerHost = Environment.GetEnvironmentVariable("PiCamCv:Web:Server", EnvironmentVariableTarget.User);
            ServerPort = Environment.GetEnvironmentVariable("PiCamCv:Web:Port", EnvironmentVariableTarget.User);
        }
    }
}
