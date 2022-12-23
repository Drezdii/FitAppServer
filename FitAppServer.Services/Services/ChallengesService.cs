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
            .Where(q => q.User.Uuid == userId)
            .GroupBy(p => p.Set.Exercise.ExerciseInfoId)
            .Select(g => g.OrderByDescending(p => p.Set.Exercise.Workout.Date).ThenByDescending(p => p.Id)
                .First()
            ).ToListAsync();
    }

    public async Task<ICollection<ChallengeEntry>> GetChallengesEntriesByUserId(string userid)
    {
        return await _context.ChallengeEntries.Where(q => q.User.Uuid == userid).Include(q => q.Challenge).ToListAsync();
    }
}