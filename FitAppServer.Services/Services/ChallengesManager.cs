using FitAppServer.DataAccess;
using FitAppServer.DataAccess.Entities;
using FitAppServer.Services.Models;
using FitAppServer.Services.Services.Challenges;

namespace FitAppServer.Services.Services;

public class ChallengesManager : IChallengesManager
{
    private readonly FitAppContext _context;

    private readonly List<IChallenge> _challenges = new();

    public ChallengesManager(FitAppContext context)
    {
        _context = context;
        // Somehow add all challenges to _challenges list here
        _challenges.Add(new NumberOfWorkoutsChallenge(context));
    }

    public async Task Notify(WorkoutAction action, Workout payload)
    {
        foreach (var challenge in _challenges)
        {
            challenge.Check(action, payload);
        }

        // Save all changes made by different challenges
        await _context.SaveChangesAsync();
    }
}