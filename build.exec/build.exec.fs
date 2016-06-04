module build.exec



let load = Seq.empty<BuildProcess.Event>
let state = load |> Seq.fold BuildProcess.apply (BuildProcess.State.Zero())


let printUsageInstructions = 
    sprintf "%s" "usage {0} <command>", System.AppDomain.CurrentDomain.FriendlyName



[<EntryPoint>]
let main argv =
    let commandResult = CommandFactory.create argv
    
    match commandResult with
    | CommandFactory.Error e when e = CommandFactory.CommandNotProvided -> printUsageInstructions |> ignore
    | _ -> sprintf "%s" "Error" |> ignore
    
    
    0 // return an integer exit code
