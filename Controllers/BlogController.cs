using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ArticleDescription()
        {
            return View();
        }
    }
}
