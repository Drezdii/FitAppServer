namespace FitAppServer.Services.Models;

public enum AuthErrorCode
{
    GenericError,
    EmailAlreadyExists,
    PasswordsNotEqual,
    InvalidEmail,
    UsernameTooShort,
    PasswordTooShort,
    UsernameAlreadyExists,
    None
}