using System.Collections.Generic;
using FitAppServer.DataAccess.Entities;

namespace Backend.DTOs.Workouts;

public class ExerciseDto
{
    public int Id { get; set; }
    public WorkoutTypeCode ExerciseType { get; set; }
    public List<SetDto> Sets { get; set; } = new();
}