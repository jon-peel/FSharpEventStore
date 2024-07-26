namespace StudentTests

open System
open StudentTests.Events

type IModel = abstract member id: Guid

type Student = { id: Guid; name: string; email: string; birth: DateTime; courses: string list }
               interface IModel with member this.id = this.id

module Student =
    let append student event =
        match student, (event: StudentEvent) with
        | None, (:? StudentCreated as created) -> Some { id = created.id; name = created.name; email = created.email; birth = created.birth; courses = [] }
        | Some student, (:? StudentUpdated as updated) -> Some { student with name = updated.name; email = updated.email }
        | Some student, (:? StudentEnrolled as enrolled) -> Some { student with courses = enrolled.course :: student.courses }
        | _ -> None
        
    let construct events = events |> Seq.fold append None