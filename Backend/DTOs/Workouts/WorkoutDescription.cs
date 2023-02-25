using System;

namespace Backend.DTOs.Workouts;

public class WorkoutDescription
{
    public int Id { get; set; }
    public DateOnly? Date { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int Type { get; set; }
}