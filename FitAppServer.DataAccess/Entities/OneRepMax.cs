using System;

namespace FitAppServer.DataAccess.Entities;

public class OneRepMax
{
    public int Id { get; set; }
    public float Value { get; set; }
    public BigLiftType Lift { get; set; }
    public Workout Workout { get; set; } = null!;
    public int WorkoutId { get; set; }
}