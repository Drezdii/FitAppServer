using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitAppServer.Services;
using FitAppServer.Types;
using FitAppServer.Utils;
using Microsoft.Extensions.Logging;

namespace FitAppServer.Resolvers;

public class AchievementsResolver
{
    private readonly IAchievementsService _achievementsService;
    private readonly IClaimsAccessor _claims;
    private readonly ILogger _logger;

    public AchievementsResolver(IAchievementsService service, ILogger<AchievementsResolver> logger,
        IClaimsAccessor accessor)
    {
        _achievementsService = service;
        _logger = logger;
        _claims = accessor;
    }

    public async Task<ICollection<OneRepMaxType>> GetOneRepMaxes()
    {
        // var userid = _claims.UserId;
        // _logger.LogInformation("Getting one rep maxes for user: {Userid}", userid);
        var userId = "Zf42J6wSkUTJWcBRfdCJCViuVxu1";
        var maxes = await _achievementsService.GetOneRepMaxesByUserId(userId);

        return maxes.Select(q => new OneRepMaxType
        {
            Id = q.Id,
            Value = q.Value,
            // Change to proper value
            BigLiftType = BigLiftType.Bench
        }).ToList();
    }
}