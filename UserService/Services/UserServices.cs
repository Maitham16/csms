// UserService class
// By Maitham Al-rubaye

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Models;
using UserService.Repositories;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace UserService.Services
{
    public class UserServices : UserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserServices> _logger;

        public UserServices(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration, ILogger<UserServices> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<User> RegisterUser(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return user;
            }
            else
            {
                throw new InvalidOperationException("User creation failed: " + result.Errors.First().Description);
            }
        }

        public async Task<string?> LoginUser(string Email, string Password)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                _logger.LogInformation("User with email {Email} not found.", Email);
                return null;
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, Password, false);
            if (!result.Succeeded)
            {
                _logger.LogInformation("Password check failed for user {Email}. Error: {Error}", Email, result.ToString());
                return null;
            }

            var token = await GenerateJwtToken(user);
            _logger.LogInformation("Token generated for user {Email}: {Token}", Email, token);
            return token;
        }

        public async Task<User?> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user;
        }

        public async Task<List<User>> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return users;
        }

        public async Task<User> UpdateUser(User userToUpdate)
        {
            if (userToUpdate == null)
            {
                throw new Exception("User update failed");
            }
            var user = await _userManager.FindByIdAsync(userToUpdate.Id);

            if (user != null)
            {
                user.UserName = userToUpdate.UserName;
                user.Email = userToUpdate.Email;
                user.PhoneNumber = userToUpdate.PhoneNumber;
                user.Address = userToUpdate.Address;
                user.City = userToUpdate.City;
                user.ZipCode = userToUpdate.ZipCode;
                user.Country = userToUpdate.Country;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return user;
                }
            }

            throw new Exception("User update failed!");
        }

        public async Task<User?> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return user;
                }
            }

            return null;
        }

        public async Task<User?> GetUserByEmail(string? email)
        {
            var user = await _userManager.FindByEmailAsync(email!);
            return user;
        }

        // logout
        public async Task LogoutUser()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> ChangePassword(User user, string currentPassword, string newPassword)
        {
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                throw new Exception("Password change failed: " + result.Errors.First().Description);
            }
        }

        private Task<string> GenerateJwtToken(User? user)
        {
            if (user == null || user.UserName == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!)); // Get from configuration
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JWT:ExpirationDays"] ?? "1"));

            var token = new JwtSecurityToken(
                _configuration["JWT:Issuer"],
                _configuration["JWT:Audience"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }


    }
}