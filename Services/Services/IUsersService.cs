using System.Threading.Tasks;
using FitAppServer.DataAccess.Entities;
using FitAppServer.Services.DTOs.Users;

namespace FitAppServer.Services.Services;

public interface IUsersService
{
    Task<UserRegisterResult> RegisterUserAsync(NewUser user);
    Task<User?> GetUserAsync(string userid);
    Task<User?> GetByUsernameOrEmailAsync(string username, string email);
}