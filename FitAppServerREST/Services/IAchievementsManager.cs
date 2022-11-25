using FitAppServer.DataAccess.Entities;

namespace FitAppServerREST.Services;

public interface IAchievementsManager
{
    public void Notify(Actions action, Workout payload);
}