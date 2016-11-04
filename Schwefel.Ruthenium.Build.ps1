[CmdletBinding()]
Param(
    $configuration="Debug"
    ,
    $revision=""
)

Write-Host "Start Build"
Set-Location $PSScriptRoot


$buildOutput = ".\Build\$configuration"
$buildVersionSuffix = ""

if($configuration -ne "Release") {
    $buildVersionSuffix = "alpha$revision"
}
else {
    $buildVersionSuffix = "beta$revision"
}

Write-Host "Build Version suffix: $buildVersionSuffix"

# CleanUp Environment
if(Test-Path $buildOutput) {
    Remove-Item $buildOutput -Force -Recurse
}
New-Item -ItemType directory -Path $buildOutput

$buildOutput = Resolve-Path $buildOutput

# Restore Nuget Packages
& ".\Schwefel.Ruthenium.Restore.ps1"

# Build Solution
Get-ChildItem ".\Schwefel.Ruthenium" -File -Recurse -Filter project.json | 
Foreach-Object {
    Write-Host ("Building " + $_.DirectoryName)
    
    $buildOutputCurrent = (Join-Path $buildOutput -ChildPath $_.Directory.Name)

    
    Write-Host "Build Output is set to: $buildOutputCurrent"

    dotnet build $_.FullName --configuration $configuration --version-suffix $buildVersionSuffix --framework netstandard1.6 --output "$buildOutputCurrent" --no-incremental
}
Write-Host "Finished Executing Build"