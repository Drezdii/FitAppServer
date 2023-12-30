using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitAppServer.DataAccess;
using FitAppServer.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FitAppServer.Services.Services;

public class StatsService : IStatsService
{
    private readonly FitAppContext _context;
    private readonly ILogger<StatsService> _logger;

    public StatsService(FitAppContext context, ILogger<StatsService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public Task<BodyWeightEntry?> GetLatestBodyWeightEntry(string userId)
    {
        return _context.BodyWeightEntries
            .Where(q => q.User.Uuid == userId)
            .OrderByDescending(q => q.Date)
            .ThenByDescending(q => q.Id)
            .FirstOrDefaultAsync();
    }

    public async Task<ICollection<BodyWeightEntry>> GetAllBodyWeightEntries(string userId)
    {
        return await _context.BodyWeightEntries.Where(q => q.User.Uuid == userId).ToListAsync();
    }

    public async Task<BodyWeightEntry> AddBodyWeightEntry(BodyWeightEntry bw)
    {
        _context.BodyWeightEntries.Add(bw);
        await _context.SaveChangesAsync();

        return bw;
    }
}