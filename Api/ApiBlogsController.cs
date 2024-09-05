using CRMSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Api
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class ApiBlogsController : ControllerBase
    {
        private readonly BlogsModel model;
        public ApiBlogsController(BlogsModel blogsModel)
        {
            model = blogsModel;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<List<Blog>> BlogsList()
            => await model.GetBlogsList();

        [HttpPost]
        public async Task<StatusCodeResult> Add(ArticleDataFromRequest articleData)
        {
            await model.Add(articleData.Name, articleData.Description, articleData.Photo);
            return StatusCode(200);
        }

        [HttpGet]
        public async Task<Blog> GetBlogById(Guid id)
            => await model.GetBlogById(id);

        [HttpPost]
        public async Task Update(Blog blog)
            => await model.Update(blog);

        [HttpPost]
        public async Task Delete(Guid id)
        => await model.Delete(id);
    }

    public record ArticleDataFromRequest(string Name, string Description, string Photo);
}
