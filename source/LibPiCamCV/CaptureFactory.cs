using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PiCamCV.Interfaces;

namespace PiCamCV
{
    public enum CaptureDevice
    {
        Unknown = 0,

        /// <summary>
        /// EmguCV. Windows or Pi USB connected camera
        /// </summary>
        Usb =1,

        /// <summary>
        /// Will use Pi Camera module on a Pi. USB on Windows.
        /// </summary>
        Pi = 2
    }

    public static class CaptureFactory
    {
        public static ICaptureGrab GetCapture(CaptureDevice device)
        {
            if (device == CaptureDevice.Pi)
            {
                return new CapturePi();
            }
            else
            {
                return new CaptureUsb();
            }
        }
    }
}
