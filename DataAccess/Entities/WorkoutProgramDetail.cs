using System.Collections.Generic;

namespace FitAppServer.DataAccess.Entities;

public class WorkoutProgramDetail
{
    public int Id { get; set; }
    public WorkoutProgram Program { get; set; } = null!;
    public int Cycle { get; set; }
    public int Week { get; set; }
    public ICollection<Workout> Workouts { get; set; } = new List<Workout>();
}