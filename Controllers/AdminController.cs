using CRMSystem.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        public AdminController(UserManager<AppUser> userManager) 
        {
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index() => View("Index", this.userManager.Users.AsEnumerable());


        [HttpGet]
        public IActionResult Register() => View("Register");

        [HttpPost]
        public IActionResult Register(RegisterModel model) => Ok("RegisterModel");

        [HttpGet]
        public IActionResult Login() => View("Login");

        [HttpPost]
        public IActionResult Login(LoginModel model) => Ok("LoginModel");

        //public IActionResult Logout()
        //{
        //    return View();
        //}
    }
}
