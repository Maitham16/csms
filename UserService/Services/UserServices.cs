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

namespace UserService.Services
{
    public class UserServices : UserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserServices(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                throw new Exception("User creation failed: " + result.Errors.First().Description);
            }
        }

        public async Task<User?> LoginUser(string Email, string Password)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, Password, false);
                if (result.Succeeded)
                {
                    return user;
                }
            }
            return null;
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

    }
}