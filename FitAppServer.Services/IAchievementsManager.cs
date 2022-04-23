using System;
using FitAppServer.DataAccess.Entities;

namespace FitAppServer.Services;

public interface IAchievementsManager
{
    public void Notify(Actions action, Workout payload);
}