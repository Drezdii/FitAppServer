using FitAppServer.DataAccess.Entities;

namespace FitAppServerREST.Services;

public class AchievementsManager : IAchievementsManager
{
    private readonly IAchievementsService _achievementsService;
    private readonly ILogger _logger;

    public AchievementsManager(ILogger<AchievementsManager> logger, IAchievementsService achievementsService)
    {
        _logger = logger;
        _achievementsService = achievementsService;
    }

    public void Notify(Actions action, Workout payload)
    {
        _logger.LogInformation("Notifying about: {Action} for workout: {Workout}", action.ToString(), payload.Id);
    }
}