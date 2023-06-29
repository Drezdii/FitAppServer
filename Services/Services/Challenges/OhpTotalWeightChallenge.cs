using System;
using System.Linq;
using System.Threading.Tasks;
using FitAppServer.DataAccess;
using FitAppServer.DataAccess.Entities;
using FitAppServer.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace FitAppServer.Services.Services.Challenges;

public class OhpTotalWeightChallenge : IChallenge
{
    private readonly FitAppContext _context;

    public OhpTotalWeightChallenge(FitAppContext context)
    {
        _context = context;
    }

    public async Task Check(WorkoutAction action, Workout workout)
    {
        // TODO: Update this once proper diffing algorithm is implemented for updating/deleting workouts
        var newCount = await _context.Sets.Where(q =>
                q.Exercise.Workout.UserId == workout.UserId &&
                q.Exercise.ExerciseInfoId == (int)WorkoutTypeCode.Ohp &&
                q.Completed)
            .SumAsync(q => q.Reps * q.Weight);

        await _context.ChallengeEntries.Where(q => q.UserId == workout.UserId && q.Challenge.Id == GetId())
            .ExecuteUpdateAsync(q => q.SetProperty(c => c.Value, newCount));
    }

    public string GetId() => "ohpTotalWeight2022";

    public Challenge GetDefinition()
    {
        return new Challenge
        {
            Id = GetId(),
            NameTranslationKey = GetId(),
            DescriptionTranslationKey = "ohpTotalWeight2022Description",
            StartDate = new DateTime(2023, 1, 1, 0, 0, 0),
            EndDate = new DateTime(2023, 12, 31, 23, 59, 59),
            Goal = 200000f,
            Unit = "kg"
        };
    }
}