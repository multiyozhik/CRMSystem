using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CRMSystem.Models;

namespace CRMSystem.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly ProjectsModel model;
        public ProjectsController(ProjectsModel projectsModel)
        {
            model = projectsModel;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View("Index", await model.GetProjectsList());
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult ProjectDescription()
        {
            return View("ProjectDescription");
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(string name, string description, string photo)
        {
            await model.Add(name, description, photo);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var project = await model.GetProjectById(id);
            return View(project);
        }

        [HttpPost]   //здесь конфликт id
        public async Task<IActionResult> Update([FromForm] Project project)
        {
            await model.Update(project);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await model.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
