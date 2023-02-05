using System;
using Microsoft.EntityFrameworkCore;

namespace FitAppServer.DataAccess.Entities;

[PrimaryKey(nameof(UserId), nameof(ChallengeId))]
public class ChallengeEntry
{
    public float Value { get; set; }
    public User User { get; set; } = null!;
    public int UserId { get; set; }
    public Challenge Challenge { get; set; } = null!;
    public string ChallengeId { get; set; } = null!;
    public DateOnly? CompletedAt { get; set; }
}