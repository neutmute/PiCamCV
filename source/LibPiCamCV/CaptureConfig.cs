using System;
using System.Text.RegularExpressions;

namespace PiCamCV.Common
{
    public class CaptureConfig
    {
        public Resolution Resolution { get; set; }
        public int Bitrate { get;set; }
        public int Framerate { get;set; }
        public bool Monochrome { get; set; }

        public CaptureConfig()
        {
            Resolution = new Resolution();
        }

        /// <summary>
        /// Expect of the form '120x340,40' say
        /// </summary>
        public static CaptureConfig Parse(string config)
        {
            string strRegex = @"(?<resWidth>\d{1,})x(?<resHeight>\d{1,}),(?<fps>\d{1,})";
            var captureRegex = new Regex(strRegex, RegexOptions.None);
            var match = captureRegex.Match(config);
            if (match.Success)
            {
                var captureConfig = new CaptureConfig();
                try
                {
                    captureConfig.Framerate = Convert.ToInt32(match.Groups["fps"].Value);
                    captureConfig.Resolution.Width = Convert.ToInt32(match.Groups["resWidth"].Value);
                    captureConfig.Resolution.Height = Convert.ToInt32(match.Groups["resHeight"].Value);
                    captureConfig.Bitrate = 100000;
                }
                catch(Exception e)
                {
                    throw new ArgumentException($"CaptureConfig='{config}' is invalid", e);
                }
                return captureConfig;
            }
            return null;
        }

        public override string ToString()
        {
            return $"res={Resolution}, bitrate={Bitrate}, framerate={Framerate}, monochrome={Monochrome}";
        }
    }
}