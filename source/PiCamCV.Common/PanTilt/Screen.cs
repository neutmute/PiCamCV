using System;

namespace PiCamCV.ConsoleApp.Runners.PanTilt
{
    public class NullScreen : IScreen
    {
        public void Clear()
        {
        }

        public void BeginRepaint()
        {
        }

        public void WriteLine(string format, params object[] args)
        {
        }
    }
    public interface IScreen
    {
        void Clear();
        void BeginRepaint();
        void WriteLine(string format, params object[] args);
    }
}