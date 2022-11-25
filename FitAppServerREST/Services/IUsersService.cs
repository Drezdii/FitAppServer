using FitAppServer.DataAccess.Entities;
using FitAppServerREST.DTOs.Users;

namespace FitAppServerREST.Services;

public interface IUsersService
{
    Task<UserRegisterResult> RegisterUserAsync(NewUser user);
    Task<User?> GetUserAsync(string userid);
    Task<User?> GetByUsernameOrEmailAsync(string username, string email);
}