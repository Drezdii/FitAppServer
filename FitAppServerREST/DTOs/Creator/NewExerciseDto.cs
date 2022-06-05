using FitAppServer.DataAccess.Entities;

namespace FitAppServerREST.DTOs.Creator;

public class NewExerciseDto
{
    public WorkoutTypeCode ExerciseType { get; set; }
    public List<NewSetDto> Sets { get; set; } = new();
}