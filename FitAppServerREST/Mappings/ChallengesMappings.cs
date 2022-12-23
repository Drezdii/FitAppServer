using FitAppServer.DataAccess.Entities;
using FitAppServerREST.DTOs.Challenges;

namespace FitAppServerREST.Mappings;

public static class ChallengesMappings
{
    public static ChallengeEntryDto ToDto(this ChallengeEntry entry)
    {
        return new ChallengeEntryDto
        (
            entry.Value, entry.ChallengeId, entry.CompletedAt, ChallengeToDto(entry.Challenge)
        );
    }

    private static ChallengeDto ChallengeToDto(Challenge challenge)
    {
        return new ChallengeDto(challenge.NameTranslationKey, challenge.DescriptionTranslationKey, challenge.StartDate,
            challenge.EndDate, challenge.Goal, challenge.Unit);
    }
}