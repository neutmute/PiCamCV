<#
Assumes 
 -ps1 script in folder with positive samples
 -opencv executables in PATH envrionment variable

#>
[void][reflection.assembly]::LoadWithPartialName("System.Windows.Forms")

$scriptDir = Split-Path $script:MyInvocation.MyCommand.Path
Set-Location -Path $scriptDir

$classifierDir="classifier"
$positiveImageListFilename = "zPosImages.txt" 
$vectorFileName = "zVectors.vec"
$outWidth = 20
$outHeight= 40
$bgColorEightBit=255 #white

 
# clean old vecs up
Remove-Item *.* -filter *.vec

# clean old lists up
Remove-Item *.* -filter *.txt

# create classifier folder
Remove-Item $classifierDir -Force -Recurse
New-Item -ItemType Directory -Force -Path $classifierDir

# create sample input info file and count how many +ve files we have
$positiveFileCounter = 0
$positiveFileContent = ""
$fileCounter = 0
foreach ($file in Get-ChildItem -filter *.jpg) 
{
    $fileCounter++
    if ($fileCounter % 3 -eq 0)  # modulus three to count down file count
    {    
        $positiveFileCounter++    
        Add-Content $positiveImageListFilename "$($file.Name) 1 0 0 40 80"
    }
}

#create samples
$sampleCmd = "& opencv_createsamples.exe -vec $vectorFileName -info $positiveImageListFilename -num $positiveFileCounter -bgcolor $bgColorEightBit -maxxangle 0 -maxyangle 0 -maxzangle 0 -maxidev 40 -w $outWidth -h $outHeight -bg ..\negative\negatives.txt"
Write-Host "$sampleCmd"
Invoke-Expression $sampleCmd

#view samples
$viewCmd = "opencv_createsamples.exe -vec $vectorFileName -w $outWidth -h $outHeight"
Write-Host "$viewCmd"

# train that app
$trainCmd = "opencv_traincascade -data $classifierDir -vec $vectorFileName -bg ..\negative\negatives.txt -numStages 24 -minHitRate 0.999 -maxFalseAlarmRate 0.5 -numPos $positiveFileCounter -numNeg 93 -w $outWidth -h $outHeight -mode ALL -precalcValBufSize 4024 -precalcIdxBufSize 6024"
Write-Host $trainCmd 
#& $trainCmd 