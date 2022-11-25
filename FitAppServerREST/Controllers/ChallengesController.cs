using FitAppServerREST.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitAppServerREST.Controllers;

[ApiController]
[Route("challenges")]
public class ChallengesController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IUsersService _usersService;
    private readonly IChallengesService _challengesService;

    public ChallengesController(IChallengesService challengesService, IUsersService usersService,
        ILogger<ChallengesController> logger)
    {
        _challengesService = challengesService;
        _usersService = usersService;
        _logger = logger;
    }

    [HttpGet]
    [Route("onerepmaxes/{userid}")]
    [Authorize]
    public async Task<IActionResult> GetOneRepMax(string userid)
    {
        var claimsUserId = User.Claims.Single(q => q.Type == "user_id").Value;

        var user = await _usersService.GetUserAsync(claimsUserId);

        if (user == null)
        {
            return BadRequest();
        }

        if (user.Uuid != claimsUserId)
        {
            return Forbid();
        }

        var maxes = _challengesService.GetOneRepMaxesByUserId(userid);
        return Ok(maxes);
    }

    [HttpGet]
    [Route("challenges/{userid}")]
    [Authorize]
    public async Task<IActionResult> GetChallengesByUserId(string userid)
    {
        var claimsUserId = User.Claims.Single(q => q.Type == "user_id").Value;

        var user = await _usersService.GetUserAsync(claimsUserId);

        if (user == null)
        {
            return BadRequest();
        }

        if (user.Uuid != claimsUserId)
        {
            return Forbid();
        }

        var challenges = _challengesService.GetChallengesByUserId(userid);
        return Ok(challenges);
    }
}