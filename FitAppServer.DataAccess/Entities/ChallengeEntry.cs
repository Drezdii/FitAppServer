using System;

namespace FitAppServer.DataAccess.Entities;

public class ChallengeEntry
{
    public int Id { get; set; }
    public float Value { get; set; }
    public User User { get; set; } = null!;
    public int UserId { get; set; }
    public Challenge Challenge { get; set; } = null!;
    public int ChallengeId { get; set; }
    public DateOnly? CompletedAt { get; set; }
}