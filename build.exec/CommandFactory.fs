[<RequireQualifiedAccess>]
module CommandFactory

open System;

type CommandCreationError = 
    | CommandNotDefined of string
    | CommandNotProvided
    

type Result = 
     | Command of BuildProcess.Command
     | Error of CommandCreationError


let private gitUri = Environment.GetEnvironmentVariable("GIT_URI")
let private branch = Environment.GetEnvironmentVariable("BRANCH") 
let private buildScript = Environment.GetEnvironmentVariable("BUILD_SCRIPT")
let private targets = Environment.GetEnvironmentVariable("TARGETS").Split [|','|]


let createBuildCommand =
    BuildProcess.Build (gitUri, branch, buildScript, targets)

let create argv = 
    match argv with 
    | a when a |> Seq.isEmpty -> Error CommandNotProvided
    | a when a |> Seq.head = "Build" -> Command (createBuildCommand)
    | _ -> Error (CommandNotDefined (argv |> Seq.head))
    