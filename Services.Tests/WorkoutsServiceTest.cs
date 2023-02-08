using System;
using System.Collections.Generic;
using System.Linq;
using FitAppServer.DataAccess.Entities;
using FitAppServer.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace Services.Tests;

public class WorkoutsServiceTest : IClassFixture<TestDatabaseFixture>
{
    private TestDatabaseFixture Fixture { get; }

    public WorkoutsServiceTest(TestDatabaseFixture fixture)
    {
        Fixture = fixture;
    }

    [Fact]
    public async void GetByWorkoutId()
    {
        await using var context = Fixture.CreateContext();

        var service = new WorkoutsService(context, NullLogger<WorkoutsService>.Instance);

        var workout = await service.GetByWorkoutIdAsync(Constants.WORKOUT_ID);

        Assert.NotNull(workout);
    }

    [Fact]
    public async void GetByWorkoutId_NonExistentId()
    {
        await using var context = Fixture.CreateContext();

        var service = new WorkoutsService(context, NullLogger<WorkoutsService>.Instance);

        var workout = await service.GetByWorkoutIdAsync(999);

        Assert.Null(workout);
    }

    [Fact]
    public async void GetByUserId()
    {
        await using var context = Fixture.CreateContext();

        var service = new WorkoutsService(context, NullLogger<WorkoutsService>.Instance);

        var workout = await service.GetByUserIdAsync(Constants.USER_UUID);

        Assert.Single(workout);
    }

    [Fact]
    public async void GetByUserId_NonExistentUser()
    {
        await using var context = Fixture.CreateContext();

        var service = new WorkoutsService(context, NullLogger<WorkoutsService>.Instance);

        var workout = await service.GetByUserIdAsync("non_existent");

        Assert.Empty(workout);
    }

    [Fact]
    public async void DeleteWorkout()
    {
        await using var context = Fixture.CreateContext();

        await context.Database.BeginTransactionAsync();

        var service = new WorkoutsService(context, NullLogger<WorkoutsService>.Instance);

        await service.DeleteWorkoutAsync(Constants.WORKOUT_ID);

        context.ChangeTracker.Clear();

        Assert.Null(await context.Workouts.SingleOrDefaultAsync(q => q.Id == Constants.WORKOUT_ID));
    }

    [Fact]
    public async void DeleteWorkout_NonExistentId()
    {
        await using var context = Fixture.CreateContext();

        var service = new WorkoutsService(context, NullLogger<WorkoutsService>.Instance);

        // Controller is supposed to check for the existence of the workout
        await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await service.DeleteWorkoutAsync(999));
    }

    [Fact]
    public async void AddOrUpdateWorkout_AddEmptyWorkout()
    {
        await using var context = Fixture.CreateContext();

        await context.Database.BeginTransactionAsync();

        var service = new WorkoutsService(context, NullLogger<WorkoutsService>.Instance);

        var workout = CreateWorkoutObject();

        workout.User = await context.Users.SingleAsync(q => q.Id == Constants.USER_ID);
        workout.Date = new DateOnly(2023, 2, 5);

        await service.AddOrUpdateWorkoutAsync(workout);

        context.ChangeTracker.Clear();

        Assert.NotNull(await context.Workouts.SingleOrDefaultAsync(q => q.Date == new DateOnly(2023, 2, 5)));
    }

    [Fact]
    public async void AddOrUpdateWorkout_AddWorkoutWithEmptyExercise()
    {
        await using var context = Fixture.CreateContext();

        await context.Database.BeginTransactionAsync();

        var service = new WorkoutsService(context, NullLogger<WorkoutsService>.Instance);

        var workout = CreateWorkoutObject();
        var exercise = CreateExerciseObject();

        workout.Type = WorkoutTypeCode.Bench;
        workout.User = await context.Users.SingleAsync(q => q.Id == Constants.USER_ID);
        workout.Exercises.Add(exercise);

        await service.AddOrUpdateWorkoutAsync(workout);

        context.ChangeTracker.Clear();

        var addedWorkout = await context.Workouts.Include(q => q.Exercises)
            .SingleAsync(q => q.Type == WorkoutTypeCode.Bench);

        Assert.Single(addedWorkout.Exercises);
    }

    [Fact]
    public async void AddOrUpdateWorkout_AddWorkoutWithExerciseWithOneSet()
    {
        await using var context = Fixture.CreateContext();

        await context.Database.BeginTransactionAsync();

        var service = new WorkoutsService(context, NullLogger<WorkoutsService>.Instance);

        var workout = CreateWorkoutObject();
        var exercise = CreateExerciseObject();
        var set = CreateSetObject();

        workout.User = await context.Users.SingleAsync(q => q.Id == Constants.USER_ID);

        exercise.Sets.Add(set);
        workout.Exercises.Add(exercise);

        await service.AddOrUpdateWorkoutAsync(workout);

        context.ChangeTracker.Clear();

        var addedWorkout = await context.Workouts.Include(q => q.Exercises)
            .ThenInclude(q => q.Sets)
            .SingleAsync(q => q.Id == 1);

        Assert.Equal(100, addedWorkout.Exercises.First().Sets.First().Weight);
    }

    [Fact]
    public async void AddOrUpdateWorkout_UpdateWorkoutToRemoveExercise()
    {
        await using var context = Fixture.CreateContext();

        await context.Database.BeginTransactionAsync();

        var service = new WorkoutsService(context, NullLogger<WorkoutsService>.Instance);

        // Prepare updated workout object
        var workout = CreateWorkoutObject();
        workout.Id = Constants.WORKOUT_ID;

        var exercises = new List<Exercise>
        {
            new()
            {
                Id = 1,
                ExerciseInfoId = 1,
                Sets = new List<Set>
                {
                    new()
                    {
                        Id = 1,
                        Reps = 5,
                        Weight = 200,
                        Completed = false
                    }
                }
            }
        };

        workout.Exercises = exercises;
        workout.User = await context.Users.SingleAsync(q => q.Id == Constants.USER_ID);

        await service.AddOrUpdateWorkoutAsync(workout);

        context.ChangeTracker.Clear();

        var updatedWorkout = await context.Workouts.Include(q => q.Exercises).ThenInclude(q => q.Sets)
            .SingleAsync(q => q.Id == Constants.WORKOUT_ID);

        Assert.Equal(200, updatedWorkout.Exercises.First().Sets.First().Weight);
        Assert.Equal(1, updatedWorkout.Exercises.Count);
    }

    [Fact]
    public async void AddOrUpdateWorkout_UpdateWorkoutToRemoveSet()
    {
        await using var context = Fixture.CreateContext();
        await context.Database.BeginTransactionAsync();

        var service = new WorkoutsService(context, NullLogger<WorkoutsService>.Instance);

        // Prepare updated workout object
        var workout = CreateWorkoutObject();
        workout.Id = Constants.WORKOUT_ID;

        var exercises = new List<Exercise>
        {
            new()
            {
                ExerciseInfoId = 1,
                Sets = new List<Set>()
            },
            new()
            {
                ExerciseInfoId = 2,
                Sets = new List<Set>
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

        workout.Exercises = exercises;

        await service.AddOrUpdateWorkoutAsync(workout);

        context.ChangeTracker.Clear();

        var updatedWorkout = await context.Workouts.Include(q => q.Exercises).ThenInclude(q => q.Sets)
            .SingleAsync(q => q.Id == Constants.WORKOUT_ID);

        Assert.Equal(0, updatedWorkout.Exercises.First().Sets.Count);
    }

    [Fact]
    public async void AddOrUpdateWorkout_UpdateWorkoutWithNewExercise()
    {
        await using var context = Fixture.CreateContext();
        await context.Database.BeginTransactionAsync();

        var service = new WorkoutsService(context, NullLogger<WorkoutsService>.Instance);

        // Prepare updated workout object
        var workout = CreateWorkoutObject();
        workout.Id = Constants.WORKOUT_ID;

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
                Sets = new List<Set>
                {
                    new()
                    {
                        Reps = 10,
                        Weight = 100,
                        Completed = false
                    }
                }
            },
            new()
            {
                ExerciseInfoId = 2,
                Sets = new List<Set>()
            }
        };

        workout.Exercises = exercises;

        await service.AddOrUpdateWorkoutAsync(workout);

        context.ChangeTracker.Clear();

        var updatedWorkout = await context.Workouts.Include(q => q.Exercises).ThenInclude(q => q.Sets)
            .SingleAsync(q => q.Id == Constants.WORKOUT_ID);

        Assert.Equal(3, updatedWorkout.Exercises.Count);
    }

    [Fact]
    public async void AddOrUpdateWorkout_UpdateWorkoutWithNewSet()
    {
        await using var context = Fixture.CreateContext();
        await context.Database.BeginTransactionAsync();

        var service = new WorkoutsService(context, NullLogger<WorkoutsService>.Instance);

        // Prepare updated workout object
        var workout = CreateWorkoutObject();
        workout.Id = Constants.WORKOUT_ID;

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
                    },
                    new()
                    {
                        Reps = 5,
                        Weight = 205,
                        Completed = false
                    }
                }
            },
            new()
            {
                ExerciseInfoId = 2,
                Sets = new List<Set>
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

        workout.Exercises = exercises;

        await service.AddOrUpdateWorkoutAsync(workout);

        context.ChangeTracker.Clear();

        var updatedWorkout = await context.Workouts.Include(q => q.Exercises).ThenInclude(q => q.Sets)
            .SingleAsync(q => q.Id == Constants.WORKOUT_ID);

        Assert.Equal(2, updatedWorkout.Exercises.First().Sets.Count);
    }
    
    // TODO: Add tests for AddProgramCycle()

    private static Workout CreateWorkoutObject() => new()
    {
        Date = null,
        StartDate = null,
        EndDate = null,
        Type = WorkoutTypeCode.None,
        UserId = Constants.USER_ID
    };

    private static Exercise CreateExerciseObject() => new()
    {
        ExerciseInfoId = 1
    };

    private static Set CreateSetObject() => new()
    {
        Reps = 5,
        Weight = 100,
        Completed = false
    };
}