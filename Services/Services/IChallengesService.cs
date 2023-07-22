﻿using System.Collections.Generic;
using System.Threading.Tasks;
using FitAppServer.DataAccess.Entities;

namespace FitAppServer.Services.Services;

public interface IChallengesService
{
    Task<ICollection<OneRepMax>> GetOneRepMaxesByUserId(string userid);
    Task<ICollection<ChallengeEntry>> GetChallengesEntriesByUserId(string userid);
    Task<ICollection<ChallengeEntry>> GetTop3Challenges(string userid);
}