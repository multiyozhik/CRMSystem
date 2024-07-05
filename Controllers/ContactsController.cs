using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Controllers
{
    public class ContactsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
