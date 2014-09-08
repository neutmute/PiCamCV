[void][reflection.assembly]::LoadWithPartialName("System.Windows.Forms")
 cd D:\Data\Documents\Pictures\!Temp\batman\positive2
 D:
 $scriptpath = $MyInvocation.MyCommand.Path

 # create the samples
 $i = 0
foreach ($file in Get-ChildItem -filter *.jpg) {
    $img = [System.Drawing.Image]::FromFile($file.FullName)
    $scale = $img.Width/80;
    $outWidth = [Convert]::ToInt32($img.Width/$scale)
    $outHeight = [Convert]::ToInt32($img.Height/$scale)
    $i++

    if ($i % 10 -eq 0)
    {
        Write-Host "$($img.Height)"
        #Write-Host $file
        #& opencv_createsamples.exe -vec $scriptpath$file.vec -img $file -num 1 -bgcolor 0 -bgthresh 0 -maxxangle 0 -maxyangle 0 -maxzangle 0 -maxidev 40 -w 20 -h 40 -bg ..\negative\negatives.txt
        Write-Host " opencv_createsamples.exe -vec $scriptpath$file.vec -img $file -num 1 -bgcolor 255 -bgthresh 0 -maxxangle 0 -maxyangle 0 -maxzangle 0 -maxidev 40 -w 40 -h 80 -bg ..\negative\negatives.txt"
    }
}

# clean our vecs up
Remove-Item $scriptpath\*.vec -include .vec

# create master vec
& dir /b *.vec >vectors.vec
