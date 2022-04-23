using System;
using System.Linq;
using FitAppServer.DataAccess.Entities;
using Microsoft.Extensions.Logging;

namespace FitAppServer.Services;

public class AchievementsManager : IAchievementsManager
{
    private readonly ILogger _logger;
    private readonly IAchievementsService _achievementsService;

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