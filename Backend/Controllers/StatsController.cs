using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs.Stats;
using FitAppServer.DataAccess.Entities;
using FitAppServer.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("stats")]
public class StatsController : ControllerBase
{
    private readonly IStatsService _statsService;
    private readonly IUsersService _usersService;

    public StatsController(IStatsService statsService, IUsersService usersService)
    {
        _statsService = statsService;
        _usersService = usersService;
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
            return Forbid();
        }

        var res = await _statsService.GetLatestBodyWeight(userid);

        return Ok(new BodyWeightDto(res.Date, res.Weight));
    }

    [HttpPost("bodyweight")]
    [Authorize]
    public async Task<IActionResult> AddBodyWeight(BodyWeightDto entry)
    {
        var claimsUserId = User.Claims.Single(q => q.Type == "user_id").Value;

        var user = await _usersService.GetUserAsync(claimsUserId);

        if (user == null)
        {
            return BadRequest();
        }

        var res = await _statsService.AddBodyWeightEntry(new BodyWeight
        {
            Date = entry.Date,
            Weight = entry.Weight
        });

        return Ok(entry);
    }
}