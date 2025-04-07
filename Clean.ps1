Write-Host "Cleaning up..."

$foldersToRemove =
     "bin",
     "obj",
     "TestResults",
     "ReSharper."

$filesToRemove = 
    "Thumbs.db",
    ".suo",
    ".user",
    ".cache",
    ".scc",
    ".vssscc",
    "*.vspscc"

#Remove Folders
Get-ChildItem .\ -include $foldersToRemove -force -recurse |
    where { $.PsIsContainer } |
    foreach ($) {
        Write-Host "  Removing folder ./$($.Name)"
        Write-Host "  Removing folder ./$($.Name)"
        Remove-Item $.FullName -force -recurse
    }

#Remove Files
Get-ChildItem .\ -include $filesToRemove -force -recurse |
    foreach ($) {
        Write-Host "  Removing file ./$($.Name)"
        Remove-Item $_.FullName -force -recurse
    }

Write-Host "Done. Press any key to close..."
[void][System.Console]::ReadKey($true)