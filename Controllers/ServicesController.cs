using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Controllers
{
    public class ServicesController : Controller
    {
        public ServicesController()
        { }

        public IActionResult Index()
        {
            return View("Index");
        }
    }
}
