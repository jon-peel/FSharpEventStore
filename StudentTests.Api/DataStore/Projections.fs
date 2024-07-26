module StudentTests.Api.DataStore.Projections

open System
open StudentTests

type Cmd<'A when 'A :> IModel> = | Set of item: 'A | Get of id: Guid * chan: AsyncReplyChannel<'A option>

let rec private loop (state: Map<Guid, 'A>) (inbox: MailboxProcessor<Cmd<'A>>) = async {
    let! msg = inbox.Receive ()
    let result =
        match msg: Cmd<'A> with
        | Set value ->
            loop (Map.add value.id value state) inbox
        | Get (id, reply) ->                    
            state |> Map.tryFind id |> reply.Reply                    
            loop state inbox
    return! result
}   
let create () = MailboxProcessor.Start (loop Map.empty)
    

