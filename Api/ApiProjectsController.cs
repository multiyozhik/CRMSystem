using CRMSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Api
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ApiProjectsController : ControllerBase
    {
        private readonly ProjectsModel model;
        public ApiProjectsController(ProjectsModel projectsModel)
        {
            model = projectsModel;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<List<Project>> ProjectsList()
            => await model.GetProjectsList();

        [HttpPost]
        public async Task Add([FromBody] ProjectDataFromRequest projectData)
            => await model.Add(projectData.Name, projectData.Description, projectData.Photo);

        [HttpGet]
        public async Task<Project> Description(Guid id)
            => await model.GetProjectById(id);

        [HttpPost]
        public async Task Update([FromBody] Project project)
            => await model.Update(project);


        [HttpPost]
        public async Task Delete(Guid id)
            => await model.Delete(id);
    }

    public record ProjectDataFromRequest(string Name, string Description, string Photo);
}
