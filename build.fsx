// include Fake libs
#r "./packages/FAKE/tools/FakeLib.dll"
#r "System.Management.Automation"

open Fake
open System.Management.Automation

// Directories
let buildDir  = "./build/"
let deployDir = "./deploy/"
let dockerDir = "./docker/"
let dockerBuildDir = "./dockerBuild/"


// Filesets
let appReferences  =
    !! "/**/*.csproj"
      ++ "/**/*.fsproj"

// version info
let version = "0.1"  // or retrieve from CI server

// Targets
Target "Clean" (fun _ ->      
    CleanDirs [buildDir; deployDir; dockerBuildDir]
    
    let clean = if isUnix then "docker/clean.sh" else "docker/clean.cmd"
    Shell.Exec (clean) |> ignore
)

Target "Build" (fun _ ->
    // compile all projects below src/app/
    MSBuildDebug buildDir "Build" appReferences
        |> Log "AppBuild-Output: "
)

Target "Docker" (fun _ ->
    !! "/docker/**/*.*" |> CopyTo dockerBuildDir
    !! "/build/**/*.exe" |> CopyTo dockerBuildDir
    
    let build = if isUnix then "./dockerBuild/build.sh" else "build.cmd"
    Shell.Exec (build, null ,dockerBuildDir) |> ignore
            
    ["Docker Completed"] |> Log "AppBuild-Output:"
)

Target "DockerRun" (fun _ ->
    let run = if isUnix then "docker/run.sh" else "docker/run.cmd"
    Shell.Exec run |> ignore
        
    ["Docker Run"] |> Log "AppBuild-Output:"
)

Target "Deploy" (fun _ ->
    !! (buildDir + "/**/*.*")
        -- "*.zip"
        |> Zip buildDir (deployDir + "ApplicationName." + version + ".zip")
)

// Build order
"Clean"
  ==> "Build"
  ==> "Docker"
  ==> "DockerRun"
  ==> "Deploy"
  

// start build
RunTargetOrDefault "Build"
