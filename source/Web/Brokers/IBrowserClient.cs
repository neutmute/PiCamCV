using PiCam.Web.Models;

namespace Web
{
    public interface IBrowserClient
    {
        void ScreenWriteLine(string message);

        void ScreenClear();

        void Toast(string message);

        void ImageReady(string base64encodedImage = null);

        void InformSettings(ServerSettings settings);
    }
}