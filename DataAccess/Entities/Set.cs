namespace FitAppServer.DataAccess.Entities;

public class Set
{
    public int Id { get; set; }
    public int Reps { get; set; }
    public float Weight { get; set; }
    public bool Completed { get; set; }
    public Exercise Exercise { get; set; } = null!;
    public int ExerciseId { get; set; }
}