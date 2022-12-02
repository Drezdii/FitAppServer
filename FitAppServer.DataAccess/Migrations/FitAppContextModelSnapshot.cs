﻿// <auto-generated />
using System;
using FitAppServer.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FitAppServer.DataAccess.Migrations
{
    [DbContext(typeof(FitAppContext))]
    partial class FitAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FitAppServer.DataAccess.Entities.Challenge", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("DescriptionTranslationKey")
                        .HasColumnType("text");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<float>("Goal")
                        .HasColumnType("real");

                    b.Property<string>("NameTranslationKey")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Unit")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Challenges");
                });

            modelBuilder.Entity("FitAppServer.DataAccess.Entities.ChallengeEntry", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("ChallengeId")
                        .HasColumnType("text");

                    b.Property<DateOnly?>("CompletedAt")
                        .HasColumnType("date");

                    b.Property<float>("Value")
                        .HasColumnType("real");

                    b.HasKey("UserId", "ChallengeId");

                    b.HasIndex("ChallengeId");

                    b.ToTable("ChallengeEntries");
                });

            modelBuilder.Entity("FitAppServer.DataAccess.Entities.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ExerciseInfoId")
                        .HasColumnType("integer");

                    b.Property<int>("WorkoutId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseInfoId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("FitAppServer.DataAccess.Entities.ExerciseInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ExerciseInfo");
                });

            modelBuilder.Entity("FitAppServer.DataAccess.Entities.OneRepMax", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ExerciseInfoId")
                        .HasColumnType("integer");

                    b.Property<int>("SetId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<float>("Value")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseInfoId");

                    b.HasIndex("SetId");

                    b.HasIndex("UserId");

                    b.ToTable("OneRepMaxes");
                });

            modelBuilder.Entity("FitAppServer.DataAccess.Entities.Set", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Completed")
                        .HasColumnType("boolean");

                    b.Property<int>("ExerciseId")
                        .HasColumnType("integer");

                    b.Property<int>("Reps")
                        .HasColumnType("integer");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.ToTable("Sets");
                });

            modelBuilder.Entity("FitAppServer.DataAccess.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Uuid")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FitAppServer.DataAccess.Entities.Workout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateOnly?>("Date")
                        .HasColumnType("date");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int?>("WorkoutProgramDetailsId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("WorkoutProgramDetailsId");

                    b.ToTable("Workouts");
                });

            modelBuilder.Entity("FitAppServer.DataAccess.Entities.WorkoutProgram", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Programs");
                });

            modelBuilder.Entity("FitAppServer.DataAccess.Entities.WorkoutProgramDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Cycle")
                        .HasColumnType("integer");

                    b.Property<int>("ProgramId")
                        .HasColumnType("integer");

                    b.Property<int>("Week")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProgramId");

                    b.ToTable("WorkoutProgramDetails");
                });

            modelBuilder.Entity("FitAppServer.DataAccess.Entities.ChallengeEntry", b =>
                {
                    b.HasOne("FitAppServer.DataAccess.Entities.Challenge", "Challenge")
                        .WithMany("ChallengeEntries")
                        .HasForeignKey("ChallengeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FitAppServer.DataAccess.Entities.User", "User")
                        .WithMany("ChallengeEntries")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Challenge");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FitAppServer.DataAccess.Entities.Exercise", b =>
                {
                    b.HasOne("FitAppServer.DataAccess.Entities.ExerciseInfo", "ExerciseInfo")
                        .WithMany("Exercises")
                        .HasForeignKey("ExerciseInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FitAppServer.DataAccess.Entities.Workout", "Workout")
                        .WithMany("Exercises")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExerciseInfo");

                    b.Navigation("Workout");
                });

            modelBuilder.Entity("FitAppServer.DataAccess.Entities.OneRepMax", b =>
                {
                    b.HasOne("FitAppServer.DataAccess.Entities.ExerciseInfo", "ExerciseInfo")
                        .WithMany()
                        .HasForeignKey("ExerciseInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FitAppServer.DataAccess.Entities.Set", "Set")
                        .WithMany()
                        .HasForeignKey("SetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FitAppServer.DataAccess.Entities.User", "User")
                        .WithMany("Maxes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExerciseInfo");

                    b.Navigation("Set");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FitAppServer.DataAccess.Entities.Set", b =>
                {
                    b.HasOne("FitAppServer.DataAccess.Entities.Exercise", "Exercise")
                        .WithMany("Sets")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exercise");
                });

            modelBuilder.Entity("FitAppServer.DataAccess.Entities.Workout", b =>
                {
                    b.HasOne("FitAppServer.DataAccess.Entities.User", "User")
                        .WithMany("Workouts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FitAppServer.DataAccess.Entities.WorkoutProgramDetail", "WorkoutProgramDetails")
                        .WithMany("Workouts")
                        .HasForeignKey("WorkoutProgramDetailsId");

                    b.Navigation("User");

                    b.Navigation("WorkoutProgramDetails");
                });

            modelBuilder.Entity("FitAppServer.DataAccess.Entities.WorkoutProgramDetail", b =>
                {
                    b.HasOne("FitAppServer.DataAccess.Entities.WorkoutProgram", "Program")
                        .WithMany("ProgramDetails")
                        .HasForeignKey("ProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Program");
                });

            modelBuilder.Entity("FitAppServer.DataAccess.Entities.Challenge", b =>
                {
                    b.Navigation("ChallengeEntries");
                });

            modelBuilder.Entity("FitAppServer.DataAccess.Entities.Exercise", b =>
                {
                    b.Navigation("Sets");
                });

            modelBuilder.Entity("FitAppServer.DataAccess.Entities.ExerciseInfo", b =>
                {
                    b.Navigation("Exercises");
                });

            modelBuilder.Entity("FitAppServer.DataAccess.Entities.User", b =>
                {
                    b.Navigation("ChallengeEntries");

                    b.Navigation("Maxes");

                    b.Navigation("Workouts");
                });

            modelBuilder.Entity("FitAppServer.DataAccess.Entities.Workout", b =>
                {
                    b.Navigation("Exercises");
                });

            modelBuilder.Entity("FitAppServer.DataAccess.Entities.WorkoutProgram", b =>
                {
                    b.Navigation("ProgramDetails");
                });

            modelBuilder.Entity("FitAppServer.DataAccess.Entities.WorkoutProgramDetail", b =>
                {
                    b.Navigation("Workouts");
                });
#pragma warning restore 612, 618
        }
    }
}
