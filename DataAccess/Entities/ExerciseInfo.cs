using System.Collections.Generic;

namespace FitAppServer.DataAccess.Entities;

public class ExerciseInfo
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<Exercise> Exercises { get; set; } = null!;
}