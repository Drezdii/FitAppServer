using System;

namespace FitAppServer.Services.Models;

public record AllTimeStats(int WorkoutCount, double TotalWorkoutTimeSeconds, float TotalWeightLifted,
    double LongestWorkoutTimeSeconds, float AvgMonthlyWorkouts);