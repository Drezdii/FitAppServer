﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitAppServer.DataAccess.Entities;

public class User
{
    public int Id { get; set; }
    [Required] public string Uuid { get; set; } = null!;

    [Required] public string Username { get; set; } = null!;

    [Required] public string Email { get; set; } = null!;

    public ICollection<Workout> Workouts { get; set; } = null!;

    public ICollection<OneRepMax> Maxes { get; set; } = null!;
    public ICollection<ChallengeEntry> ChallengeEntries { get; set; } = null!;
    public ICollection<BodyWeightEntry> BodyWeights { get; set; } = null!;
}