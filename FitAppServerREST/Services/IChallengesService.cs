using FitAppServer.DataAccess.Entities;

namespace FitAppServerREST.Services;

public interface IChallengesService
{
    List<OneRepMax> GetOneRepMaxesByUserId(string userid);
    List<ChallengeEntry> GetChallengesByUserId(string userid);
}