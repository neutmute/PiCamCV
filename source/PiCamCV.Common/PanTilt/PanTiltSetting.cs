namespace PiCamCV.ConsoleApp.Runners.PanTilt
{
    public enum PanTiltSettingCommandType
    {
        Unknown = 0,
        MoveAbsolute,
        MoveRelative,
        MoveSmooth,
        SetRangeMotionUpper,
        SetRangeMotionLower,
        SetRangePursuitUpper,
        SetRangePursuitLower
    }

    public class PanTiltSettingCommand: PanTiltSetting
    {
        public PanTiltSettingCommandType Type { get; set; }

        public override string ToString()
        {
            return $"{Type}: {base.ToString()}";
        }
    }

    public class PanTiltSetting
    {
        public decimal? PanPercent { get; set; }
        public decimal? TiltPercent{get;set;}
        
        public PanTiltSetting(decimal pan, decimal tilt)
        {
            PanPercent = pan;
            TiltPercent = tilt;
        }

        public PanTiltSetting()
        {
            
        }

        public PanTiltSetting Clone()
        {
            var clone = new PanTiltSetting();
            clone.TiltPercent = TiltPercent;
            clone.PanPercent = PanPercent;
            return clone;
        }

        public override string ToString()
        {
            return $"Pan={PanPercent:N2}%, Tilt={TiltPercent:N2}%";
        }
    }
}