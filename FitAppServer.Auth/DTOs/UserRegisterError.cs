using FitAppServer.Services.Models;

namespace FitAppServer.Auth.DTOs
{
    public class UserRegisterError
    {
        public string? FieldName { get; set; }
        public string? ErrorMessage { get; set; }
        public AuthErrorCode ErrorCode { get; set; }
    }
}