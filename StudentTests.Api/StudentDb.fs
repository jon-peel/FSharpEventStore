namespace StudentTests.Api

open System
open StudentTests
open StudentTests.EventDb
open StudentTests.Events

type IStudentDb =
    abstract member Append: StudentEvent -> Unit
    abstract member Get: Guid -> Student option
    
module StudentDb =
    let create (db: EventDbContext) =
        let append event =
            db.StudentEvents.Add event |> ignore
            db.SaveChanges () |> ignore
            
        let get id =
            db.StudentEvents
            |> Seq.filter (fun e -> e.id = id)
            |> Student.construct
        
        { new IStudentDb with
            override _.Append event = append event
            override _.Get id = get id }
