using System;
using System.Threading.Tasks;
using FitAppServer.Services;
using FitAppServer.Types;
using FitAppServer.Utils;
using Microsoft.Extensions.Logging;

namespace FitAppServer.Mutations;

public class WorkoutMutation
{
    private readonly IWorkoutsService _workoutsService;
    private readonly IUsersService _usersService;
    private readonly ILogger<WorkoutMutation> _logger;
    private readonly IAchievementsManager _achievementsManager;
    private readonly IClaimsAccessor _claims;

    public WorkoutMutation(IWorkoutsService workoutsService, IUsersService usersService,
        ILogger<WorkoutMutation> logger,
        IAchievementsManager achievementsManager,
        IClaimsAccessor accessor)
    {
        _workoutsService = workoutsService;
        _usersService = usersService;
        _logger = logger;
        _achievementsManager = achievementsManager;
        _claims = accessor;
    }

    public async Task<int> DeleteWorkout(int id)
    {
        await _workoutsService.DeleteWorkoutAsync(id);
        return id;
    }

    public async Task<WorkoutType> SaveWorkout(WorkoutInput workout)
    {
        var userId = _claims.UserId;

        if (userId == "")
        {
            userId = "Zf42J6wSkUTJWcBRfdCJCViuVxu1";
        }

        var user = await _usersService.GetUserAsync(userId);

        if (user == null)
        {
            _logger.LogError("Cannot find user with provided ID");
            throw new ArgumentException("Cannot find user with provided ID");
        }

        var entity = workout.ToModel();

        entity.User = user;

        var result = await _workoutsService.AddOrUpdateWorkoutAsync(entity);
        _achievementsManager.Notify(Actions.SaveWorkout, result);
        _logger.LogInformation("Saving workout for user: {Id}", userId);
        return result.ToResultType();
    }
}