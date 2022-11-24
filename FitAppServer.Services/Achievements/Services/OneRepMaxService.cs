using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitAppServer.DataAccess;
using FitAppServer.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitAppServer.Services.Achievements.Services;

public class OneRepMaxService
{
    private readonly FitAppContext _context;

    public OneRepMaxService(FitAppContext context)
    {
        _context = context;
    }

    public async Task<bool> OnWorkoutChanged(Workout workout)
    {
        var oneRepMaxes = await _context.OneRepMaxes
            .Include(q => q.ExerciseInfo)
            .GroupBy(p => p.ExerciseInfo.Id)
            .Select(g => g.OrderByDescending(p => p.Set.Exercise.Workout.Date).ThenByDescending(p => p.Id)
                .First()
            ).ToDictionaryAsync(q => q.ExerciseInfo.Id);

        // Group sets into groups of <ExerciseInfoId, List<Set>>
        var setsGroups = workout.Exercises
            .GroupBy(q => q.ExerciseInfoId)
            .ToLookup(group => group.Key, group => group.Select(ex => ex.Sets).SelectMany(sets => sets)
                .ToList());

        // var maxes = setsGroups.ToLookup(group => group.Key,
        //     group => group.SelectMany(sets => sets).MaxBy(CalculateOneRepMax));

        var newMaxes = new List<OneRepMax>();

        foreach (var group in setsGroups)
        {
            // Get one rep max from the set with the highest one rep max
            var newMax = GetOneRepMaxFromSet(group.SelectMany(q => q)
                // Always get the last set in case of multiple sets having the same 1RM
                .OrderByDescending(q => q.Id)
                .MaxBy(CalculateOneRepMax));

            newMax.User = workout.User;
            newMax.ExerciseInfoId = group.Key;

            if (oneRepMaxes.ContainsKey(group.Key))
            {
                var currentMax = oneRepMaxes[group.Key];

                if (newMax.Value > currentMax.Value)
                {
                    newMaxes.Add(newMax);
                }
            }
            else
            {
                newMaxes.Add(newMax);
            }
        }

        _context.OneRepMaxes.AddRange(newMaxes);

        // 1RM = weight * (1 + (number of reps/ 30))
        return true;
    }

    private OneRepMax GetOneRepMaxFromSet(Set set)
    {
        return new OneRepMax
        {
            Set = set,
            Value = CalculateOneRepMax(set),
        };
    }

    private int CalculateOneRepMax(Set set)
    {
        return (int) Math.Round(set.Weight / (1.0278 - (0.0278 * set.Reps)), 0);
    }
}