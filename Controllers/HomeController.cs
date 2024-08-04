using CRMSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> AddOrder(string name, string email, string message)
        {
            await Model.Add(name, email, message);
            return View("OrderAdded");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Desktop()
        {
            return View(await Model.GetOrdersList());
        }

        [Authorize]
        [HttpGet]
        public IActionResult ChangeStatus(Guid id)
        {
            ViewBag.OrderId = id;
            return View("ChangeStatus");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangeStatus(OrderStatus status, Guid id)
        {
            await Model.UpdateOrderStatus(status, id);
            return RedirectToAction("Desktop");
        }
    }
}