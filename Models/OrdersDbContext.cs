using Microsoft.EntityFrameworkCore;

namespace CRMSystem.Models
{
    public class OrdersDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public OrdersDbContext(DbContextOptions<OrdersDbContext> options): base(options)
        {
            Database.EnsureCreated();
        }
    }
}