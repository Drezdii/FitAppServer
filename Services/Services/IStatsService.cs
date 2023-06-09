using System.Threading.Tasks;
using FitAppServer.DataAccess.Entities;

namespace FitAppServer.Services.Services;

public interface IStatsService
{
    Task<BodyWeight> GetLatestBodyWeight(string userId);
    Task<BodyWeight> AddBodyWeightEntry(BodyWeight bw);
}