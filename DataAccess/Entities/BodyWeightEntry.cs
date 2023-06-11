using System;

namespace FitAppServer.DataAccess.Entities;

public class BodyWeightEntry
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public float Weight { get; set; }
    public User User { get; set; } = null!;
    public int UserId { get; set; }
}