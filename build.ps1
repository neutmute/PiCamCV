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


init

restorePackages

buildSolution

Write-Host "Build $configuration complete"