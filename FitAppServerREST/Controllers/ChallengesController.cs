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

    public ChallengesController(IWorkoutsService service, IUsersService usersService,
        ILogger<ChallengesController> logger)
    {
        _usersService = usersService;
        _logger = logger;
    }

    [HttpGet]
    [Route("onerepmaxes")]
    [Authorize]
    public async Task<IActionResult> GetOneRepMax()
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

        return Ok();
    }
}