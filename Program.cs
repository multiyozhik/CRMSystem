using CRMSystem.Authentication;
using CRMSystem.Models;
using CRMSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
ConfigureServices(builder.Services);

var app = builder.Build();
Configure(app, app.Environment);

app.MapDefaultControllerRoute();

app.Run();

void ConfigureServices(IServiceCollection services)
{
    services.AddControllersWithViews();
    services.AddTransient<HomeModel>();     //TODO: � appsetings.json ConnectionStr ����� User ID, Password
    services.AddTransient<ProjectsModel>();

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
        options.Password.RequireLowercase = false;
        options.Password.RequireDigit = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Lockout.MaxFailedAccessAttempts = 3;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    });

    services.AddAuthentication();
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

    CreateAdminAccount(app, builder.Configuration).Wait(); //������. ������
}

static async Task CreateAdminAccount(IApplicationBuilder app, IConfiguration config)
{
    //���� var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>(); �� ������ -
    //Can not resolve scoped service UserManager from root provider => ������������ �� ����. scope =>
    //https://learn.microsoft.com/en-us/dotnet/core/extensions/scoped-service#rewrite-the-worker-class

    using IServiceScope scope = app.ApplicationServices.CreateScope();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var adminLogin = config["AdminAccountData:Login"] ?? "admin";
    var adminEmail = config["AdminAccountData:Email"];
    var adminPassword = config["AdminAccountData:Password"];
    var adminRole = config["AdminAccountData:Role"] ?? "admin";

    var user = await userManager.FindByNameAsync(adminLogin);
    if (user is null)
    {
        await roleManager.CreateAsync(new IdentityRole(adminRole));
        var adminUser = new AppUser()
        {
            UserName = adminLogin,
            Email = adminEmail
        };
        var result = await userManager.CreateAsync(adminUser, adminPassword);
        if (result is not null)
        {
            await userManager.AddToRoleAsync(adminUser, adminRole);
        }
    }
}
