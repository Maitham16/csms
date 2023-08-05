// User model
// By Maitham Al-rubaye

using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace UserService.Models
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(20, ErrorMessage = "First name must be between 2 and 20 characters", MinimumLength = 2)]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(20, ErrorMessage = "Last name must be between 2 and 20 characters", MinimumLength = 2)]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Birthdate is required")]
        public DateTime? Birthdate { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(50, ErrorMessage = "Address must be between 2 and 50 characters", MinimumLength = 2)]
        public string? Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(20, ErrorMessage = "City must be between 2 and 20 characters", MinimumLength = 2)]
        public string? City { get; set; }

        [Required(ErrorMessage = "Zip code is required")]
        [StringLength(10, ErrorMessage = "Zip code must be between 2 and 10 characters", MinimumLength = 2)]
        public string? ZipCode { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [StringLength(20, ErrorMessage = "Country must be between 2 and 20 characters", MinimumLength = 2)]
        public string? Country { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [StringLength(20, ErrorMessage = "Role must be between 2 and 20 characters", MinimumLength = 2)]
        public string? Role { get; set; }
    }
}