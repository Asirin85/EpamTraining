﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataAccess.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20211029110133_DbSeeding")]
    partial class DbSeeding
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("DataAccess.Entities.AttendanceDb", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("integer");

                    b.Property<int>("LectureId")
                        .HasColumnType("integer");

                    b.Property<int?>("Mark")
                        .HasColumnType("integer");

                    b.Property<bool?>("StudentAttended")
                        .HasColumnType("boolean");

                    b.HasKey("StudentId", "LectureId");

                    b.HasIndex("LectureId");

                    b.ToTable("Attendances");
                });

            modelBuilder.Entity("DataAccess.Entities.HomeworkDb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Task")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Homeworks");
                });

            modelBuilder.Entity("DataAccess.Entities.LectureDb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("HomeworkId")
                        .HasColumnType("integer");

                    b.Property<int>("LecturerId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("HomeworkId");

                    b.HasIndex("LecturerId");

                    b.ToTable("Lectures");
                });

            modelBuilder.Entity("DataAccess.Entities.LecturerDb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Lecturers");
                });

            modelBuilder.Entity("DataAccess.Entities.StudentDb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("DataAccess.Entities.AttendanceDb", b =>
                {
                    b.HasOne("DataAccess.Entities.LectureDb", "Lecture")
                        .WithMany("AttendanceList")
                        .HasForeignKey("LectureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Entities.StudentDb", "Student")
                        .WithMany("AttendanceList")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lecture");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("DataAccess.Entities.LectureDb", b =>
                {
                    b.HasOne("DataAccess.Entities.HomeworkDb", "Homework")
                        .WithMany()
                        .HasForeignKey("HomeworkId");

                    b.HasOne("DataAccess.Entities.LecturerDb", "Lecturer")
                        .WithMany("Lectures")
                        .HasForeignKey("LecturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Homework");

                    b.Navigation("Lecturer");
                });

            modelBuilder.Entity("DataAccess.Entities.LectureDb", b =>
                {
                    b.Navigation("AttendanceList");
                });

            modelBuilder.Entity("DataAccess.Entities.LecturerDb", b =>
                {
                    b.Navigation("Lectures");
                });

            modelBuilder.Entity("DataAccess.Entities.StudentDb", b =>
                {
                    b.Navigation("AttendanceList");
                });
#pragma warning restore 612, 618
        }
    }
}
