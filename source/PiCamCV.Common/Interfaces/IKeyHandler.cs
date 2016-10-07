using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiCamCV.Common.Interfaces
{
    public interface IKeyHandler
    {
        bool HandleKeyPress(char key);
    }
}
