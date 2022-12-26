using FitAppServer.DataAccess.Entities;
using FitAppServer.Services.Models;

namespace FitAppServer.Services.Services;

public interface IWorkoutsService
{
    Task<ICollection<Workout>> GetByUserIdAsync(string userid);
    Task<Workout?> GetByWorkoutIdAsync(int workoutid);
    Task<Workout> AddOrUpdateWorkoutAsync(Workout workout);
    Task DeleteWorkoutAsync(int workoutid);

    Task<IReadOnlyDictionary<int, IReadOnlyCollection<Workout>>> AddProgramCycle(ProgramCycle programCycle);
}