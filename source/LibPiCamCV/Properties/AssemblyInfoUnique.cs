using System.Reflection;

[assembly: AssemblyTitle("LibPiCamCV")]
[assembly: AssemblyDescription("Wrapper for C raspiCamCV shared library")]

#if DEBUG && UNIX
[assembly: AssemblyProduct("LibPiCamCV - UNIX Debug")]
#elif DEBUG
    [assembly: AssemblyProduct("LibPiCamCV - Windows Debug")]
//#elif RELEASE && UNIX
//    [assembly: AssemblyProduct("LibPiCamCV - UNIX Release")]
#else
    [assembly: AssemblyProduct("LibPiCamCV - Windows Release")]
#endif

