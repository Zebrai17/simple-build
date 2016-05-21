module build.exec



let load = Seq.empty<BuildProcess.Event>
let state = load |> Seq.fold BuildProcess.apply (BuildProcess.State.Zero())


[<EntryPoint>]
let main argv =
    printfn "%A" argv
    0 // return an integer exit code
