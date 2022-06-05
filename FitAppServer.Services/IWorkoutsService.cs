using System.Collections.Generic;
using System.Threading.Tasks;
using FitAppServer.DataAccess.Entities;

namespace FitAppServer.Services;

public interface IWorkoutsService
{
    Task<ICollection<Workout>> GetByUserIdAsync(string userid);
    Task<Workout?> GetByWorkoutIdAsync(int workoutid);
    Task<Workout> AddOrUpdateWorkoutAsync(Workout workout);
    Task DeleteWorkoutAsync(int workoutid);
}