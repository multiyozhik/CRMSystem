using CRMSystem.Authentication;
using CRMSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Controllers
{
    public class AdminController : Controller
    {
        readonly UserManager<AppUser> userManager;
        readonly IPasswordHasher<AppUser> passwordHasher;

        public AdminController(UserManager<AppUser> userManager, IPasswordHasher<AppUser> passwordHasher) 
        {
            this.userManager = userManager;
            this.passwordHasher = passwordHasher;
        }

        [HttpGet]
        public IActionResult Index() => View("Index", userManager.Users.AsEnumerable());

        [HttpGet]
        public IActionResult CreateAppUser() => View();

        [HttpPost]
        public async Task<IActionResult> CreateAppUser([FromForm] AppUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser newAppUser = new()
                {
                    UserName = model.UserName,
                    Email = model.Email
                };
                IdentityResult result = await userManager.CreateAsync(newAppUser, model.Password);
                if (result.Succeeded) 
                    return RedirectToAction("Index");
                else 
                    AddErrorToEntityResult(result.Errors);                
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditAppUser(string id)
        {
            AppUser? user = await userManager.FindByIdAsync(id);
            if (user is not null)
            {
                return View("EditAppUser", 
                    new AppUserViewModel() { 
                        Id = user.Id, 
                        UserName = user.UserName, 
                        Email = user.Email, 
                        Password = user.PasswordHash 
                    }) ;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditAppUser(
            [FromRoute] string id, [FromForm] string userName, string email, string password)
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
                        return RedirectToAction("Index");
                    else
                    {
                        AddErrorToEntityResult(result.Errors);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь не найден");
                }                
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAppUser(string id)
        {
            AppUser? user = await userManager.FindByIdAsync(id);
            if (user is not null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    AddErrorToEntityResult(result.Errors);
            }
            else
            {
                ModelState.AddModelError("", "Пользователь не найден");
            }                
            return View("Index", userManager.Users.AsEnumerable());
        }

        private void AddErrorToEntityResult(IEnumerable<IdentityError> errors)
        {
            foreach (IdentityError error in errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}
