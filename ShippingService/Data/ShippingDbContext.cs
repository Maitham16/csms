// Shipping context class
// By Maitham Al-rubaye

using Microsoft.EntityFrameworkCore;
using ShippingService.Models;

namespace ShippingService.Data
{
    public class ShippingDbContext : DbContext
    {
        public ShippingDbContext(DbContextOptions<ShippingDbContext> options) : base(options)
        {
        }

        public DbSet<Shipping> Shippings { get; set; }
    }
}