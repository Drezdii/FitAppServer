using System;
using System.Collections.Generic;
using FitAppServer.DataAccess.Entities;

namespace FitAppServer.Types;

public record WorkoutInput(int Id, DateOnly Date, DateTime? StartDate, DateTime? EndDate, WorkoutTypeCode Type,
    IReadOnlyCollection<ExerciseInput> Exercises);