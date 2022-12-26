namespace FitAppServerREST.DTOs.Challenges;

public record ChallengeEntryDto(float Value, string ChallengeId, DateOnly? CompletedAt, ChallengeDto Challenge);