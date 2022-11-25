using FitAppServer.DataAccess.Entities;

namespace FitAppServerREST.Services;

public interface IAchievementsService
{
    public Task<ICollection<OneRepMax>> GetOneRepMaxesByUserId(string userId);
}