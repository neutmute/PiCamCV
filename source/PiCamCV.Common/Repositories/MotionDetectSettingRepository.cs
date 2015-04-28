using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiCamCV.Common.Repositories
{
    public interface IMotionDetectSettingsRepository : IFileBasedRepository<MotionDetectSettings>
    {
    }

    public class MotionDetectSettingRepository : FileBasedRepository<MotionDetectSettings>, IMotionDetectSettingsRepository
    {
        protected override string Filename
        {
            get { return "motiondetectsettings.xml"; }
        }
    }
}
