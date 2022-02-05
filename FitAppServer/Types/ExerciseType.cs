namespace FitAppServer.Types;

public class ExerciseType
{
    public int Id { get; set; }

    // public ExerciseInfo ExerciseInfo { get; set; } = null!;
    public int ExerciseInfoId { get; set; }

    public int WorkoutId { get; set; }
    // public List<Set> Sets { get; set; } = null!;
}