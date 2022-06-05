using FitAppServer.Services;
using FitAppServerREST.DTOs.Creator;
using FitAppServerREST.DTOs.Workouts;
using FitAppServerREST.Mappings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitAppServerREST.Controllers;

[ApiController]
[Route("workouts")]
public class WorkoutsController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IUsersService _usersService;
    private readonly IWorkoutsService _workoutsService;

    public WorkoutsController(IWorkoutsService service, IUsersService usersService, ILogger<WorkoutsController> logger)
    {
        _workoutsService = service;
        _usersService = usersService;
        _logger = logger;
    }

    [HttpGet]
    [Route("user/{userid}")]
    [Authorize]
    public async Task<IActionResult> ByUserId(string userid)
    {
        _logger.LogInformation("Getting workouts for user: {Id}", userid);

        var workouts = await _workoutsService.GetByUserIdAsync(userid);

        return Ok(workouts.Select(q => q.ToDto()));
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddOrUpdateWorkout(WorkoutDto workout)
    {
        // Compare userid to Token userid here
        // if userid == userid from token => continue
        var claimsUserId = User.Claims.Single(q => q.Type == "user_id").Value;

        var wrk = workout.ToModel();

        var user = await _usersService.GetUserAsync(claimsUserId);

        if (user == null) return BadRequest();

        if (user.Uuid != claimsUserId) return Forbid();

        wrk.User = user;

        // Perform these checks only for existing workouts
        if (wrk.Id > 0)
        {
            var existingWorkout = await _workoutsService.GetByWorkoutIdAsync(wrk.Id);

            if (existingWorkout == null) return NotFound();

            // Workout belongs to another user
            if (existingWorkout.User.Uuid != claimsUserId) return Forbid();
        }

        var res = await _workoutsService.AddOrUpdateWorkoutAsync(wrk);

        return Ok(res.ToDto());
    }

    [HttpGet]
    [Route("{workoutid:int}")]
    // [Authorize]
    public async Task<IActionResult> GetWorkout(int workoutid)
    {
        _logger.LogInformation("Getting workout with id: {Id}", workoutid);

        var workout = await _workoutsService.GetByWorkoutIdAsync(workoutid);

        if (workout == null) return NotFound();

        return Ok(workout.ToDto());
    }

    [HttpDelete]
    [Route("{workoutid:int}")]
    public async Task<IActionResult> DeleteWorkout(int workoutid)
    {
        await _workoutsService.DeleteWorkoutAsync(workoutid);

        return Ok();
    }

    [HttpPost]
    [Route("program")]
    public async Task<IActionResult> AddProgramCycle(ProgramCycleDto programCycle)
    {
        var test = new Dictionary<int, List<NewWorkoutDto>>
        {
            {
                1, new List<NewWorkoutDto>
                {
                    new NewWorkoutDto
                    {
                    }
                }
            }
        };
        return Ok(new ProgramCycleDto
        {
            Program = new ProgramDto
            {
                Id = 1
            },
            WorkoutsByWeek = test
        });
    }
}