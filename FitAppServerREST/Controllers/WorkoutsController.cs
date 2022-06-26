using FitAppServer.DataAccess.Entities;
using FitAppServer.Services;
using FitAppServer.Services.Models;
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

        if (user == null)
        {
            return BadRequest();
        }

        if (user.Uuid != claimsUserId)
        {
            return Forbid();
        }

        wrk.User = user;

        // Perform these checks only for existing workouts
        if (wrk.Id > 0)
        {
            var existingWorkout = await _workoutsService.GetByWorkoutIdAsync(wrk.Id);

            if (existingWorkout == null)
            {
                return NotFound();
            }

            // Workout belongs to another user
            if (existingWorkout.User.Uuid != claimsUserId)
            {
                return Forbid();
            }
        }

        var res = await _workoutsService.AddOrUpdateWorkoutAsync(wrk);

        return Ok(res.ToDto());
    }

    [HttpGet]
    [Route("{workoutid:int}")]
    [Authorize]
    public async Task<IActionResult> GetWorkout(int workoutid)
    {
        _logger.LogInformation("Getting workout with id: {Id}", workoutid);

        var claimsUserId = User.Claims.Single(q => q.Type == "user_id").Value;

        var workout = await _workoutsService.GetByWorkoutIdAsync(workoutid);

        if (workout == null)
        {
            return NotFound();
        }

        if (workout.User.Uuid != claimsUserId)
        {
            return Forbid();
        }

        return Ok(workout.ToDto());
    }

    [HttpDelete]
    [Route("{workoutid:int}")]
    [Authorize]
    public async Task<IActionResult> DeleteWorkout(int workoutid)
    {
        // Compare userid to Token userid here
        // if userid == userid from token => continue
        var claimsUserId = User.Claims.Single(q => q.Type == "user_id").Value;

        var existingWorkout = await _workoutsService.GetByWorkoutIdAsync(workoutid);

        if (existingWorkout == null)
        {
            return NotFound();
        }

        // Workout belongs to another user
        if (existingWorkout.User.Uuid != claimsUserId)
        {
            return Forbid();
        }

        await _workoutsService.DeleteWorkoutAsync(workoutid);

        return Ok();
    }

    [HttpPost]
    [Route("program")]
    [Authorize]
    public async Task<IActionResult> AddProgramCycle(ProgramCycleDto programCycle)
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

        // Map workouts to model objects
        var workoutsByWeek =
            programCycle.WorkoutsByWeek.ToDictionary(q => q.Key,
                q => (IReadOnlyCollection<Workout>) q.Value.Select(w => w.ToModel()).ToList());


        var cycle = new ProgramCycle
        {
            Program = programCycle.WorkoutProgramDetails.ToModel(),
            WorkoutsByWeek = workoutsByWeek,
            User = user
        };

        var res = await _workoutsService.AddProgramCycle(cycle);

        return Ok(res.ToString());
    }
}