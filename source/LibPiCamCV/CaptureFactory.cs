﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Common.Logging;
using PiCamCV.Interfaces;

namespace PiCamCV
{
    public class CaptureRequest
    {
        public CaptureDevice Device { get; set; }

        public int CameraIndex { get; set; }

        public FileInfo File { get; set; }

        public override string ToString()
        {
            if (File != null)
            {
                return string.Format("Device={0}, Camera Index={1}", Device, CameraIndex);    
            }
            return string.Format("Video='{0}'", File.FullName);
        }
    }

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
        static readonly ILog Log = LogManager.GetCurrentClassLogger();
        public static ICaptureGrab GetCapture(CaptureRequest request)
        {
            Log.Info(m => m("CV Library={0}", CvInvokeRaspiCamCV.CVLibrary));
            Log.Info(m => m("Capturing {0}", request));

            if (request.File != null)
            {
                EmitFileWarnings(request);
                return new CaptureFile(request.File.FullName);
            }
            else
            {
                EmitUsbWarnings();
                if (request.Device == CaptureDevice.Pi)
                {
                    return new CapturePi();
                }
                else
                {
                    return new CaptureUsb(request.CameraIndex);
                }
            }
        }

        private static void EmitFileWarnings(CaptureRequest request)
        {
            if (!request.File.Exists)
            {
                Log.Error(m => m("Video file '{0}' not found", request.File.FullName));
            }
        }

        private static void EmitUsbWarnings()
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                Log.Warn("You are in Unix but aren't requesting a Pi camera? Whatever you say boss...");
                if (CvInvokeRaspiCamCV.CVLibrary.Contains("opencv"))
                {
                    Log.Warn("You are in Unix but trying to bind to opencv libraries - not raspicamcv");
                }
            }
        }
    }
}
