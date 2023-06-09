using System.Threading.Tasks;
using FitAppServer.DataAccess.Entities;

namespace FitAppServer.Services.Services;

public class StatsService : IStatsService
{
    public Task<BodyWeight> GetLatestBodyWeight(string userId)
    {
        throw new System.NotImplementedException();
    }

    public Task<BodyWeight> AddBodyWeightEntry(BodyWeight bw)
    {
        throw new System.NotImplementedException();
    }
}