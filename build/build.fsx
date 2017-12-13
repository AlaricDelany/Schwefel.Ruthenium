#I "./../packages/fake_build/FAKE.Core/tools/"
#r "./../packages/fake_build/FAKE.Core/tools/FakeLib.dll"

open Fake
open System
open System.IO

let currentTarget = getBuildParamOrDefault "Target" "Pack"
let buildDir = __SOURCE_DIRECTORY__
trace ("Build Dir: " + buildDir)

let solutionDir =  Path.GetFullPath buildDir @@ ".."
let versionFile = buildDir @@ "version.txt"
let publishDir = solutionDir @@ "Publish"
let version = File.ReadAllText versionFile
let buildNumber = getBuildParamOrDefault "BuildNumber" "0"
let apiKey = getBuildParamOrDefault "NugetKey" "NONE"
let fullVersion = String.Format("{0}.{1}", version, buildNumber)
trace ("Full Version Number: " + fullVersion)

trace ("Solution Dir: " + solutionDir)

Target "Clean" (fun _ -> 
    DotNetCli.RunCommand(fun p -> {p with WorkingDir=solutionDir}) "clean"

    CleanDirs !! (solutionDir @@ "**/bin/*/")
    CleanDirs !! (solutionDir @@ "**/obj/*/")
)

Target "Restore" (fun _ ->
    DotNetCli.Restore(fun p -> 
    { p with 
        WorkingDir=solutionDir
        NoCache=true
    })
)

Target "Build" (fun _ -> 
    !!(solutionDir @@ "/Schwefel.Ruthenium.*/*.csproj")
    |> Seq.iter (fun currentProject ->
        trace currentProject
        
        let projectDir = DirectoryName currentProject

        DotNetCli.Build(fun p -> 
            { p with
                Configuration = "Release"
                Project=currentProject
                WorkingDir=projectDir
                AdditionalArgs=[String.Format("/p:Version={0}", fullVersion)]
            })
    )
)

Target "Pack" (fun _ ->
    ensureDirectory publishDir

    !!(solutionDir @@ "/Schwefel.Ruthenium.*/*.csproj")
    |> Seq.iter (fun currentProject ->
        let projectDir = DirectoryName currentProject
        let projectName = fileNameWithoutExt currentProject

        trace projectDir

        DotNetCli.Pack(fun p -> 
            { p with
                Configuration = "Release"
                WorkingDir=projectDir
                OutputPath=publishDir
                AdditionalArgs=[String.Format("/p:PackageVersion={0}", fullVersion); "--no-build"; "--no-restore"]
            })
    )
)

Target "PushNuget" (fun _ ->
    let nugetFeedUrl = "https://api.nuget.org/v3/index.json"
    let publishBaseDir = getBuildParamOrDefault "PublishBaseDir" publishDir

    Paket.Push(fun p ->
        { p with
            ApiKey = apiKey
            WorkingDir = publishBaseDir
            EndPoint=nugetFeedUrl
        })
)

"Clean"
  ==> "Build"
"Restore"
  ==> "PushNuget"
  ==> "Build"
"Build"
  ==> "Pack"

Run currentTarget