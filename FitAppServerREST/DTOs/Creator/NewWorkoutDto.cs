using FitAppServer.DataAccess.Entities;

namespace FitAppServerREST.DTOs.Creator;

public class NewWorkoutDto
{
    public DateOnly? Date { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public WorkoutTypeCode Type { get; set; } = WorkoutTypeCode.None;
    public List<NewExerciseDto> Exercises { get; set; } = new();
}