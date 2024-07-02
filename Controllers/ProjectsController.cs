using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Controllers
{
    public class ProjectsController : Controller
    {
        public ProjectsController()
        { }

        public IActionResult Index()
        {
            return View("Index");
        }

        public IActionResult ProjectDescription()
        {
            return View("ProjectDescription");
        }
    }
}
