using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitAppServer.DataAccess;
using FitAppServer.DataAccess.Entities;
using FitAppServer.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace FitAppServer.Services.Services;

public class ChallengesService : IChallengesService
{
    private readonly FitAppContext _context;

    public ChallengesService(FitAppContext context)
    {
        _context = context;
    }

    public async Task<ICollection<OneRepMax>> GetOneRepMaxesByUserId(string userId)
    {
        // Get one rep max for each big lift based on workout date and the id of the one rep max
        return await _context.OneRepMaxes
            .Where(q => q.User.Uuid == userId)
            .Include(q => q.ExerciseInfo)
            .Include(q => q.Set)
            .ThenInclude(q => q.Exercise)
            .ThenInclude(q => q.Workout)
            .GroupBy(p => p.ExerciseInfo.Id)
            .Select(q => q.OrderByDescending(p => p.Id).First())
            .ToListAsync();
    }

    public async Task<ICollection<ChallengeEntry>> GetChallengesEntriesByUserId(string userid)
    {
        return await _context.ChallengeEntries.Where(q => q.User.Uuid == userid)
            .Include(q => q.Challenge)
            .ToListAsync();
    }

    public async Task<ICollection<ChallengeEntry>> GetTop3Challenges(string userid)
    {
        return await _context.ChallengeEntries
            .Where(q => q.User.Uuid == userid && q.CompletedAt == null)
            .Include(q => q.Challenge)
            .OrderByDescending(q => (float) q.Value / q.Challenge.Goal)
            .Take(3)
            .ToListAsync();
    }

    public async Task<AllTimeStats> GetAllTimeStats(string userid)
    {
        var allWorkouts = await _context.Workouts
            .Where(q => q.User.Uuid == userid)
            .Where(q => q.StartDate != null && q.EndDate != null && q.Date != null)
            .Include(q => q.Exercises)
            .ThenInclude(q => q.Sets)
            .AsSplitQuery()
            .ToListAsync();

        var totalWorkoutTime = allWorkouts.Sum(q => (q.EndDate!.Value - q.StartDate!.Value).TotalSeconds);

        var totalWeightLifted = allWorkouts
            .SelectMany(q => q.Exercises.SelectMany(w => w.Sets))
            .Sum(s => s.Reps * s.Weight);

        var longestWorkout = allWorkouts.MaxBy(q => q.EndDate - q.StartDate);

        var longestWorkoutTime = longestWorkout == null
            ? 0
            : (longestWorkout.EndDate!.Value - longestWorkout.StartDate!.Value).TotalSeconds;

        var avgMonthlyWorkouts = (float) allWorkouts.Count / allWorkouts
            .GroupBy(q => new {q.Date!.Value.Month, q.Date!.Value.Year}).Count();

        return new AllTimeStats(allWorkouts.Count, totalWorkoutTime, totalWeightLifted,
            longestWorkoutTime, avgMonthlyWorkouts);
    }
}