using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Common.Logging;
using Emgu.CV.Structure;
using Kraken.Core;
using NDesk.Options;
using PiCamCV.Common;

namespace PiCamCV.ConsoleApp
{
    public enum Mode
    {
        Help = 0,
        simple,
        colourdetect,
        haar,
        servosort,
        pantiltjoy,
        pantiltface,
        pantiltcolour,
        pantiltmultimode,
        noop
    }

    public class ConsoleOptions
    {
        protected static ILog Log = LogManager.GetLogger< ConsoleOptions>();
        public Mode Mode { get; set; }

        public bool ShowHelp;

        public MCvScalar LowThreshold { get; set; }

        public MCvScalar HighThreshold { get; set; }

        public OptionSet OptionSet { get; private set; }

        public bool HasColourSettings { get; private set; }

        public bool UseFakeDevice { get; set; }

        public ColourDetectSettings ColourSettings { get; set; }

        public ConsoleOptions(string[] args)
        {
            ReadColorSettings();

            OptionSet = new OptionSet {
                { "m|mode=", "Name of the mode to execute. [" + Enumeration.GetAll<Mode>().ToCsv(",", m => m.ToString()) + "]", v =>
                {
                    Mode outMode;
                    if (Enum.TryParse(v, out outMode))
                    {
                        Mode = outMode;
                    }
                    else
                    {
                        ShowHelp = true;
                    }
                }},
                { "nopwm", "Do not try and connect to a real PWM device", v => UseFakeDevice=true},
                //{ "t|threshold=", "Color thresholds for colour detection [Hl,Sl,Vl+Hh,Sh,Vh]. eg: -t=140,57,25+187,153,82 or -t=155,128,44+182,214,105"
                //    , v =>
                //    {
                //        var lowHigh = v.Split('+');
                //        var low =  lowHigh[0].Split(',').ToList().ConvertAll(Convert.ToInt32);
                //        var high = lowHigh[1].Split(',').ToList().ConvertAll(Convert.ToInt32);

                //        LowThreshold = new MCvScalar(low[0], low[1], low[2]);
                //        HighThreshold = new MCvScalar(high[0], high[1], high[2]);
                //        HasColourSettings = true;
                //    }},
                { "h|?", "Show this help", v => ShowHelp = true }
            };
            OptionSet.Parse(args);
            if (Mode == Mode.Help)
            {
                ShowHelp = true;
            }
        }

        private void ReadColorSettings()
        {
            var repo = new ColourSettingsRepository();
            if (repo.IsPresent)
            {
                ColourSettings = repo.Read();
                HasColourSettings = true;
                Log.Info(m => m("Color detection settings found: {0}", ColourSettings));
            }
            else
            {
                Log.Info(m => m("No color detection settings found"));
            }
        }

        public override string ToString()
        {
            var s = new StringBuilder();

            s.AppendFormat("Mode={0}", Mode);

            if (HasColourSettings)
            {
                s.AppendFormat(
                    ", LowThreshold={0}, HighThreshold={1}"
                    , LowThreshold.ToArray().ToCsv(',', d=>d.ToString())
                    , HighThreshold.ToArray().ToCsv(',', d => d.ToString()));
            }

            if (UseFakeDevice)
            {
                s.AppendFormat(", UseFakeDevice=true", Environment.NewLine);
            }
            return s.ToString();
        }

    }
}
