[CmdletBinding()]
Param(
    $configuration="Debug"
    ,
    $revision=""
)

Write-Host "Start Executing Pack"
Set-Location $PSScriptRoot


$packOutput = ".\Pack\$configuration"
$packVersionSuffix = ""

if($configuration -ne "Release") {
    $packVersionSuffix = "alpha$revision"
}
else {
    $packVersionSuffix = "beta$revision"
}
# CleanUp Environment
if(Test-Path $packOutput) {
    Remove-Item $packOutput -Force -Recurse
}
New-Item -ItemType directory -Path $packOutput

$packOutput = Resolve-Path $packOutput


Write-Host "Build Version suffix: $packVersionSuffix"

# Pack Projects
Get-ChildItem ".\Schwefel.Ruthenium" -File -Recurse -Filter project.json | 
Foreach-Object {
    Write-Host ("Packing " + $_.DirectoryName)
    
    $packOutputCurrent = (Join-Path $packOutput -ChildPath $_.Directory.Name)
    
    Write-Host "Build Output is set to: $packOutputCurrent"

    dotnet pack $_.FullName --configuration $configuration --version-suffix $packVersionSuffix --output "$packOutputCurrent" --no-build 

}
Write-Host "Finished Executing Pack"