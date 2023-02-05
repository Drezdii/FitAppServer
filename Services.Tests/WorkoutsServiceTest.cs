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

        Assert.Empty(context.Workouts.ToList());
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

        var user = await context.Users.SingleAsync(q => q.Id == Constants.USER_ID);

        var workout = new Workout
        {
            Date = new DateOnly(2023, 2, 5),
            StartDate = null,
            EndDate = null,
            User = user,
            Type = WorkoutTypeCode.Deadlift,
            UserId = Constants.USER_ID
        };

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

        var user = await context.Users.SingleAsync(q => q.Id == Constants.USER_ID);

        var exercise = new Exercise
        {
            Id = 0,
            ExerciseInfoId = 1
        };

        var workout = new Workout
        {
            Date = null,
            StartDate = null,
            EndDate = null,
            User = user,
            Type = WorkoutTypeCode.Bench,
            UserId = Constants.USER_ID
        };

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

        var user = await context.Users.SingleAsync(q => q.Id == Constants.USER_ID);

        var exercise = new Exercise
        {
            Id = 0,
            ExerciseInfoId = 1
        };

        var set = new Set
        {
            Id = 0,
            Reps = 5,
            Weight = 100,
            Completed = false
        };

        exercise.Sets.Add(set);

        var workout = new Workout
        {
            Date = null,
            StartDate = null,
            EndDate = null,
            User = user,
            Type = WorkoutTypeCode.Bench,
            UserId = Constants.USER_ID
        };

        workout.Exercises.Add(exercise);

        await service.AddOrUpdateWorkoutAsync(workout);

        context.ChangeTracker.Clear();

        var addedWorkout = await context.Workouts.Include(q => q.Exercises)
            .ThenInclude(q => q.Sets)
            .SingleAsync(q => q.Type == WorkoutTypeCode.Bench);

        Assert.Equal(5, addedWorkout.Exercises.First().Sets.First().Reps);
    }
}