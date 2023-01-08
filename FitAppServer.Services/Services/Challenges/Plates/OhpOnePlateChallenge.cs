using FitAppServer.DataAccess;
using FitAppServer.DataAccess.Entities;
using FitAppServer.Services.Models;
using FitAppServer.Services.Utils;
using Microsoft.EntityFrameworkCore;

namespace FitAppServer.Services.Services.Challenges.Plates;

public class OhpOnePlateChallenge : IChallenge
{
    private readonly FitAppContext _context;

    public OhpOnePlateChallenge(FitAppContext context)
    {
        _context = context;
    }

    public async Task Check(WorkoutAction action, Workout workout)
    {
        var correctSet = workout.Exercises
            .Where(q => q.ExerciseInfoId == (int)WorkoutTypeCode.Ohp)
            .SelectMany(q => q.Sets)
            .FirstOrDefault(q => q.Weight >= 60);

        if (correctSet is null)
        {
            return;
        }

        var entry = await _context.ChallengeEntries.Where(q => q.UserId == workout.UserId && q.Challenge.Id == GetId())
            .FirstOrDefaultAsync();

        if (entry == null)
        {
            // TODO: Add custom exception
            throw new Exception();
        }

        if (entry.CompletedAt != null)
        {
            return;
        }

        entry.Value = 1;
        entry.CompletedAt = DateOnlyHelper.DateNow();
    }

    public string GetId() => "ohpOnePlateChallenge";

    public Challenge GetDefinition()
    {
        return new Challenge
        {
            Id = GetId(),
            NameTranslationKey = GetId(),
            StartDate = new DateTime(2023, 1, 1, 0, 0, 0),
            Goal = 1f,
        };
    }
}