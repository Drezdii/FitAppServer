using System.Collections.Generic;

namespace FitAppServer.DataAccess.Entities;

public class WorkoutProgram
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<Workout> Workouts { get; set; } = new List<Workout>();
}