namespace StudentTests.Api.DataStore

open System
open StudentTests
open StudentTests.EventDb
open StudentTests.Events

type IStudentDb =
    abstract member Append: StudentEvent -> Unit
    abstract member Get: Guid -> Student option
    
module StudentDb =
    let projections = Projections.create<Student> ()
    
    let create (db: EventDbContext) =
        let get id =
            let student = projections.PostAndReply (fun chan -> Projections.Get (id, chan))
            match student with
            | Some student' -> Some student' 
            | None -> db.StudentEvents
                      |> Seq.filter (fun e -> e.id = id)
                      |> Student.construct
        
        let append (event: StudentEvent) =
            let student = get event.id
            let student = Student.append student event
            match student with
            | Some _ ->
                db.StudentEvents.Add event |> ignore
                db.SaveChanges () |> ignore
            | None -> ()
            
        { new IStudentDb with
            override _.Append event = append event
            override _.Get id = get id }
