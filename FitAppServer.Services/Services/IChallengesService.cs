using FitAppServer.DataAccess.Entities;

namespace FitAppServer.Services.Services;

public interface IChallengesService
{
    Task<ICollection<OneRepMax>> GetOneRepMaxesByUserId(string userid);
    Task<ICollection<ChallengeEntry>> GetChallengesEntriesByUserId(string userid);
}