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

    public async Task Check(WorkoutAction action, Workout payload)
    {
        if (action is WorkoutAction.Created or WorkoutAction.Deleted)
        {
            var challenge = await _context.ChallengeEntries.FirstOrDefaultAsync(
                q => q.UserId == payload.UserId && q.Challenge.Id == GetId() && q.CompletedAt == null);

            if (challenge == null)
            {
                return;
            }

            var additionValue = 1;
            // Decrease the counter on workout deletion
            if (action == WorkoutAction.Deleted)
            {
                additionValue = -1;
            }

            await _context.ChallengeEntries.Where(q => q.UserId == payload.UserId && q.Challenge.Id == GetId())
                .ExecuteUpdateAsync(q => q.SetProperty(c => c.Value, c => c.Value + additionValue));
        }
    }


    public string GetId() => "numOfCreatedWorkoutsChallenge";
}