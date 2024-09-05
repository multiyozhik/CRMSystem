using CRMSystem.Authentication;
using CRMSystem.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CRMSystem.Api
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ApiAdminController : ControllerBase
    {
        readonly UserManager<AppUser> userManager;
        readonly IPasswordHasher<AppUser> passwordHasher;
        readonly IConfiguration config;

        public ApiAdminController(
            UserManager<AppUser> userManager,
            IPasswordHasher<AppUser> passwordHasher,
            IConfiguration config)
        {
            this.userManager = userManager;
            this.passwordHasher = passwordHasher;
            this.config = config;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppUser([FromBody] AppUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser newAppUser = new()
                {
                    UserName = model.UserName,
                    Email = model.Email
                };
                IdentityResult result = await userManager.CreateAsync(newAppUser, model.Password);
                string? adminRole = config["AdminAccountData:Role"];
                if (result.Succeeded && adminRole is not null && !adminRole.IsNullOrEmpty())
                {
                    await userManager.AddToRoleAsync(newAppUser, adminRole);
                    return new StatusCodeResult(200);
                }
                else
                    return new StatusCodeResult(400);
            }
            return new StatusCodeResult(400);
        }

        [HttpPost]
        public async Task<IActionResult> EditAppUser(
            [FromRoute] string id, [FromBody] string userName, string email, string password)
        {
            if (ModelState.IsValid)
            {
                AppUser? user = await userManager.FindByIdAsync(id);
                if (user is not null)
                {
                    user.UserName = userName;
                    user.Email = email;
                    user.PasswordHash = passwordHasher.HashPassword(user, password);
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return new StatusCodeResult(200);
                    else
                        return new StatusCodeResult(404);
                }
                else
                    return new StatusCodeResult(404);
            }
            return new StatusCodeResult(400);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAppUser(string id)
        {
            AppUser? user = await userManager.FindByIdAsync(id);
            if (user is not null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return new StatusCodeResult(200);
                else
                    return new StatusCodeResult(404);
            }
            else
                return new StatusCodeResult(400);
        }
    }
}
