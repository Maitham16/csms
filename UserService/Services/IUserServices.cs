// IUserService class
// By Maitham Al-rubaye

using System.Collections.Generic;
using System.Threading.Tasks;
using UserService.Models;

namespace UserService.Services
{
    public interface IUserServices
    {
        Task<List<User>> GetUsers();
        Task<User?> GetUser(string id);
        Task<User> RegisterUser(string Username, string FirstName, string LastName, DateTime Birthdate, string Email, string Password, string PhoneNumber, string Address, string City, string ZipCode, string Country, string Role);
        Task<User> UpdateUser(UpdateModel user);
        Task<User?> DeleteUser(string id);
        Task<User?> GetUserByEmail(string? email);
        Task<User?> LoginUser(string Email, string Password);
        Task LogoutUser();
    }
}
