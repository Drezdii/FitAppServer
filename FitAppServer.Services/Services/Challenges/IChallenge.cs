using FitAppServer.DataAccess.Entities;
using FitAppServer.Services.Models;

namespace FitAppServer.Services.Services.Challenges;

public interface IChallenge
{
    void Check(WorkoutAction action, Workout payload);
    string GetId();
}