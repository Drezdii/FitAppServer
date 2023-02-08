namespace FitAppServerREST.DTOs.Workouts;

public class SetDto
{
    public int Id { get; set; }
    public int Reps { get; set; }
    public float Weight { get; set; }
    public bool Completed { get; set; }
}