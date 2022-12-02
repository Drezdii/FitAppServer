using FitAppServer.DataAccess.Entities;
using FitAppServer.Services.Models;

namespace FitAppServer.Services.Services.Challenges;

public interface IChallenge
{
    Task Check(WorkoutAction action, Workout payload);

    // Use those identifiers when setting up the challenges in database when creating a new user
    string GetId();
}