using FitAppServer.DataAccess;
using FitAppServer.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FitAppServer.Services.Services;

public class AchievementsService : IAchievementsService
{
    private readonly FitAppContext _context;
    private readonly ILogger<AchievementsService> _logger;

    public AchievementsService(FitAppContext context, ILogger<AchievementsService> logger)
    {
        _context = context;
        _logger = logger;
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
}