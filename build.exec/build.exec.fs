module build.exec

open System
open Newtonsoft.Json

let load = Seq.empty<BuildProcess.Event>
let state = load |> Seq.fold BuildProcess.apply (BuildProcess.State.Zero())


let printUsageInstructions = 
    printf "usage %s <command>" System.AppDomain.CurrentDomain.FriendlyName
    printf "%s" Environment.NewLine

[<EntryPoint>]
let main argv =
    let commandResult = CommandFactory.create argv
    
    match commandResult with
    | CommandFactory.Error e when e = CommandFactory.CommandNotProvided -> printUsageInstructions
    | _ -> printf "%s" "Error"
    
    0 // return an integer exit code
