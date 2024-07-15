using CRMSystem.Authentication;
using CRMSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
ConfigureServices(builder.Services);
var app = builder.Build();
Configure(app, app.Environment);

app.MapDefaultControllerRoute();
app.Run();


void ConfigureServices(IServiceCollection services)
{
    services.AddControllersWithViews();
    services.AddTransient<HomeModel>();     //TODO: в appsetings.json ConnectionStr через User ID, Password

    services.AddDbContext<OrdersDbContext>(options => options
        .UseSqlServer(builder.Configuration.GetConnectionString("OrdersConnectionString")));
    services.AddDbContext<AppUsersDbContext>(options => options
        .UseSqlServer(builder.Configuration.GetConnectionString("AppUsersConnectionString")));

    services.AddIdentity<AppUser, IdentityRole>()
        .AddEntityFrameworkStores<AppUsersDbContext>()
        .AddDefaultTokenProviders();

    services.Configure<IdentityOptions>(options =>
    {
        options.User.RequireUniqueEmail = true;
        options.Password.RequiredLength = 5;
        options.Password.RequiredUniqueChars = 1;
        options.Password.RequireLowercase = true;
    });
}

void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
{
    if (environment.IsDevelopment())
        app.UseDeveloperExceptionPage();
    else
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
}
