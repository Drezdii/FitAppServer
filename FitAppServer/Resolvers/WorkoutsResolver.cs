using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitAppServer.DataLoaders;
using FitAppServer.Services;
using FitAppServer.Types;
using FitAppServer.Utils;
using HotChocolate;
using Microsoft.Extensions.Logging;

namespace FitAppServer.Resolvers;

public class WorkoutsResolver
{
    private readonly IClaimsAccessor _claims;
    private readonly ILogger _logger;
    private readonly IWorkoutsService _workoutsService;

    public WorkoutsResolver(IWorkoutsService service, ILogger<WorkoutsResolver> logger, IClaimsAccessor accessor)
    {
        _workoutsService = service;
        _logger = logger;
        _claims = accessor;
    }

    public async Task<WorkoutType?> GetWorkout(int id)
    {
        _logger.LogInformation("Getting workout with id: {Id}", id);

        var workout = await _workoutsService.GetByWorkoutIdAsync(id);

        if (workout == null) return null;

        return new WorkoutType
        {
            Id = workout.Id,
            Date = workout.Date,
            StartDate = workout.StartDate,
            EndDate = workout.EndDate,
            Type = workout.Type
        };
    }

    public async Task<List<WorkoutType>> GetUserWorkouts()
    {
        var userid = _claims.UserId;
        _logger.LogInformation("Getting workouts for user: {Userid}", userid);
        var workout = await _workoutsService.GetByUserIdAsync(userid);

        return workout.Select(q => new WorkoutType
        {
            Id = q.Id,
            Date = q.Date,
            StartDate = q.StartDate,
            EndDate = q.EndDate,
            Type = q.Type
        }).ToList();
    }

    public async Task<ExerciseType[]> GetExercises([Parent] WorkoutType workout, ExerciseDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(workout.Id);
    }

    public async Task<SetType[]> GetSets([Parent] ExerciseType exercise, SetsDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(exercise.Id);
    }
}