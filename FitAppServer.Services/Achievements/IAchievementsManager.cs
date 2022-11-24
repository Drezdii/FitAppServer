using System.Threading.Tasks;
using FitAppServer.DataAccess.Entities;

namespace FitAppServer.Services.Achievements;

public interface IAchievementsManager
{
    public Task Notify(Action action, Workout workout);
}