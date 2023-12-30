using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitAppServer.DataAccess.Entities;

namespace FitAppServer.Services.Services;

public interface IStatsService
{
    Task<BodyWeightEntry?> GetLatestBodyWeightEntry(string userId);
    Task<ICollection<BodyWeightEntry>> GetAllBodyWeightEntries(string userId);
    Task<BodyWeightEntry> AddBodyWeightEntry(BodyWeightEntry bw);
}