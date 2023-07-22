using System.Linq;
using System.Threading.Tasks;
using FitAppServer.Services.Services;
using Backend.DTOs.Challenges;
using FitAppServer.DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Backend.Controllers;

[ApiController]
[Route("challenges")]
public class ChallengesController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IUsersService _usersService;
    private readonly IChallengesService _challengesService;
    private readonly IStringLocalizer<ChallengesController> _localizer;

    public ChallengesController(IChallengesService challengesService, IUsersService usersService,
        ILogger<ChallengesController> logger, IStringLocalizer<ChallengesController> localizer)
    {
        _challengesService = challengesService;
        _usersService = usersService;
        _logger = logger;
        _localizer = localizer;
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

        if (userid != claimsUserId)
        {
            return Forbid();
        }

        var maxes = await _challengesService.GetOneRepMaxesByUserId(userid);

        return Ok(maxes.Select(max => new
        {
            ExerciseType = (WorkoutTypeCode) max.ExerciseInfoId,
            max.Value,
            max.Set.Exercise.Workout.Date
        }));
    }

    [HttpGet]
    [Route("user/{userid}")]
    [Authorize]
    public async Task<IActionResult> GetChallengesByUserId(string userid)
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

        var challenges = await _challengesService.GetChallengesEntriesByUserId(userid);

        var translatedChallenges = challenges.Select(entry =>
        {
            var challenge = new TranslatedChallengeDto(_localizer[entry.Challenge.NameTranslationKey],
                _localizer[entry.Challenge.DescriptionTranslationKey ?? ""], entry.Challenge.StartDate,
                entry.Challenge.EndDate, entry.Challenge.Goal, entry.Challenge.Unit);

            return new ChallengeEntryDto(entry.Value, entry.ChallengeId, entry.CompletedAt, challenge);
        }).ToList();

        return Ok(translatedChallenges);
    }
}