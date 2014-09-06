[void][reflection.assembly]::LoadWithPartialName("System.Windows.Forms")
# cd D:\Data\Documents\Pictures\!Temp\batman\positive2
# D:
$scriptDir = Split-Path $script:MyInvocation.MyCommand.Path
$classifierDir="classifier"
 
 
# clean old vecs up
Remove-Item $scriptDir\*.* -filter *.vec
Remove-Item $scriptDir\$classifierDir -Force -Recurse
New-Item -ItemType Directory -Force -Path $scriptDir\$classifierDir

 # create the sample vecs
 $i = 0
 $sampleCount = 0
foreach ($file in Get-ChildItem -filter *.jpg) {
    $img = [System.Drawing.Image]::FromFile($file.FullName)
    $scale = $img.Width/80;
    $outWidth = 40 # [Convert]::ToInt32($img.Width/$scale)
    $outHeight= 80 #[Convert]::ToInt32($img.Height/$scale)
    $i++

    if ($i % 3 -eq 0)
    {
        $sampleCount++
        Write-Host "$($img.Height)"
        #Write-Host $file
        & opencv_createsamples.exe -vec $scriptDir\v$file.vec -img $file -num 1 -bgcolor 255 -bgthresh 0 -maxxangle 0 -maxyangle 0 -maxzangle 0 -maxidev 40 -w $outWidth -h $outHeight -bg ..\negative\negatives.txt
        #Write-Host " opencv_createsamples.exe -vec $scriptDir\v$file.vec -img $file -num 1 -bgcolor 255 -bgthresh 0 -maxxangle 0 -maxyangle 0 -maxzangle 0 -maxidev 40 -w 40 -h 80 -bg ..\negative\negatives.txt"
    }
}

# create master vec txt file
$vecListFilename = "zVectors.txt"
$mergedVecFileName = "zMerged.vec"
cmd /c "dir $scriptDir\*.vec /b > $vecListFilename"

# merge the vectors
& C:\apps\opencvmergevec\opencv_mergevec.exe $scriptDir\$vecListFilename $scriptDir\$mergedVecFileName

# train that app
& opencv_traincascade -data $classifierDir -vec $mergedVecFileName -bg ..\negative\negatives.txt -numStages 19 -minHitRate 0.999 -maxFalseAlarmRate 0.5 -numPos $sampleCount -numNeg 15 -w $outWidth -h $outHeight -mode ALL -precalcValBufSize 4024 -precalcIdxBufSize 6024