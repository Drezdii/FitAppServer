using FitAppServer.DataAccess;
using FitAppServer.DataAccess.Entities;
using FitAppServer.Services.Models;
using FitAppServer.Services.Services.Challenges;
using FitAppServer.Services.Services.Challenges.Plates;

namespace FitAppServer.Services.Services;

public class ChallengesManager : IChallengesManager
{
    private readonly FitAppContext _context;

    private readonly List<IChallenge> _challenges = new();

    public ChallengesManager(FitAppContext context)
    {
        _context = context;
        _challenges.Add(new NumberOfWorkoutsChallenge(context));
        _challenges.Add(new OneRepMaxChallenge(context));
        _challenges.Add(new NumberOfPullupsChallenge(context));
        _challenges.Add(new DeadliftTotalWeightChallenge(context));
        _challenges.Add(new SquatTotalWeightChallenge(context));
        _challenges.Add(new BenchTotalWeightChallenge(context));
        _challenges.Add(new OhpTotalWeightChallenge(context));
        _challenges.Add(new BenchTwoPlatesChallenge(context));
        _challenges.Add(new OhpOnePlateChallenge(context));
        _challenges.Add(new DeadliftTwoPlatesChallenge(context));
        _challenges.Add(new DeadliftThreePlatesChallenge(context));
    }

    public async Task Notify(WorkoutAction action, Workout payload)
    {
        foreach (var challenge in _challenges)
        {
            await challenge.Check(action, payload);
        }

        // Save all changes made by different challenges
        await _context.SaveChangesAsync();
    }
}