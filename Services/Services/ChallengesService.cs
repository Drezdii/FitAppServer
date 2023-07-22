using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            .Include(q => q.ExerciseInfo)
            .Include(q => q.Set)
            .ThenInclude(q => q.Exercise)
            .ThenInclude(q => q.Workout)
            .GroupBy(p => p.ExerciseInfo.Id)
            .Select(q => q.OrderByDescending(p => p.Id).First())
            .ToListAsync();
    }

    public async Task<ICollection<ChallengeEntry>> GetChallengesEntriesByUserId(string userid)
    {
        return await _context.ChallengeEntries.Where(q => q.User.Uuid == userid).Include(q => q.Challenge)
            .ToListAsync();
    }

    public async Task<ICollection<ChallengeEntry>> GetTop3Challenges(string userid)
    {
        return await _context.ChallengeEntries
            .Where(q => q.User.Uuid == userid && q.CompletedAt == null)
            .Include(q => q.Challenge)
            .OrderByDescending(q => q.Value / q.Challenge.Goal).Take(3).ToListAsync();
    }
}