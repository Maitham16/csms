// User model
// By Maitham Al-rubaye

using System.ComponentModel.DataAnnotations;

namespace UserService.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Email must be between 2 and 50 characters")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Password must be between 2 and 50 characters")]
        public string? Password { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "Phone number must be between 2 and 50 characters")]
        public string? PhoneNumber { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "Address must be between 2 and 50 characters")]
        public string? Address { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "City must be between 2 and 50 characters")]
        public string? City { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "Zip code must be between 2 and 50 characters")]
        public string? ZipCode { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Country must be between 2 and 50 characters")]
        public string? Country { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Role must be between 2 and 50 characters")]
        public string? Role { get; set; }

    }
}

