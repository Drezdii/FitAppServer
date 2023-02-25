using System.Collections.Generic;
using FitAppServer.DataAccess.Entities;

namespace Backend.DTOs.Creator;

public class NewExerciseDto
{
    public WorkoutTypeCode ExerciseType { get; set; }
    public List<NewSetDto> Sets { get; set; } = new();
}