using Microsoft.EntityFrameworkCore;
using CRMSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CRMSystem.Authentication
{
    //если наследовать просто от DbContext, то ошибка, что "Cannot create a DbSet for 'IdentityUserClaim<string>'
    //because this type is not included in the model for the context"
    //https://github.com/dotnet/aspnetcore/issues/5793

    public class AppUsersDbContext : IdentityDbContext
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
