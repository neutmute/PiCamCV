using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kraken.Core;

namespace PiCamCV.Common
{
    public interface IColourSettingsRepository
    {
        bool IsPresent { get; }
        void Write(ColourDetectSettings settings);
        ColourDetectSettings Read();
    }

    public class ColourSettingsRepository : IColourSettingsRepository
    {
        public bool IsPresent
        {
            get { return GetFileInfo().Exists; }
        }

        private FileInfo GetFileInfo()
        {
            var appData = ExecutionEnvironment.GetApplicationMetadata();
            var settingsFilename = Path.Combine(appData.ExeFolder, "colordetectsettings.xml");
            var colorSettingsFile = new FileInfo(settingsFilename);
            return colorSettingsFile;
        }

        public void Write(ColourDetectSettings settings)
        {
            var fileinfo = GetFileInfo();
            Kelvin<ColourDetectSettings>.ToXmlFile(settings, fileinfo.FullName);
        }

        public ColourDetectSettings Read()
        {
            var settingsFile = GetFileInfo();
            ColourDetectSettings output = null;
            if (settingsFile.Exists)
            {
                output = Kelvin<ColourDetectSettings>.FromXmlFile(settingsFile.FullName);
            }
            return output;
        }
    }
}
