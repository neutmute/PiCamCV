using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiCamCV.Common
{
    public interface IClassifierParametersRepository : IFileBasedRepository<ClassifierParameters>
    {
    }

    public class CascadeClassifierSettingsRepository : FileBasedRepository<ClassifierParameters>, IClassifierParametersRepository
    {
        protected override string Filename => "classifiersettings.xml";
    }
}
