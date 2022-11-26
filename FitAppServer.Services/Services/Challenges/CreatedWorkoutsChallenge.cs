using FitAppServer.DataAccess;
using FitAppServer.DataAccess.Entities;
using FitAppServer.Services.Models;

namespace FitAppServer.Services.Services.Challenges;

public class CreatedWorkoutsChallenge : IChallenge
{
    private readonly FitAppContext _context;

    public CreatedWorkoutsChallenge(FitAppContext context)
    {
        _context = context;
    }

    public void Check(WorkoutAction action, Workout payload)
    {
        if (action is WorkoutAction.Created or WorkoutAction.Deleted)
        {
            // _context.ChallengeEntries.Where(q => q.UserId == payload.UserId && q.Challenge.Identifier == GetIdentifier());
            // Run checks
        }
    }


    // Use those identifiers when setting up the challenges in database when creating a new user
    public string GetId()
    {
        return "numOfCreatedWorkoutsChall";
    }
}