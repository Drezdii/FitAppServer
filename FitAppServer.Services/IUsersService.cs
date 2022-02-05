﻿using FitAppServer.DataAccess.Entities;
using FitAppServer.Services.DTOs.Users;
using System.Threading.Tasks;

namespace FitAppServer.Services
{
    public interface IUsersService
    {
        Task<UserRegisterResult> RegisterUserAsync(NewUser user);
        Task<User?> GetUser(string userid);
        Task<User?> GetByUsernameOrEmail(string username, string email);
    }
}
