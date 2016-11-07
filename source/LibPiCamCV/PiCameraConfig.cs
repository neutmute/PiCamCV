using System.Runtime.InteropServices;
using PiCamCV.Common;

namespace PiCamCV
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PiCameraConfig
    {
        public int Width;              
        public int Height;             
        public int Bitrate;            
        public int Framerate;          
        public int Monochrome;

        public override string ToString()
        {
            return string.Format(
                "w={0}, h={1}, bitrate={2}, framerate={3}, monochrome={4}"
                , Width
                , Height
                , Bitrate
                , Framerate
                , Monochrome
                );
        }

        public static PiCameraConfig FromConfig(CaptureConfig config)
        {
            var s = new PiCameraConfig();

            if (config == null)
            {
                s.Width = 0;
                s.Height = 0;
                s.Bitrate = 0;
                s.Framerate = 0;
                s.Monochrome = 0;
            }
            else
            {
                s.Width = config.Resolution.Width;
                s.Height = config.Resolution.Height;
                s.Bitrate = config.Bitrate;
                s.Framerate = config.Framerate;
                s.Monochrome = config.Monochrome ? 1 : 0;
            }

            return s;
        }
    };
}