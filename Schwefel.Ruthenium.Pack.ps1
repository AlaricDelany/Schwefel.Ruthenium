[CmdletBinding()]
Param(
    $configuration="Debug"
    ,
    $revision=""
)

Write-Host "Start Executing Pack"
Set-Location $PSScriptRoot

function PackProject($filter) {
    Write-Host "Start Pack for $filter Projects"

    Get-ChildItem ".\Schwefel.Ruthenium" -File -Recurse -Filter $filter | 
    Foreach-Object {
        Write-Host ("Packing " + $_.DirectoryName)
    
        $packOutputCurrent = (Join-Path $packOutput -ChildPath $_.Directory.Name)
    
        Write-Host "Build Output is set to: $packOutputCurrent"

        dotnet pack $_.FullName --configuration $configuration --version-suffix $packVersionSuffix --output "$packOutputCurrent"

    }
    Write-Host "Finished Pack for $filter Projects"
}

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
PackProject "project.json"
PackProject "*.csproj"


Write-Host "Finished Executing Pack"