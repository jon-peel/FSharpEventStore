namespace StudentTests.Events

open System

type IEvent =
    abstract member id: Guid
    abstract member time: DateTime
    
type StudentEvent (id, time) =
     member _.id: Guid = id
     member _.time: DateTime = time
     
     interface IEvent with
         member _.id: Guid = id
         member _.time: DateTime = time
    
type StudentCreated (id, time, name, email, birth) =
    inherit StudentEvent (id, time)
    member _.name: string = name
    member _.email: string = email
    member _.birth: DateTime = birth
    
type StudentUpdated (id, time, name, email) =
    inherit StudentEvent (id, time)
    member _.name: string = name
    member _.email: string = email
    
type StudentEnrolled (id, time, course) =
    inherit StudentEvent (id, time)
    member _.course: string = course
