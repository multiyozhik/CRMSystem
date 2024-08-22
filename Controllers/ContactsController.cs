using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Controllers
{    
    public class ContactsController : Controller
    {
        public ContactsController()
        { }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
    }
}
