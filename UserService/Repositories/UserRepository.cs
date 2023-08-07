// IUserService class
// By Maitham Al-rubaye

using System.Collections.Generic;
using System.Threading.Tasks;
using UserService.Models;

namespace UserService.Repositories
{
    public interface UserRepository
    {
        Task<List<User>> GetUsers();
        Task<User?> GetUser(string id);
        Task<User> RegisterUser(User user, string password);
        Task<User> UpdateUser(User user);
        Task<User?> DeleteUser(string id);
        Task<User?> GetUserByEmail(string? email);
        Task<string?> LoginUser(string Email, string Password);
        Task LogoutUser();
    }
}