using Microsoft.EntityFrameworkCore;
using CRMSystem.Models;

namespace CRMSystem.Models
{
    public class OrdersDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Project> Projects { get; set; }
        public OrdersDbContext(DbContextOptions<OrdersDbContext> options): base(options)
        {
            Database.EnsureCreated();            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().HasData(
                new Project(Guid.Parse("5cae337b-74a8-4519-8af0-040a441e4258"),
                    "Обработка кредитных заявок",
                    "Описание проекта - Обработка кредитных заявок",
                    "card1.png"
                ),
                new Project(Guid.Parse("892aaeb2-ef85-48cd-8bb5-d61e9ff7d6a8"),
                    "Управление кадрами",
                    "Описание проекта - Управление кадрами",
                    "card2.png"),

                new Project(Guid.Parse("9ee5fb6d-1c64-4d96-9be1-4675379eca0a"), 
                    "Доработка CRM",
                    "Описание проекта - Доработка CRM",
                    "card3.png"));
        }
    }
}