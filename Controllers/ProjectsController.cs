using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Controllers
{
    public class ProjectsController : Controller
    {
        public ProjectsController()
        { }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View("Index");
        }

        [AllowAnonymous]
        public IActionResult ProjectDescription()
        {
            return View("ProjectDescription");
        }
    }
}
