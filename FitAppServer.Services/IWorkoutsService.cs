using FitAppServer.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitAppServer.Services
{
    public interface IWorkoutsService
    {
        Task<List<Workout>> GetByUserIdAsync(string userid);
        Task<Workout?> GetByWorkoutIdAsync(int workoutid);
        Task<ICollection<Exercise>> GetExercisesByWorkoutIdsAsync(ICollection<int> ids);
        Task<Workout> AddOrUpdateWorkoutAsync(Workout workout);

        Task DeleteWorkoutAsync(int workoutid);
    }
}