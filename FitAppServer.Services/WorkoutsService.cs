using System;
using FitAppServer.DataAccess;
using FitAppServer.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace FitAppServer.Services;

public class WorkoutsService : IWorkoutsService, IAsyncDisposable
{
    private readonly FitAppContext _context;
    private readonly ILogger _logger;

    public WorkoutsService(IDbContextFactory<FitAppContext> context, ILogger<WorkoutsService> logger)
    {
        _context = context.CreateDbContext();
        _logger = logger;
    }

    public async Task<List<Workout>> GetByUserIdAsync(string userid)
    {
        return await _context.Workouts
            .Where(q => q.User.Uuid == userid)
            .OrderByDescending(q => q.StartDate)
            .ToListAsync();
    }

    public async Task<Workout?> GetByWorkoutIdAsync(int workoutid)
    {
        return await _context.Workouts.Where(q => q.Id == workoutid)
            .Include(q => q.Exercises)
            .ThenInclude(q => q.ExerciseInfo)
            .Include(q => q.Exercises)
            .ThenInclude(q => q.Sets)
            .Include(q => q.User)
            .AsNoTrackingWithIdentityResolution()
            .AsSplitQuery()
            .SingleOrDefaultAsync();
    }

    public async Task<ICollection<Exercise>> GetExercisesByWorkoutIdsAsync(IReadOnlyCollection<int> ids)
    {
        return await _context.Exercises
            .Where(q => ids.Contains(q.WorkoutId))
            .AsNoTrackingWithIdentityResolution()
            .ToListAsync();
    }

    public async Task<ICollection<Set>> GetSetsByExerciseIdsAsync(IReadOnlyCollection<int> ids)
    {
        return await _context.Sets
            .Where(q => ids.Contains(q.ExerciseId))
            .AsNoTrackingWithIdentityResolution()
            .ToListAsync();
    }

    public async Task<Workout> AddOrUpdateWorkoutAsync(Workout workout)
    {
        // Clear any IDs that might have been posted
        if (workout.Id < 0)
        {
            workout.Id = 0;
        }

        foreach (var ex in workout.Exercises)
        {
            foreach (var set in ex.Sets)
            {
                if (set.Id < 0)
                {
                    set.Id = 0;
                }
            }

            if (ex.Id < 0)
            {
                ex.Id = 0;
            }
        }

        // Check if this is a new workout
        if (workout.Id <= 0)
        {
            _context.Workouts.Add(workout);
            _context.Entry(workout.User).State = EntityState.Unchanged;

            await _context.SaveChangesAsync();
            return workout;
        }

        // Begin tracking the workout
        _context.Update(workout);

        // Remove all exercises and sets that are missing from the payload
        // Get all IDs from the payload before Entity Framework populates it when searching for all existing exercises
        var exIds = workout.Exercises.Select(q => q.Id).ToList();

        var setsIds = new List<int>();

        foreach (var ex in workout.Exercises)
        {
            setsIds.AddRange(ex.Sets.Select(q => q.Id).ToList());
        }

        // Load all exercises in this workout at once
        var allExistingExercises = await _context.Exercises.Where(q => q.Workout.Id == workout.Id)
            .Include(q => q.Sets).ToListAsync();

        // Get exercises that are missing from the payload
        var missingExercises = allExistingExercises.Where(q => !exIds.Contains(q.Id));
        _context.Exercises.RemoveRange(missingExercises);

        var allExistingSets = allExistingExercises.SelectMany(q => q.Sets).ToList();

        // Get sets that are missing from the payload
        var missingSets = allExistingSets.Where(q => !setsIds.Contains(q.Id)).ToList();
        _context.Sets.RemoveRange(missingSets);

        await _context.SaveChangesAsync();
        return workout;
    }

    public async Task DeleteWorkoutAsync(int workoutid)
    {
        var workout = new Workout
        {
            Id = workoutid
        };

        _context.Workouts.Remove(workout);

        await _context.SaveChangesAsync();
    }

    public ValueTask DisposeAsync()
    {
        return _context.DisposeAsync();
    }
}