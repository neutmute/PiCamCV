using System;

namespace PiCamCV.ConsoleApp.Runners.PanTilt
{
    public interface IScreen
    {
        void Clear();
        void BeginRepaint();
        void WriteLine(string format, params object[] args);
    }
}