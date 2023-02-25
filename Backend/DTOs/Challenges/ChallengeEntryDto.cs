using System;

namespace Backend.DTOs.Challenges;

public record ChallengeEntryDto(float Value, string ChallengeId, DateOnly? CompletedAt,
    TranslatedChallengeDto Challenge);