﻿// <auto-generated />
using System;
using FitAppServer.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FitAppServer.DataAccess.Migrations
{
    [DbContext(typeof(FitAppContext))]
    [Migration("20210716152048_workout-date-changes")]
    partial class workoutdatechanges
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("FitAppServer.DataAccess.Entites.Exercise", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ExerciseInfoID")
                        .HasColumnType("integer");

                    b.Property<int>("WorkoutID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("ExerciseInfoID");

                    b.HasIndex("WorkoutID");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("FitAppServer.DataAccess.Entites.ExerciseInfo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("ExerciseInfo");
                });

            modelBuilder.Entity("FitAppServer.DataAccess.Entites.Set", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Completed")
                        .HasColumnType("boolean");

                    b.Property<int>("ExerciseID")
                        .HasColumnType("integer");

                    b.Property<int>("Reps")
                        .HasColumnType("integer");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.HasKey("ID");

                    b.HasIndex("ExerciseID");

                    b.ToTable("Sets");
                });

            modelBuilder.Entity("FitAppServer.DataAccess.Entites.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Uuid")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FitAppServer.DataAccess.Entites.Workout", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<int>("UserID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Workouts");
                });

            modelBuilder.Entity("FitAppServer.DataAccess.Entites.Exercise", b =>
                {
                    b.HasOne("FitAppServer.DataAccess.Entites.ExerciseInfo", "ExerciseInfo")
                        .WithMany()
                        .HasForeignKey("ExerciseInfoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FitAppServer.DataAccess.Entites.Workout", "Workout")
                        .WithMany("Exercises")
                        .HasForeignKey("WorkoutID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExerciseInfo");

                    b.Navigation("Workout");
                });

            modelBuilder.Entity("FitAppServer.DataAccess.Entites.Set", b =>
                {
                    b.HasOne("FitAppServer.DataAccess.Entites.Exercise", "Exercise")
                        .WithMany("Sets")
                        .HasForeignKey("ExerciseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exercise");
                });

            modelBuilder.Entity("FitAppServer.DataAccess.Entites.Workout", b =>
                {
                    b.HasOne("FitAppServer.DataAccess.Entites.User", "User")
                        .WithMany("Workouts")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FitAppServer.DataAccess.Entites.Exercise", b =>
                {
                    b.Navigation("Sets");
                });

            modelBuilder.Entity("FitAppServer.DataAccess.Entites.User", b =>
                {
                    b.Navigation("Workouts");
                });

            modelBuilder.Entity("FitAppServer.DataAccess.Entites.Workout", b =>
                {
                    b.Navigation("Exercises");
                });
#pragma warning restore 612, 618
        }
    }
}
