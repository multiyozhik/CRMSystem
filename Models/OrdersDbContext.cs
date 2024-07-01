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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasData(
                new Order(
                        Guid.NewGuid(),
                        DateTime.Now.AddMonths(-2),
                        "Иванов",
                        "Ivanov@mail.ru",
                        "Заявка на проведение оценки риска"),
                    new Order(
                        Guid.NewGuid(),
                        DateTime.Now.AddDays(-12),
                        "Петров",
                        "Petrov@mail.ru",
                        "Заявка на разработку раздела по пожарной безопасности"));
        }     

    }
}