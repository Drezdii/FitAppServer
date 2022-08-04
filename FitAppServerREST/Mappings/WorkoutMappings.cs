using FitAppServer.DataAccess.Entities;
using FitAppServerREST.DTOs.Creator;
using FitAppServerREST.DTOs.Workouts;

namespace FitAppServerREST.Mappings;

public static class WorkoutMappings
{
    public static WorkoutDto ToDto(this Workout workout)
    {
        var exercises = new List<ExerciseDto>();

        foreach (var exercise in workout.Exercises)
        {
            var sets = new List<SetDto>();

            foreach (var set in exercise.Sets)
                sets.Add(new SetDto
                {
                    Id = set.Id,
                    Completed = set.Completed,
                    Reps = set.Reps,
                    Weight = set.Weight
                });

            exercises.Add(new ExerciseDto
            {
                ExerciseType = (WorkoutTypeCode) exercise.ExerciseInfoId,
                Id = exercise.Id,
                Sets = sets
            });
        }

        return new WorkoutDto
        {
            Id = workout.Id,
            Date = workout.Date,
            StartDate = workout.StartDate,
            EndDate = workout.EndDate,
            Type = workout.Type,
            Exercises = exercises,
            WorkoutProgramDetails = workout.WorkoutProgramDetails?.ToDto()
        };
    }

    public static Workout ToModel(this WorkoutDto workout)
    {
        var exercises = new List<Exercise>();

        foreach (var exercise in workout.Exercises)
        {
            var sets = new List<Set>();

            foreach (var set in exercise.Sets)
                sets.Add(new Set
                {
                    Id = set.Id,
                    Completed = set.Completed,
                    Reps = set.Reps,
                    Weight = set.Weight
                });

            exercises.Add(new Exercise
            {
                ExerciseInfoId = (int) exercise.ExerciseType,
                Id = exercise.Id,
                Sets = sets
            });
        }

        return new Workout
        {
            Id = workout.Id,
            Date = workout.Date,
            StartDate = workout.StartDate,
            EndDate = workout.EndDate,
            Type = workout.Type,
            Exercises = exercises
        };
    }

    public static Workout ToModel(this NewWorkoutDto workout)
    {
        var exercises = new List<Exercise>();

        foreach (var exercise in workout.Exercises)
        {
            var sets = new List<Set>();

            foreach (var set in exercise.Sets)
                sets.Add(new Set
                {
                    Reps = set.Reps,
                    Weight = set.Weight,
                    Completed = false
                });

            exercises.Add(new Exercise
            {
                ExerciseInfoId = (int) exercise.ExerciseType,
                Sets = sets
            });
        }

        return new Workout
        {
            Date = workout.Date,
            StartDate = workout.StartDate,
            EndDate = workout.EndDate,
            Type = workout.Type,
            Exercises = exercises
        };
    }

    public static WorkoutProgram ToModel(this WorkoutProgramDetailsDto workoutProgramDetails)
    {
        return new WorkoutProgram
        {
            Id = workoutProgramDetails.Id,
        };
    }

    private static WorkoutProgramDetailsDto ToDto(this WorkoutProgramDetail workoutProgram)
    {
        return new WorkoutProgramDetailsDto
        {
            Id = workoutProgram.Program.Id,
            Name = workoutProgram.Program.Name,
            Week = workoutProgram.Week
        };
    }
}