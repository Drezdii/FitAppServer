using System.Collections.Generic;

namespace FitAppServer.DataAccess.Entities;

public class Exercise
{
    public int Id { get; set; }
    public ExerciseInfo ExerciseInfo { get; set; } = null!;
    public int ExerciseInfoId { get; set; }
    public IList<Set> Sets { get; set; } = null!;
    public Workout Workout { get; set; } = null!;
    public int WorkoutId { get; set; }
}