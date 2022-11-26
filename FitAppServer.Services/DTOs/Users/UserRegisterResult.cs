using FitAppServer.Services.Models;

namespace FitAppServer.Services.DTOs.Users;

public class UserRegisterResult
{
    public string? ID { get; set; }
    public string? SignInToken { get; set; }
    public AuthErrorCode ErrorCode { get; set; }
}