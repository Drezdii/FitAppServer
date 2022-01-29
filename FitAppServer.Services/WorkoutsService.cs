using FitAppServer.DataAccess;
using FitAppServer.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitAppServer.Services
{
    public class WorkoutsService : IWorkoutsService
    {
        private readonly FitAppContext _context;

        public WorkoutsService(FitAppContext context)
        {
            _context = context;
        }

        public async Task<List<Workout>> GetByUserIDAsync(string userid)
        {
            return await _context.Workouts
                .Where(q => q.User.Uuid == userid)
                .OrderByDescending(q => q.StartDate)
                .ToListAsync();
        }

        public async Task<Workout> GetByWorkoutIDAsync(int workoutid)
        {
            return await _context.Workouts.Where(q => q.ID == workoutid)
                .Include(q => q.Exercises)
                .ThenInclude(q => q.ExerciseInfo)
                .Include(q => q.Exercises)
                .ThenInclude(q => q.Sets)
                .Include(q => q.User)
                .AsNoTrackingWithIdentityResolution()
                .AsSplitQuery()
                .SingleOrDefaultAsync();
        }

        public async Task<Workout> AddOrUpdateWorkout(Workout workout)
        {
            // Clear any IDs that might have been posted
            if (workout.ID < 0)
            {
                workout.ID = 0;
            }

            foreach (var ex in workout.Exercises)
            {
                foreach (var set in ex.Sets)
                {
                    if (set.ID < 0)
                    {
                        set.ID = 0;
                    }
                }

                if (ex.ID < 0)
                {
                    ex.ID = 0;
                }
            }

            // Check if this is a new workout
            if (workout.ID <= 0)
            {
                _context.Workouts.Add(workout);
                _context.Entry(workout.User).State = EntityState.Unchanged;
            }
            else
            {
                // Begin tracking the workout
                _context.Update(workout);

                // Remove all exercises and sets that are missing in the payload
                // Get all IDs from the payload before Entity Framework populates it when searching for all existing exercises
                var exIds = workout.Exercises.Select(q => q.ID).ToList();

                var setsIds = new List<int>();

                foreach (var ex in workout.Exercises)
                {
                    setsIds.AddRange(ex.Sets.Select(q => q.ID).ToList());
                }

                // Load all exercises in this workout at once
                var allExistingExercises = await _context.Exercises.Where(q => q.Workout.ID == workout.ID)
                    .Include(q => q.Sets).ToListAsync();

                // Get exercises that are missing from the payload
                var missingExercises = allExistingExercises.Where(q => !exIds.Contains(q.ID));
                _context.Exercises.RemoveRange(missingExercises);

                var allExistingSets = allExistingExercises.SelectMany(q => q.Sets).ToList();

                // Get sets that are missing from the payload
                var missingSets = allExistingSets.Where(q => !setsIds.Contains(q.ID)).ToList();
                _context.Sets.RemoveRange(missingSets);
            }

            await _context.SaveChangesAsync();
            return workout;
        }

        public async Task DeleteWorkout(int workoutid)
        {
            var workout = new Workout
            {
                ID = workoutid
            };

            _context.Workouts.Remove(workout);

            await _context.SaveChangesAsync();
        }
    }
}