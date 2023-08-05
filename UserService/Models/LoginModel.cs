// log in model
// By Maitham Al-rubaye

using System.ComponentModel.DataAnnotations;

namespace UserService.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, ErrorMessage = "Password must be between 6 and 20 characters", MinimumLength = 6)]
        public string? Password { get; set; }
    }
}