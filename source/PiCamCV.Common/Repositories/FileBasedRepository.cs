using System.IO;
using Kraken.Core;

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
        protected abstract string Filename { get; }
        public bool IsPresent
        {
            get { return GetFileInfo().Exists; }
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
                output = Kelvin<T>.FromXmlFile(settingsFile.FullName);
            }
            return output;
        }
    }
}