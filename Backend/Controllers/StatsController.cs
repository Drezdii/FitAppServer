using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs.Stats;
using FitAppServer.DataAccess.Entities;
using FitAppServer.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Backend.Controllers;

[ApiController]
[Route("stats")]
public class StatsController : ControllerBase
{
    private readonly IStatsService _statsService;
    private readonly IUsersService _usersService;
    private readonly ILogger<StatsController> _logger;

    public StatsController(IStatsService statsService, IUsersService usersService, ILogger<StatsController> logger)
    {
        _statsService = statsService;
        _usersService = usersService;
        _logger = logger;
    }

    [HttpGet("bodyweight/{userid}")]
    [Authorize]
    public async Task<IActionResult> GetLatestBodyWeight(string userid)
    {
        var claimsUserId = User.Claims.Single(q => q.Type == "user_id").Value;

        var user = await _usersService.GetUserAsync(claimsUserId);

        if (user == null)
        {
            return BadRequest();
        }

        if (userid != claimsUserId)
        {
            _logger.LogError("{} was trying to access {} user's bodyweight", claimsUserId, userid);
            return Forbid();
        }

        var res = await _statsService.GetLatestBodyWeightEntry(userid);

        if (res == null)
        {
            return Ok(null);
        }

        return Ok(new BodyWeightEntryDto(res.Id, res.Date, res.Weight));
    }

    [HttpPost("bodyweight")]
    [Authorize]
    public async Task<IActionResult> AddBodyWeightEntry(BodyWeightEntryDto entry)
    {
        var claimsUserId = User.Claims.Single(q => q.Type == "user_id").Value;

        var user = await _usersService.GetUserAsync(claimsUserId);

        if (user == null)
        {
            return BadRequest();
        }

        var res = await _statsService.AddBodyWeightEntry(new BodyWeightEntry
        {
            Date = entry.Date,
            Weight = entry.Weight,
            UserId = user.Id
        });

        return Ok(new BodyWeightEntryDto(res.Id, res.Date, res.Weight));
    }
}