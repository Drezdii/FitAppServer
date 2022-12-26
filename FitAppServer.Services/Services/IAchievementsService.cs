using FitAppServer.DataAccess.Entities;

namespace FitAppServer.Services.Services;

public interface IAchievementsService
{
    public Task<ICollection<OneRepMax>> GetOneRepMaxesByUserId(string userId);
}