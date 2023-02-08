using System.Collections;
using System.Collections.Generic;

namespace FitAppServer.DataAccess.Entities;

public class Exercise
{
    public int Id { get; set; }
    public ExerciseInfo ExerciseInfo { get; set; } = null!;
    public int ExerciseInfoId { get; set; }
    public ICollection<Set> Sets { get; set; } = new List<Set>();
    public Workout Workout { get; set; } = null!;
    public int WorkoutId { get; set; }
}