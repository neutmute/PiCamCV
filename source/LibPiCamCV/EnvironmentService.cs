using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PiCamCV
{
    public class EnvironmentService
    {
        public static bool IsUnix
        {
            get
            {
                return (Environment.OSVersion.Platform == PlatformID.Unix);
            }
        }

        public static void DemandUnix(string message)
        {
            if (!IsUnix)
            {
                throw new NotSupportedException(message);
            }
        }
        public string GetAbsolutePathFromAssemblyRelative(string relativePath)
        {
            var assemblyPath = Assembly.GetExecutingAssembly().Location;
            var absolutePath = Path.Combine(new FileInfo(assemblyPath).DirectoryName, relativePath);
            return absolutePath;
        }
    }
}
