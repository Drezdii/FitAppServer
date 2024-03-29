﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitAppServer.DataAccess.Entities;
using FitAppServer.Services.Models;
using FitAppServer.Services.Services;
using Backend.DTOs.Creator;
using Backend.DTOs.Workouts;
using Backend.Mappings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Backend.Controllers;

[ApiController]
[Route("workouts")]
public class WorkoutsController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IUsersService _usersService;
    private readonly IWorkoutsService _workoutsService;
    private readonly IChallengesManager _challengesManager;

    public WorkoutsController(IWorkoutsService service, IUsersService usersService, ILogger<WorkoutsController> logger,
        IChallengesManager challengesManager)
    {
        _workoutsService = service;
        _usersService = usersService;
        _logger = logger;
        _challengesManager = challengesManager;
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

        if (userid != claimsUserId)
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
        var claimsUserId = User.Claims.Single(q => q.Type == "user_id").Value;

        var wrk = workout.ToModel();

        var user = await _usersService.GetUserAsync(claimsUserId);
        var isNewWorkout = true;

        if (user == null)
        {
            return BadRequest();
        }

        wrk.User = user;

        // Perform these checks only for existing workouts
        if (wrk.Id > 0)
        {
            isNewWorkout = false;
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

        // Update challenges only for finished workouts
        if (res.StartDate is not null && res.EndDate is not null)
        {
            await _challengesManager.Notify(isNewWorkout ? WorkoutAction.Created : WorkoutAction.Updated, wrk);
        }

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
        await _challengesManager.Notify(WorkoutAction.Deleted, existingWorkout);

        _logger.LogInformation("Deleted workout: {Id}", workoutid);

        return Ok();
    }

    [HttpPost]
    [Route("program")]
    [Authorize]
    public async Task<IActionResult> AddProgramCycle(ProgramCycleDto programCycle)
    {
        var claimsUserId = User.Claims.Single(q => q.Type == "user_id").Value;

        _logger.LogInformation("Adding program to user: {UserId}", claimsUserId);

        var user = await _usersService.GetUserAsync(claimsUserId);

        if (user == null)
        {
            return BadRequest();
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

        // Map the dictionary to a dictionary of DTOs
        var mappedResult = res.Select(q => q)
            .ToDictionary(q => q.Key,
                q => q.Value.Select(a => a.ToDto()).ToList());


        return Ok(new {workoutsByWeek = mappedResult});
    }
}