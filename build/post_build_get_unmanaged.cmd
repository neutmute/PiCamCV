rem @echo off
@echo on
SET EmguCVRelativePath=..\emgu-source
SET SolutionFolder=%1
SET TargetBinaryFolder=%2
echo Getting x64 binaries
xcopy /Y /I %SolutionFolder%%EmguCVRelativePath%\bin\x64\*.* %TargetBinaryFolder%x64
echo Getting x86 binaries
xcopy /Y /I %SolutionFolder%%EmguCVRelativePath%\bin\x86\*.* %TargetBinaryFolder%x86