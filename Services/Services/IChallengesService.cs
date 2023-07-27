using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitAppServer.DataAccess.Entities;
using FitAppServer.Services.Models;

namespace FitAppServer.Services.Services;

public interface IChallengesService
{
    Task<ICollection<OneRepMax>> GetOneRepMaxesByUserId(string userid);
    Task<ICollection<ChallengeEntry>> GetChallengesEntriesByUserId(string userid);
    Task<ICollection<ChallengeEntry>> GetTop3Challenges(string userid);
    Task<AllTimeStats> GetAllTimeStats(string userid);
}