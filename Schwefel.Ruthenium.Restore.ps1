Write-Host "Start Executing Restore"
Set-Location "$PSScriptRoot"


# See: https://docs.microsoft.com/en-us/dotnet/articles/core/tools/dotnet-restore
dotnet restore



Write-Host "Finished Executing Restore"