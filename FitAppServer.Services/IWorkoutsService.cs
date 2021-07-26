using FitAppServer.DataAccess.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitAppServer.Services
{
    public interface IWorkoutsService
    {
        Task<List<Workout>> GetByUserIDAsync(string userid);
        Task<Workout> GetByWorkoutIDAsync(int workoutid);
        Task<Workout> AddOrUpdateWorkout(Workout workout);

        Task DeleteWorkout(int workoutid);
    }
}
