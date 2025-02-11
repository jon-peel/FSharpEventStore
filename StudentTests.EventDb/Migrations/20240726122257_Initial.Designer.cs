﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentTests.EventDb;

#nullable disable

namespace StudentTests.EventDb.Migrations
{
    [DbContext(typeof(EventDbContext))]
    [Migration("20240726122257_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("StudentTests.Events.StudentEvent", b =>
                {
                    b.Property<Guid>("id")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("time")
                        .HasColumnType("TEXT");

                    b.Property<string>("EventType")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("TEXT");

                    b.HasKey("id", "time");

                    b.ToTable("StudentEvents", (string)null);

                    b.HasDiscriminator<string>("EventType").HasValue("StudentEvent");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("StudentTests.Events.StudentCreated", b =>
                {
                    b.HasBaseType("StudentTests.Events.StudentEvent");

                    b.Property<DateTime>("birth")
                        .HasColumnType("TEXT")
                        .HasColumnName("birth");

                    b.Property<string>("email")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("TEXT")
                        .HasColumnName("email");

                    b.Property<string>("name")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.HasDiscriminator().HasValue("StudentCreated");
                });

            modelBuilder.Entity("StudentTests.Events.StudentEnrolled", b =>
                {
                    b.HasBaseType("StudentTests.Events.StudentEvent");

                    b.Property<string>("course")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(true)
                        .HasColumnType("TEXT")
                        .HasColumnName("course");

                    b.HasDiscriminator().HasValue("StudentEnrolled");
                });

            modelBuilder.Entity("StudentTests.Events.StudentUpdated", b =>
                {
                    b.HasBaseType("StudentTests.Events.StudentEvent");

                    b.Property<string>("email")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("TEXT")
                        .HasColumnName("email");

                    b.Property<string>("name")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.HasDiscriminator().HasValue("StudentUpdated");
                });
#pragma warning restore 612, 618
        }
    }
}
