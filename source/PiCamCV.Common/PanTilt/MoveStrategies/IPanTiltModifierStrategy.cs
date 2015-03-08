namespace PiCamCV.ConsoleApp.Runners.PanTilt.MoveStrategies
{
    internal interface IPanTiltModifierStrategy
    {
        PanTiltSetting CalculateNewSetting(PanTiltSetting currentSetting);
    }
}