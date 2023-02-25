using System;

namespace Backend.DTOs.Challenges;

public record TranslatedChallengeDto(string Name, string? Description, DateTime StartDate, DateTime? EndDate,
    float Goal, string? Unit);