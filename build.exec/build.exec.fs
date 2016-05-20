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
    | BuildAlreadyRequested

type AssertResult<'TEvent, 'TError> = 
    | Pass of 'TEvent
    | Fail of 'TError

module private Assert = 
    let noPreviousState state event = match state.buildState with 
                                      | Some s    -> Fail BuildAlreadyRequested
                                      | _         ->  Pass event
                                      
    let previousStateRequested state event = match state.buildState with 
                                             | Some s when s = Requested -> Fail BuildAlreadyRequested
                                             | _                         -> Pass event
                                             
    let previousStateStarted state event = match state.buildState with 
                                           | Some s when s = Started   -> Fail BuildAlreadyRequested
                                           | _                         -> Pass event
                                      
                                      

let (>|) a b = b |>  a 

let exec command state = 
    match command with
    | Build (u, b)      -> Assert.noPreviousState state         >| BuildBranchRequested (u, b)
    | NotifyStart       -> Assert.previousStateRequested state  >| StartNotified
    | NotifyCompleted c -> Assert.previousStateStarted state    >| CompletedNotify c
                    




[<EntryPoint>]
let main argv =
    printfn "%A" argv
    0 // return an integer exit code
