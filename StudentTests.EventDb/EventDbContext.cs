using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentTests.Events;

namespace StudentTests.EventDb;

public class EventDbContext(DbContextOptions<EventDbContext> options) : DbContext(options)
{
    public DbSet<StudentEvent> StudentEvents { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var student = modelBuilder.Entity<StudentEvent>().ToTable("StudentEvents");
        student.HasDiscriminator("EventType", typeof(string));
        student.Property(x => x.id).IsRequired();
        student.Property(x => x.time).IsRequired();
        student.HasKey(x => new { x.id, x.time });
        
        var studentCreated = modelBuilder.Entity<StudentCreated>().HasBaseType<StudentEvent>();
        studentCreated.Property(x => x.name).HasColumnName("name").IsRequired().HasMaxLength(50).IsUnicode();
        studentCreated.Property(x => x.email).HasColumnName("email").IsRequired().HasMaxLength(100).IsUnicode(false);
        studentCreated.Property(x => x.birth).HasColumnName("birth").IsRequired();
        
        var studentUpdated = modelBuilder.Entity<StudentUpdated>().HasBaseType<StudentEvent>();
        studentUpdated.Property(x => x.name).HasColumnName("name").IsRequired().HasMaxLength(50).IsUnicode();
        studentUpdated.Property(x => x.email).HasColumnName("email").IsRequired().HasMaxLength(100).IsUnicode(false);
        
        var studentEnrolled = modelBuilder.Entity<StudentEnrolled>().HasBaseType<StudentEvent>();
        studentEnrolled.Property(x => x.course).HasColumnName("course").IsRequired().HasMaxLength(150).IsUnicode();
    }
}

public class EventDbContextFactory : IDesignTimeDbContextFactory<EventDbContext>
{
    const string ConnectionString = "Data Source=test.db";
    
    
    public EventDbContext CreateDbContext(string[] args) => new (Options());

    static DbContextOptions<EventDbContext> Options() =>
        new DbContextOptionsBuilder<EventDbContext>().UseSqlite(ConnectionString).Options;
}