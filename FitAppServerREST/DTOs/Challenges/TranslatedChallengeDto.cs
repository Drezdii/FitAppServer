using System;

namespace FitAppServerREST.DTOs.Challenges;

public record TranslatedChallengeDto(string Name, string? Description, DateTime StartDate, DateTime? EndDate,
    float Goal, string? Unit);