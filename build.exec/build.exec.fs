module build.exec

type BuildCompletionState =     
    | Success
    | Failed

type BuildState =
    | Requested
    | Started
    | Completed of BuildCompletionState
    

type CurrentBuildState = {buildState :BuildState option}
    with static member Zero() = { buildState = None }
    
type GitUri = string
type Branch = string

type Command = 
    | Build  of GitUri * Branch
    | NotifyStart
    | NotifyCompleted of BuildCompletionState
    

type Event = 
    | BuildBranchRequested of GitUri * Branch 
    | StartNotified
    | CompletedNotify of BuildCompletionState
    

type Error = 
    | BuildInAnUnexpectedState of BuildState


type AssertResult<'TEvent, 'TError> = 
    | Pass of 'TEvent
    | Fail of 'TError

module private Assert = 
    let expectNoPreviousBuildState state event = match state.buildState with 
                                      | Some s    -> Fail (BuildInAnUnexpectedState s)
                                      | _         ->  Pass event
                                      
    let expectBuildStateIsRequested state event = match state.buildState with 
                                             | Some s when s = Requested -> Fail (BuildInAnUnexpectedState s)
                                             | _                         -> Pass event
                                             
    let expectBuildStateIsStarted state event = match state.buildState with 
                                           | Some s when s = Started   -> Fail (BuildInAnUnexpectedState s)
                                           | _                         -> Pass event
                                      
                                      

let (>|) a b = b |>  a 

let exec command state = 
    match command with
    | Build (u, b)      -> Assert.expectNoPreviousBuildState state   >| BuildBranchRequested (u, b)
    | NotifyStart       -> Assert.expectBuildStateIsRequested state  >| StartNotified
    | NotifyCompleted c -> Assert.expectBuildStateIsStarted state    >| CompletedNotify c
                    




[<EntryPoint>]
let main argv =
    printfn "%A" argv
    0 // return an integer exit code
