using System;
using System.Collections.Generic;
using FitAppServer.DataAccess.Entities;
using Backend.DTOs.Creator;

namespace Backend.DTOs.Workouts;

public class WorkoutDto
{
    public int Id { get; set; }
    public DateOnly? Date { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public WorkoutTypeCode Type { get; set; } = WorkoutTypeCode.None;
    public List<ExerciseDto> Exercises { get; set; } = new();
    public WorkoutProgramDetailsDto? WorkoutProgramDetails { get; set; }
}