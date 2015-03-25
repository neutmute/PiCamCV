using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiCamCV.Common
{
    public interface IColourSettingsRepository : IFileBasedRepository<ColourDetectSettings>
    {
    }

    public class ColourSettingsRepository : FileBasedRepository<ColourDetectSettings>, IColourSettingsRepository
    {
        protected override string Filename
        {
            get { return "colourdetectsettings.xml"; }
         }
    }
}
