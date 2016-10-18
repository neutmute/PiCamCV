using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Client
{
    /// <summary>
    /// # WINDOWS POWERSHELL. Restart visual studio afterward
    /// [Environment]::SetEnvironmentVariable("PiCamCv__Web__Server", "localhost", "User") 
    /// [Environment]::SetEnvironmentVariable("PiCamCv__Web__Port", "4091", "User") 
    /// 
    /// # RASPBERRY PI BASH. Add this to ~/.profile
    /// export PiCamCv__Web__Server=192.168.1.3
    /// export PiCamCv__Web__Port=80
    /// </summary>
    public static class Config
    {
        public static string ServerHost { get; private set; }

        public static string ServerPort { get; private set; }

        
        static Config()
        {
            ServerHost = Environment.GetEnvironmentVariable("PiCamCv__Web__Server", EnvironmentVariableTarget.User);
            ServerPort = Environment.GetEnvironmentVariable("PiCamCv__Web__Port", EnvironmentVariableTarget.User);
        }
    }
}
