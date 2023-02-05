﻿using System.Collections.Generic;

namespace FitAppServerREST.DTOs.Creator;

public class ProgramCycleDto
{
    public WorkoutProgramDetailsDto WorkoutProgramDetails { get; set; } = null!;
    public Dictionary<int, List<NewWorkoutDto>> WorkoutsByWeek { get; set; } = new();
}