using FitAppServerREST.Models;

namespace FitAppServerREST.DTOs.Users;

public class UserRegisterResult
{
    public string? ID { get; set; }
    public string? SignInToken { get; set; }
    public AuthErrorCode ErrorCode { get; set; }
}