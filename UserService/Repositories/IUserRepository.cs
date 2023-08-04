// IUserRepository interface
// By Maitham Al-rubaye

using UserService.Models;

namespace UserService.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User?> GetUser(int id);
        Task<User> AddUser(User user);
        Task<User> UpdateUser(User user);
        Task<User?> DeleteUser(int id);
        Task<User?> GetUserByEmail(string? email);

    }
}
