﻿using CRMSystem.Models;
using CRMSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace CRMSystem.Api
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class ApiHomeController : Controller
    {
        private HomeModel Model { get; }
        public ApiHomeController(HomeModel model)
        {
            Model = model;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<StatusCodeResult> AddOrder([FromBody] OrderDataFromRequest orderData)
        {
            await Model.Add(orderData.Name, orderData.Email, orderData.Message);
            return StatusCode(200);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<List<Order>> OrdersList() 
            => await Model.GetOrdersList();

        [HttpPost]
        public async Task ChangeStatus([FromBody] OrderStatus status, Guid id)
        {
            await Model.UpdateOrderStatus(status, id);
        }

        [HttpPost]
        public async Task<List<Order>> FilterByDateRange([FromBody] DateRangeFromRequest dateRangeFromRequest)
            => await Model.FilterOrdersByDateRange(
                dateRangeFromRequest.DateStart, 
                dateRangeFromRequest.DateEnd.AddDays(1));

        [HttpGet]
        public async Task<List<Order>> FilterByPeriod(string period)
        {
            var endDate = period == "yesterday" ? DateTime.Today : DateTime.Today.AddDays(1);
            return await Model.FilterOrdersByDateRange(
                period switch
                {
                    "today" => DateTime.Today,
                    "yesterday" => DateTime.Today.AddDays(-1),
                    "week" => DateTime.Today.AddDays(-7),
                    "month" => DateTime.Today.AddMonths(-1),
                    _ => DateTime.MinValue
                },
                endDate);
        }

        [HttpPost]
        public async Task Edit([FromBody] FieldValuesViewModel homeInterfaceVM)
        {
            using var fs = new FileStream("./wwwroot/files/default.json", FileMode.Create);
            await JsonSerializer.SerializeAsync(fs, homeInterfaceVM, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)
            });
        }
    }
    public record OrderDataFromRequest(string Name, string Email, string Message);
    public record DateRangeFromRequest(DateTime DateStart, DateTime DateEnd);
}
