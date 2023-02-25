using System;

namespace Backend.DTOs.Challenges;

public record ChallengeDto(string NameTranslationKey, string? DescriptionTranslationKey, DateTime StartDate, DateTime? EndDate, float Goal, string? Unit);