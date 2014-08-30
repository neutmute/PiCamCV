rem @echo off
@echo on
SET EmguCVRelativePath=..\emgu-source
SET SolutionFolder=%1
SET TargetBinaryFolder=%2
dir %SolutionFolder%..\emgucv-source\bin\x86\*.*
xcopy /Y /I %SolutionFolder%%EmguCVRelativePath%\bin\x64\*.* %TargetBinaryFolder%x64
xcopy /Y /I %SolutionFolder%%EmguCVRelativePath%\bin\x86\*.* %TargetBinaryFolder%x86