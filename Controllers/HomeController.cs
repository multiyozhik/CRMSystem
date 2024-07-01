using CRMSystem.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CRMSystem.Controllers
{
    public class HomeController : Controller
    {
        private HomeModel Model { get; }

        public HomeController(HomeModel model)
        {
            Model = model;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddOrder(string name, string email, string message)
        {
            await Model.Add(name, email, message);
            return Ok("Заявка успешно отправлена");
        }
    }
}