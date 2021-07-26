using System.ComponentModel.DataAnnotations;

namespace FitAppServer.Models.Auth
{
    public class UserRegister
    {
        [Required]
        [StringLength(36, MinimumLength = 3)]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(999, MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }
    }
}
