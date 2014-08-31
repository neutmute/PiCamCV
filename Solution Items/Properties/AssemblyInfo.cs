using System.Reflection;

#if DEBUG && UNIX
    [assembly: AssemblyConfiguration("UNIX Debug")]
#elif DEBUG
    [assembly: AssemblyConfiguration("Windows Debug")]
//#elif RELEASE && UNIX
//    [assembly: AssemblyConfiguration("UNIX Release")]
#else
    [assembly: AssemblyConfiguration("Windows Release")]
#endif

[assembly: AssemblyCompany("Neutmute")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: AssemblyVersion("1.0.*")]


