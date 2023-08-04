// login view model
// By Maitham Al-rubaye

using System.ComponentModel.DataAnnotations;

namespace UserService.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
