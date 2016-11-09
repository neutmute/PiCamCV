using System;
using System.IO;
using Common.Logging;
using Kraken.Core;
using PiCamCV.Common.PanTilt.MoveStrategies;

namespace PiCamCV.Common
{
    public interface IFileBasedRepository<T>
    {
        bool IsPresent { get; }
        void Write(T settings);
        T Read();
    }

    public abstract class FileBasedRepository<T> : IFileBasedRepository<T>
    {
        private ILog Log = LogManager.GetLogger<AutoCalibratedModifierStrategy>();
       
        protected abstract string Filename { get; }
        public bool IsPresent
        {
            get
            {
                var fileExists = GetFileInfo().Exists;
                var canRead = true;
                if (fileExists)
                {
                    try
                    {
                        var text = File.ReadAllText(GetFileInfo().FullName);
                    }
                    catch (UnauthorizedAccessException e)
                    {
                        Log.Warn($"Access denied: {GetFileInfo().Name}");
                        canRead = false;
                    }
                }
                return fileExists && canRead;
            }
        }

        protected FileInfo GetFileInfo()
        {
            return GetFileInfo(Filename);
        }

        protected FileInfo GetFileInfo(string filename83)
        {
            var appData = ExecutionEnvironment.GetApplicationMetadata();
            var settingsFilename = Path.Combine(appData.ExeFolder, filename83);
            var colorSettingsFile = new FileInfo(settingsFilename);
            return colorSettingsFile;
        }

        public void Write(T settings)
        {
            var fileinfo = GetFileInfo();
            Kelvin<T>.ToXmlFile(settings, fileinfo.FullName);
        }

        public T Read()
        {
            var settingsFile = GetFileInfo();
            T output = default(T);
            if (settingsFile.Exists)
            {
                var text = File.ReadAllText(GetFileInfo().FullName);
                output = Kelvin<T>.FromXml(text);
            }
            else
            {
                Log.WarnFormat("Settings file '{0}' does not exist", settingsFile.Name);
            }
            return output;
        }
    }
}