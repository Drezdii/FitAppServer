using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitAppServer.DataAccess;
using FitAppServer.DataAccess.Entities;
using FitAppServer.Services.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FitAppServer.Services.Services;

public class WorkoutsService : IWorkoutsService
{
    private readonly FitAppContext _context;
    private readonly ILogger<WorkoutsService> _logger;

    public WorkoutsService(FitAppContext context, ILogger<WorkoutsService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ICollection<Workout>> GetByUserIdAsync(string userid)
    {
        var workouts = await _context.Workouts
            .Where(q => q.User.Uuid == userid)
            .OrderByDescending(q => q.StartDate)
            .Include(q => q.WorkoutProgramDetails)
            .ThenInclude(q => q.Program)
            .ToListAsync();

        return workouts;
    }

    public async Task<Workout?> GetByWorkoutIdAsync(int workoutid)
    {
        var workout = await _context.Workouts.Where(q => q.Id == workoutid)
            .Include(q => q.Exercises)
            .ThenInclude(q => q.ExerciseInfo)
            .Include(q => q.Exercises)
            .ThenInclude(q => q.Sets)
            .Include(q => q.User)
            .AsSplitQuery()
            .SingleOrDefaultAsync();

        return workout;
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
            if (workout.Id != 0)
            {
                continue;
            }

            foreach (var set in ex.Sets)
            {
                if (set.Id != 0)
                {
                    set.Id = 0;
                }
            }

            if (ex.Id != 0)
            {
                ex.Id = 0;
            }
        }

        // Check if this is a new workout
        if (workout.Id == 0)
        {
            _context.Workouts.Add(workout);
            _context.Entry(workout.User).State = EntityState.Unchanged;

            await _context.SaveChangesAsync();
            return workout;
        }

        var existingWorkout = await GetByWorkoutIdAsync(workout.Id);

        if (existingWorkout == null)
        {
            _logger.LogError("Workout with ID: {Id} couldn't be found when trying to update it", workout.Id);
            throw new Exception("Couldn't update workout.");
        }

        existingWorkout.Date = workout.Date;
        existingWorkout.StartDate = workout.StartDate;
        existingWorkout.EndDate = workout.EndDate;

        // Remove exercises that are missing from the payload
        existingWorkout.Exercises = existingWorkout.Exercises
            .IntersectBy(workout.Exercises.Select(q => q.Id), q => q.Id).ToList();

        var addedExercises =
            workout.Exercises.ExceptBy(existingWorkout.Exercises.Select(q => q.Id), x => x.Id).ToList();

        if (addedExercises.Any(ex => ex.Id != 0))
        {
            throw new Exception("New exercise with non-zero ID was posted.");
        }

        existingWorkout.Exercises.AddRange(addedExercises);

        existingWorkout.Exercises.ForEach(ex =>
        {
            var updatedSets = workout.Exercises.First(q => q.Id == ex.Id).Sets;

            ex.Sets = ex.Sets.IntersectBy(updatedSets.Select(q => q.Id), q => q.Id).ToList();

            foreach (var set in ex.Sets)
            {
                var updatedSet = updatedSets.First(q => q.Id == set.Id);

                set.Reps = updatedSet.Reps;
                set.Weight = updatedSet.Weight;
                set.Completed = updatedSet.Completed;
            }

            var addedSets = updatedSets.ExceptBy(ex.Sets.Select(q => q.Id), q => q.Id);
            
            ex.Sets.AddRange(addedSets);
        });
        
        await _context.SaveChangesAsync();
        return existingWorkout;
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

    public async Task<IReadOnlyDictionary<int, IReadOnlyCollection<Workout>>> AddProgramCycle(ProgramCycle programCycle)
    {
        var program = _context.Programs.First(q => q.Id == programCycle.Program.Id);
        foreach (var workoutWeek in programCycle.WorkoutsByWeek)
        {
            var workoutDetails = new WorkoutProgramDetail
            {
                Program = program,
                // TODO: Add automatic counting of cycles already finished
                Cycle = 1,
                Week = workoutWeek.Key,
            };

            // Add program details to each workout
            foreach (var workout in workoutWeek.Value)
            {
                workout.UserId = programCycle.User.Id;
                workout.WorkoutProgramDetails = workoutDetails;
            }

            _context.Workouts.AddRange(workoutWeek.Value);
        }

        await _context.SaveChangesAsync();

        return programCycle.WorkoutsByWeek;
    }
}