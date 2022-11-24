namespace FitAppServer.DataAccess.Entities;

public class OneRepMax
{
    public int Id { get; set; }
    public float Value { get; set; }
    public User User { get; set; } = null!;
    public int UserId { get; set; }
    public Set Set { get; set; } = null!;
    public int SetId { get; set; }

    public ExerciseInfo ExerciseInfo { get; set; } = null!;
    public int ExerciseInfoId { get; set; }
}