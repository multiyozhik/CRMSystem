using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Controllers
{
    public class ServicesController : Controller
    {
        public ServicesController()
        { }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}
