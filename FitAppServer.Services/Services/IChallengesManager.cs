using System.Threading.Tasks;
using FitAppServer.DataAccess.Entities;
using FitAppServer.Services.Models;

namespace FitAppServer.Services.Services;

public interface IChallengesManager
{
    public Task Notify(WorkoutAction action, Workout payload);
}