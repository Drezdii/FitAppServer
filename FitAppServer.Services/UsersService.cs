using FirebaseAdmin.Auth;
using FitAppServer.DataAccess;
using FitAppServer.DataAccess.Entites;
using FitAppServer.Services.DTOs.Users;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using FitAppServer.Services.Models;
using AuthErrorCode = FitAppServer.Services.Models.AuthErrorCode;

namespace FitAppServer.Services
{
    public class UsersService : IUsersService
    {
        private readonly FitAppContext _context;
        public UsersService(FitAppContext context)
        {
            _context = context;
        }

        public async Task<User> GetUser(string userid)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(q => q.Uuid == userid);
        }

        public async Task<User> GetByUsernameOrEmail(string username, string email)
        {
            return await _context.Users.FirstOrDefaultAsync(q => q.Username == username || q.Email == email);
        }

        public async Task<UserRegisterResult> RegisterUserAsync(NewUser user)
        {
            // New Firebase user
            var newUser = new UserRecordArgs
            {
                DisplayName = user.Username,
                Email = user.Email,
                EmailVerified = true,
                Password = user.Password
            };

            try
            {
                // Save the user to the Firebase Auth DB
                var userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(newUser);

                var dbUser = new User
                {
                    Uuid = userRecord.Uid,
                    Username = userRecord.DisplayName,
                    Email = userRecord.Email
                };

                // Save the user to the DB
                _context.Users.Add(dbUser);
                await _context.SaveChangesAsync();

                var token = await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(userRecord.Uid);

                return new UserRegisterResult
                {
                    ID = userRecord.Uid,
                    SignInToken = token,
                    ErrorCode = AuthErrorCode.None
                };
            }
            catch (FirebaseAuthException)
            {
                return new UserRegisterResult
                {
                    ErrorCode = AuthErrorCode.GenericError
                };
            }
        }
    }
}
