using FitAppServer.Models.Auth;
using FitAppServer.Services.DTOs.Users;
using FitAppServer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using FitAppServer.DTOs.Auth;

namespace FitAppServer.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _service;
        public UsersController(IUsersService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("", Name = "GetUser")]
        public async Task<IActionResult> GetUserAsync(int id)
        {
            return Ok();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync(UserRegister user)
        {
            var usr = new NewUser
            {
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                PasswordConfirm = user.PasswordConfirm
            };

            var res = await _service.RegisterUserAsync(usr);

            switch (res.ErrorCode)
            {
                case AuthErrorCode.EmailAlreadyExists:
                    var result = new UserRegisterError
                    {
                        ErrorMessage = "User already exists.",
                        ErrorCode = res.ErrorCode
                    };
                    return BadRequest(new { errors = result });
            }


            return CreatedAtRoute("GetUser", new { id = res.ID }, new { res.ID, res.SignInToken });
        }
    }
}