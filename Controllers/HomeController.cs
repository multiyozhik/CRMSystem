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
        [HttpGet]
        public IActionResult Index() => View();

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> AddOrder(string name, string email, string message)
        {
            await Model.Add(name, email, message);
            return View("OrderAdded");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Desktop() => View(await Model.GetOrdersList());

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

        [Authorize]
        [HttpPost]
        //AddDays(1), чтобы заявки последнего дня входили включительно, т.к. TimeStamp содержит время
        public async Task<IActionResult> FilterByDateRange([FromForm]DateTime dateStart, DateTime dateEnd)
        {
            var ordersByPeriod = await Model.FilterOrdersByDateRange(dateStart, dateEnd.AddDays(1));
            ViewBag.OrdersByPeriodCount = ordersByPeriod.Count;
            return View("Desktop", ordersByPeriod);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> FilterByPeriod([FromQuery]string period)
        {
            //иначе заявки последнего дня не войдут, т.к. TimeStamp содержит время
            var endDate = period =="yesterday"? DateTime.Today : DateTime.Today.AddDays(1);
            var ordersByPeriod = await Model.FilterOrdersByDateRange(
                period switch
                {
                    "today" => DateTime.Today,
                    "yesterday" => DateTime.Today.AddDays(-1),
                    "week" => DateTime.Today.AddDays(-7),
                    "month" => DateTime.Today.AddMonths(-1),
                    _ => DateTime.MinValue
                },
                endDate);
            ViewBag.OrdersByPeriodCount = ordersByPeriod.Count;
            return View("Desktop", ordersByPeriod);
        }
    }
}