using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitAppServer.DataLoaders;
using FitAppServer.Services;
using FitAppServer.Types;
using HotChocolate;

namespace FitAppServer.Resolvers;

public class WorkoutsResolver
{
    private readonly IWorkoutsService _workoutsService;

    public WorkoutsResolver(IWorkoutsService service)
    {
        _workoutsService = service;
    }

    public async Task<ExerciseType[]> GetExercises([Parent] WorkoutType workout, ExerciseDataLoader dataLoader)
    {
        return await dataLoader.LoadAsync(workout.Id);
    }

    public async Task<WorkoutType?> GetWorkout(int id)
    {
        var workout = await _workoutsService.GetByWorkoutIdAsync(id);

        if (workout == null)
        {
            return null;
        }

        return new WorkoutType
        {
            Id = workout.Id,
            Date = workout.Date,
            StartDate = workout.StartDate,
            EndDate = workout.EndDate
        };
    }

    public async Task<List<WorkoutType>> GetUserWorkouts(string userid)
    {
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
}