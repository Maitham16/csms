// UserContext class for Entity Framework Core
// by Maitham Al-rubaye

using Microsoft.EntityFrameworkCore;
using UserService.Models;

namespace UserService.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
            Users = Set<User>();
        }

        public DbSet<User> Users { get; set; }
    }
}
