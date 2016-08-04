rem @echo off
@echo on
SET EmguCV_Bin_Folder=D:\CodeOther\emgucv\bin
SET SolutionFolder=%1
SET TargetBinaryFolder=%2
echo Getting openCV x64 binaries
xcopy /Y /I %EmguCV_Bin_Folder%\x64\release\*.* %TargetBinaryFolder%x64
REM echo Getting openCV x86 binaries
REM xcopy /Y /I %EmguCV_Bin_Folder%\bin\x86\*.* %TargetBinaryFolder%x86
echo Getting emgucv assemblies

xcopy /Y /I %EmguCV_Bin_Folder%\Emgu.CV.UI.* %SolutionFolder%\lib\emgucv
xcopy /Y /I %EmguCV_Bin_Folder%\Emgu.CV.World.* %SolutionFolder%\lib\emgucv
