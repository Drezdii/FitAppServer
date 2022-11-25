using FitAppServer.DataAccess.Entities;

namespace FitAppServerREST.Models;

public class ProgramCycle
{
    public WorkoutProgram Program { get; set; } = null!;
    public User User { get; set; } = null!;

    public IReadOnlyDictionary<int, IReadOnlyCollection<Workout>> WorkoutsByWeek { get; set; } =
        new Dictionary<int, IReadOnlyCollection<Workout>>();
}