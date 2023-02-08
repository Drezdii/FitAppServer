using System.Collections.Generic;
using FitAppServer.DataAccess;
using FitAppServer.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Services.Tests;

public class TestDatabaseFixture
{
    private const string ConnectionString =
        @"Server=localhost;Port=5432;Database=tests;User Id=postgres;Password=root;";

    private static readonly object Lock = new();
    private static bool _databaseInitialized;

    public TestDatabaseFixture()
    {
        lock (Lock)
        {
            if (_databaseInitialized)
            {
                return;
            }

            using (var context = CreateContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.AddRange(
                    new ExerciseInfo {Id = 1, Name = "Deadlift"},
                    new ExerciseInfo {Id = 2, Name = "Bench"},
                    new ExerciseInfo {Id = 3, Name = "Squat"},
                    new ExerciseInfo {Id = 4, Name = "Ohp"},
                    new ExerciseInfo {Id = 5, Name = "Pullups"}
                );

                context.Add(new User
                {
                    Id = Constants.USER_ID, Email = Constants.USER_EMAIL, Username = Constants.USER_USERNAME,
                    Uuid = Constants.USER_UUID
                });

                var exercises = new List<Exercise>
                {
                    new()
                    {
                        ExerciseInfoId = 1,
                        Sets = new List<Set>
                        {
                            new()
                            {
                                Reps = 5,
                                Weight = 200,
                                Completed = false
                            }
                        }
                    },
                    new()
                    {
                        ExerciseInfoId = 2,
                        Sets = new List<Set>()
                        {
                            new()
                            {
                                Reps = 10,
                                Weight = 100,
                                Completed = false
                            }
                        }
                    }
                };

                context.Add(new Workout
                {
                    Id = Constants.WORKOUT_ID,
                    Date = null,
                    StartDate = null,
                    EndDate = null,
                    UserId = Constants.USER_ID,
                    Type = WorkoutTypeCode.None,
                    Exercises = exercises
                });


                context.SaveChanges();
            }

            _databaseInitialized = true;
        }
    }

    public FitAppContext CreateContext() =>
        new(new DbContextOptionsBuilder<FitAppContext>().UseNpgsql(ConnectionString).Options);
}