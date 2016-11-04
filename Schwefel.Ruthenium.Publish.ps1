[CmdletBinding()]
Param(
    $configuration="Debug"
    ,
    $revision=""
)


Write-Host "Start Executing Publish"
Set-Location $PSScriptRoot


$packOutput = Resolve-Path ".\Pack\$configuration"
$packVersionSuffix = ""

if($configuration -ne "Release") {
    $packVersionSuffix = "alpha$revision"
}
else {
    $packVersionSuffix = "beta$revision"
}

Write-Host "Build Version suffix: $packVersionSuffix"

# Publish Solution
Get-ChildItem $packOutput -File -Recurse -Filter *.nupkg | 
Foreach-Object {
    Write-Host ("Publishing " + $_.Name)

}

Write-Host "Finished Executing Publish"