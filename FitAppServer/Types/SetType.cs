namespace FitAppServer.Types;

public class SetType
{
    public int Id { get; set; }
    public int Reps { get; set; }
    public float Weight { get; set; }
    public bool Completed { get; set; }
    public int ExerciseId { get; set; }
}