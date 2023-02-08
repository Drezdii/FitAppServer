using System;
using System.Linq;
using System.Threading.Tasks;
using FitAppServer.DataAccess;
using FitAppServer.DataAccess.Entities;
using FitAppServer.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace FitAppServer.Services.Services.Challenges;

public class NumberOfWorkoutsChallenge : IChallenge
{
    private readonly FitAppContext _context;

    public NumberOfWorkoutsChallenge(FitAppContext context)
    {
        _context = context;
    }

    public async Task Check(WorkoutAction action, Workout workout)
    {
        var entry = await _context.ChallengeEntries.Where(q => q.UserId == workout.UserId && q.Challenge.Id == GetId())
            .FirstOrDefaultAsync();

        if (entry == null)
        {
            // TODO: Add appropriate exception
            return;
        }

        // TODO: Update this once proper diffing algorithm is implemented for updating/deleting workouts
        var newCount = await _context.Workouts.Where(q =>
                q.UserId == workout.UserId && q.StartDate != null && q.EndDate != null)
            .CountAsync();
        
        entry.Value = newCount;
    }


    public string GetId() => "numOfCreatedWorkoutsChallenge2022";

    public Challenge GetDefinition()
    {
        return new Challenge
        {
            Id = GetId(),
            NameTranslationKey = GetId(),
            DescriptionTranslationKey = "numOfCreatedWorkoutsChallengeDescription",
            StartDate = new DateTime(2023, 1, 1, 0, 0, 0),
            EndDate = new DateTime(2023, 12, 31, 23, 59, 59),
            Goal = 200f
        };
    }
}