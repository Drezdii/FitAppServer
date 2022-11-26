using FitAppServer.DataAccess.Entities;

namespace FitAppServer.Services.Services;

public interface IChallengesService
{
    Task<ICollection<OneRepMax>> GetOneRepMaxesByUserId(string userid);
    List<ChallengeEntry> GetChallengesByUserId(string userid);
}