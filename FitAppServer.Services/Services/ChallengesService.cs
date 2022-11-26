using FitAppServer.DataAccess;
using FitAppServer.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitAppServer.Services.Services;

public class ChallengesService : IChallengesService
{
    private readonly FitAppContext _context;

    public ChallengesService(FitAppContext context)
    {
        _context = context;
    }

    public async Task<ICollection<OneRepMax>> GetOneRepMaxesByUserId(string userId)
    {
        // Get one rep max for each big lift based on workout date and the id of the one rep max
        return await _context.OneRepMaxes
            .GroupBy(p => p.Set.Exercise.ExerciseInfoId)
            .Select(g => g.OrderByDescending(p => p.Set.Exercise.Workout.Date).ThenByDescending(p => p.Id)
                .First()
            ).ToListAsync();
    }

    public List<ChallengeEntry> GetChallengesByUserId(string userid)
    {
        throw new NotImplementedException();
    }
}