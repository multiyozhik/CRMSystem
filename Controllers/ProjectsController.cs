using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Controllers
{
    public class ProjectsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task GetProjectDescription()
        {
            Response.ContentType="text/html";
            await Response.WriteAsync("Project descriprion here");
        }
    }
}
