open System
open Microsoft.EntityFrameworkCore
open StudentTests.Events

let studentDb =
    let db = StudentTests.EventDb.EventDbContextFactory().CreateDbContext([||])
    db.Database.Migrate ()
    StudentTests.Api.DataStore.StudentDb.create db

let gid = Guid.Parse("18bc2a0f-99b2-436b-b485-ea6098aff93c")

// let s1 = StudentCreated (gid, DateTime.Now, "Alice", "alice@shop.com", DateTime.Parse("1987-12-12"))
// let s2 = StudentUpdated (gid, DateTime.Now, "Alici", "alici@shop.com")
// let s3 = StudentEnrolled (gid, DateTime.Now, "Math")
// let s4 = StudentEnrolled (gid, DateTime.Now, "Science")
// studentDb.Append s1
// studentDb.Append s2
// studentDb.Append s3
// studentDb.Append s4


let student = studentDb.Get gid
Console.WriteLine student