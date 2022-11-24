using System;
using System.Linq;
using System.Threading.Tasks;
using FitAppServer.DataAccess;
using FitAppServer.DataAccess.Entities;
using FitAppServer.Services.Achievements.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FitAppServer.Services.Achievements;

public class AchievementsManager : IAchievementsManager
{
    private readonly OneRepMaxService _oneRepMaxService;
    private readonly ILogger _logger;
    private readonly FitAppContext _context;

    public AchievementsManager(ILogger<AchievementsManager> logger, FitAppContext context,
        OneRepMaxService oneRepMaxService)
    {
        _logger = logger;
        _context = context;
        _oneRepMaxService = oneRepMaxService;
    }

    public async Task Notify(Action action, Workout workout)
    {
        switch (action)
        {
            case Action.WorkoutCreated:
                await _oneRepMaxService.OnWorkoutChanged(workout);
                // Add other services that should react to this action
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(action), action, null);
        }

        await _context.SaveChangesAsync();
        _logger.LogInformation("One Rep Max changes saved");
    }
}