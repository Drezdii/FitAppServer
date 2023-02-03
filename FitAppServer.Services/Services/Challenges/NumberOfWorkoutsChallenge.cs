﻿using System;
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
        if (action is not WorkoutAction.Created and not WorkoutAction.Deleted)
        {
            return;
        }

        var challenge = await _context.ChallengeEntries.FirstOrDefaultAsync(
            q => q.UserId == workout.UserId && q.Challenge.Id == GetId() && q.CompletedAt == null);

        if (challenge == null)
        {
            // TODO: Handle missing ChallengeEntry
            return;
        }

        var additionValue = 1;

        // Decrease the counter on workout deletion
        if (action == WorkoutAction.Deleted)
        {
            additionValue = -1;
        }

        await _context.ChallengeEntries.Where(q => q.UserId == workout.UserId && q.Challenge.Id == GetId())
            .ExecuteUpdateAsync(q => q.SetProperty(c => c.Value, c => c.Value + additionValue));
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