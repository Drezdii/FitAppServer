using System.Linq;
using FitAppServer.DataAccess.Entities;

namespace FitAppServer.Types;

public static class Mappers
{
    public static Workout ToModel(this WorkoutInput input)
    {
        var exercises = input.Exercises.Select(e => new Exercise
        {
            Id = e.Id,
            ExerciseInfoId = e.ExerciseInfoId,
            Workout = new Workout
            {
                Id = input.Id
            },
            Sets = e.Sets.Select(s => new Set
            {
                Id = s.Id,
                Reps = s.Reps,
                Weight = s.Weight,
                Completed = s.Completed
            }).ToList()
        }).ToList();

        return new Workout
        {
            Id = input.Id,
            Date = input.Date,
            StartDate = input.StartDate,
            EndDate = input.EndDate,
            Exercises = exercises,
            Type = input.Type
        };
    }

    public static WorkoutType ToResultType(this Workout input)
    {
        var exercises = input.Exercises.Select(e => new ExerciseType
        {
            Id = e.Id,
            ExerciseInfoId = e.ExerciseInfoId,
            Sets = e.Sets.Select(s => new SetType
            {
                Id = s.Id,
                Reps = s.Reps,
                Weight = s.Weight,
                Completed = s.Completed
            }).ToList()
        }).ToList();

        return new WorkoutType
        {
            Id = input.Id,
            Date = input.Date,
            StartDate = input.StartDate,
            EndDate = input.EndDate,
            Exercises = exercises,
            Type = input.Type
        };
    }
}