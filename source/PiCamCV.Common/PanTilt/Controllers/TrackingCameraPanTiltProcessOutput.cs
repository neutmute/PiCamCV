namespace PiCamCV.ConsoleApp.Runners.PanTilt
{
    public abstract class TrackingCameraPanTiltProcessOutput : CameraPanTiltProcessOutput
    {
        public abstract bool IsDetected { get; }
    }
}