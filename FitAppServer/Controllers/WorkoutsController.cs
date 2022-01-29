using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using FitAppServer.Services;
using System.Threading.Tasks;
using FitAppServer.DTOs.Workouts;
using FitAppServer.Mappings;
using Microsoft.AspNetCore.Authorization;

namespace FitAppServer.Controllers
{
    [ApiController]
    [Route("workouts")]
    public class WorkoutsController : ControllerBase
    {
        private readonly IWorkoutsService _service;
        private readonly IUsersService _usersService;
        public WorkoutsController(IWorkoutsService service, IUsersService usersService)
        {
            _service = service;
            _usersService = usersService;
        }

        [HttpGet]
        [Route("descriptions/{userid}")]
        [Authorize]
        public async Task<IActionResult> GetByUserId(string userid)
        {
            var workouts = await _service.GetByUserIDAsync(userid);

            var result = new List<WorkoutDescription>();

            foreach (var workout in workouts)
            {
                result.Add(new WorkoutDescription
                {
                    ID = workout.ID,
                    Date = workout.Date,
                    StartDate = workout.StartDate,
                    EndDate = workout.EndDate,
                    Type = (int)workout.Type
                });
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("user")]
        [Authorize]
        public async Task<IActionResult> AddOrUpdateWorkout(WorkoutDTO workout)
        {
            // Compare userid to Token userid here
            // if userid == userid from token => continue
            var claimsUserId = User.Claims.Single(q => q.Type == "user_id").Value;

            var wrk = workout.FromDTO();
            var user = await _usersService.GetUser(claimsUserId);

            if (user == null)
            {
                return BadRequest("Could not find the user");
            }

            wrk.User = user;

            // Only check if this workout is being updated and not added
            if (wrk.ID > 0)
            {
                var existingWorkout = await _service.GetByWorkoutIDAsync(wrk.ID);

                if (existingWorkout == null)
                {
                    return NotFound();
                }

                // Check if the Workout belongs to the sender
                if (existingWorkout.User.Uuid == user.Uuid)
                {
                    // Check if each exercise being updated belongs to this workout
                    foreach (var ex in wrk.Exercises)
                    {
                        // Don't check if this exercise is being added
                        if (ex.ID <= 0)
                        {
                            continue;
                        }

                        var existingExercise = existingWorkout.Exercises.FirstOrDefault(q => q.ID == ex.ID);

                        if (existingExercise == null)
                        {
                            return Unauthorized();
                        }

                        // Check if each set being updated belongs to this exercise
                        foreach (var set in ex.Sets)
                        {
                            // Don't check when this set is being added
                            if (set.ID <= 0)
                            {
                                continue;
                            }

                            if (existingExercise.Sets.FirstOrDefault(q => q.ID == set.ID) == null)
                            {
                                return Unauthorized();
                            }
                        }
                    }
                }
            }

            var res = await _service.AddOrUpdateWorkout(wrk);
            return Ok(res.ToDTO());
        }

        [HttpGet]
        [Route("{workoutid}")]
        [Authorize]
        public async Task<IActionResult> GetWorkout(int workoutid)
        {
            var workout = await _service.GetByWorkoutIDAsync(workoutid);

            if (workout == null)
            {
                return NotFound();
            }

            return Ok(workout.ToDTO());
        }

        [HttpDelete]
        [Route("{workoutid}")]
        public async Task<IActionResult> DeleteWorkout(int workoutid)
        {
            await _service.DeleteWorkout(workoutid);

            return Ok();
        }
    }
}
