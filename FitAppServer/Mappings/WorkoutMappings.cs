using FitAppServer.DataAccess.Entites;
using FitAppServer.DTOs.Workouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitAppServer.Mappings
{
    public static class WorkoutMappings
    {
        public static WorkoutDTO ToDTO(this Workout workout)
        {
            var exercises = new List<ExerciseDTO>();

            foreach (var exercise in workout.Exercises)
            {
                var sets = new List<SetDTO>();

                foreach (var set in exercise.Sets)
                {
                    sets.Add(new SetDTO
                    {
                        ID = set.ID,
                        Completed = set.Completed,
                        Reps = set.Reps,
                        Weight = set.Weight
                    });
                }

                exercises.Add(new ExerciseDTO
                {
                    ExerciseInfoID = exercise.ExerciseInfoID,
                    ID = exercise.ID,
                    Sets = sets
                });
            }

            var description = new WorkoutDescription
            {
                ID = workout.ID,
                Date = workout.Date,
                StartDate = workout.StartDate,
                EndDate = workout.EndDate,
                Type = (int)workout.Type
            };

            return new WorkoutDTO
            {
                Description = description,
                Exercises = exercises
            };
        }

        public static Workout FromDTO(this WorkoutDTO workout)
        {
            var exercises = new List<Exercise>();

            foreach (var exercise in workout.Exercises)
            {
                var sets = new List<Set>();

                foreach (var set in exercise.Sets)
                {
                    sets.Add(new Set
                    {
                        ID = set.ID,
                        Completed = set.Completed,
                        Reps = set.Reps,
                        Weight = set.Weight
                    });
                }

                exercises.Add(new Exercise
                {
                    ExerciseInfoID = exercise.ExerciseInfoID,
                    ID = exercise.ID,
                    Sets = sets
                });
            }

            return new Workout
            {
                ID = workout.Description.ID,
                Date = workout.Description.Date,
                StartDate = workout.Description.StartDate,
                EndDate = workout.Description.EndDate,
                Type = (WorkoutType)workout.Description.Type,
                Exercises = exercises
            };
        }
    }
}
