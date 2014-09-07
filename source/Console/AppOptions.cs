using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV.Structure;
using Kraken.Core;
using NDesk.Options;

namespace PiCamCV.ConsoleApp
{
    public enum Mode
    {
        Help = 0,
        simple,
        colourdetect,
        haar
    }

    public class ConsoleOptions
    {
        public Mode Mode { get; set; }

        public bool ShowHelp;

        public MCvScalar LowThreshold { get; set; }

        public MCvScalar HighThreshold { get; set; }

        public OptionSet OptionSet { get; private set; }

        public bool HasThresholds { get; private set; }

        public ConsoleOptions(string[] args)
        {
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
                { "t|threshold=", "Color thresholds for colour detection [Hl,Sl,Vl+Hh,Sh,Vh]. eg: -t=140,57,25+187,153,82 or -t=155,128,44+182,214,105"
                    , v =>
                    {
                        var lowHigh = v.Split('+');
                        var low =  lowHigh[0].Split(',').ToList().ConvertAll(Convert.ToInt32);
                        var high = lowHigh[1].Split(',').ToList().ConvertAll(Convert.ToInt32);

                        LowThreshold = new MCvScalar(low[0], low[1], low[2]);
                        HighThreshold = new MCvScalar(high[0], high[1], high[2]);
                        HasThresholds = true;
                    }},
                //{ "a|alarmdate=", "Set alarm for future datetime", v => {AlarmDate =  DateTime.Parse(v); Mode = Mode.AlarmClock;}},
                //{ "t|alarmtime=", "Set alarm for a future timespan - eg: a few seconds away", v => {AlarmDate =  DateTime.Now + TimeSpan.Parse(v); Mode = Mode.AlarmClock;}},
                { "h|?", "Show this help", v => ShowHelp = true }
            };
            OptionSet.Parse(args);
            if (Mode == Mode.Help)
            {
                ShowHelp = true;
            }
        }

        public override string ToString()
        {
            var s = new StringBuilder();

            s.AppendFormat("Mode={0}", Mode);

            if (HasThresholds)
            {
                s.AppendFormat(
                    ", LowThreshold={0}, HighThreshold={1}"
                    , LowThreshold.ToArray().ToCsv(',', d=>d.ToString())
                    , HighThreshold.ToArray().ToCsv(',', d => d.ToString()));
            }

            //if (UseFakeDevice)
            //{
            //    s.AppendFormat(", UseFakeDevice=true", Environment.NewLine);
            //}
            return s.ToString();
        }

    }
}
