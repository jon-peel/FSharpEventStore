module StudentTests.Api.DataStore.Projections

open System
open Microsoft.EntityFrameworkCore.ChangeTracking.Internal

type Cmd<'A> = | Set of id: Guid * item: 'A | Get of id: Guid * chan: AsyncReplyChannel<'A option>

let rec private loop<'A> (state: Map<Guid, 'A>) (inbox: MailboxProcessor<Cmd<'A>>) = async {
    let! msg = inbox.Receive ()
    let result =
        match msg: Cmd<'A> with
        | Set (id, value) ->
            loop (Map.add id value state) inbox
        | Get (id, reply) ->                    
            state |> Map.tryFind id |> reply.Reply                    
            loop state inbox
    return! result
}   
let create<'A> () = MailboxProcessor.Start (loop<'A> Map.empty)
    

