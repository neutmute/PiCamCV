param(
    [string]$configuration = "Debug"
)

. ".\build.common.ps1"

$solutionName = "PiCamCV"

function init {
    # Initialization
    $global:rootFolder = Split-Path -parent $script:MyInvocation.MyCommand.Path
    $global:rootFolder = Join-Path $rootFolder .
    $global:packagesFolder = Join-Path $rootFolder packages
    $global:outputFolder = Join-Path $rootFolder _artifacts
    $global:msbuild = "C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe"

    _WriteOut -ForegroundColor $ColorScheme.Banner "-= $solutionName Build =-"
    _WriteConfig "rootFolder" $rootFolder
}

function restorePackages{

    _WriteOut -ForegroundColor $ColorScheme.Banner "nuget restore"
    
    New-Item -Force -ItemType directory -Path $packagesFolder
    _DownloadNuget $packagesFolder
    nuget restore
}

function buildSolution{

    #& "$rootFolder\lib\raspberry-sharp-io\build.cmd"
    #& "$rootFolder\lib\RPi.Demo\build.cmd"    

    _WriteOut -ForegroundColor $ColorScheme.Banner "Build Solution"
    & $msbuild "$rootFolder\$solutionName.sln" /p:Configuration=$configuration

}

function executeTests{

    Write-Host "Execute Tests"

    $testResultformat = ""
    $nunitConsole = "$rootFolder\packages\NUnit.Runners.2.6.4\tools\nunit-console.exe"

    if(Test-Path Env:\APPVEYOR){
        $testResultformat = ";format=AppVeyor"
        $nunitConsole = "nunit3-console"
    }

    & $nunitConsole .\tests\PiCamCV.Common.Tests\bin\$configuration\PiCamCV.Common.Tests.dll --result=.\tests\PiCamCV.Common.Tests\bin\$configuration\nunit-results.xml$testResultformat

    checkExitCode
}

init

restorePackages

buildSolution

executeTests

Write-Host "Build $configuration complete"