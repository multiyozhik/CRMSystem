using Microsoft.EntityFrameworkCore;
using CRMSystem.Models;

namespace CRMSystem.Authentication
{
    public class AppUsersDbContext : DbContext
    {
        public DbSet<AppUser>? AppUsers { get; }
        public AppUsersDbContext(DbContextOptions<AppUsersDbContext> options) : base(options) 
        {
            base.Database.EnsureCreated();            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AppUser>().HasData(
                new AppUser() {
                    Id = "1", 
                    UserName = "User1", 
                    Email = "User1@mail.ru", 
                    PasswordHash = "11111"},
                new AppUser()
                {
                    Id = "2",
                    UserName = "User2",
                    Email = "User2@mail.ru",
                    PasswordHash = "22222"
                }
                );
        }
    }
}
