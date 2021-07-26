using FitAppServer.DataAccess.Entites;
using FitAppServer.Services.DTOs.Users;
using System.Threading.Tasks;

namespace FitAppServer.Services
{
    public interface IUsersService
    {
        Task<UserRegisterResult> RegisterUserAsync(NewUser user);
        Task<User> GetUser(string userid);
    }
}
