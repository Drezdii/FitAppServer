using System;
using System.Collections.Generic;

namespace FitAppServer.DataAccess.Entities;

public class Challenge
{
    public string Id { get; set; } = null!;
    public string NameTranslationKey { get; set; } = null!;
    public string? DescriptionTranslationKey { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public float Goal { get; set; }
    public string? Unit { get; set; }
    public ICollection<ChallengeEntry> ChallengeEntries { get; set; } = null!;
}