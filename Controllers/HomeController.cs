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

        public async Task AddOrder(string name, string email, string message)
        {
            await Model.Add(name, email, message);
            Response.ContentType = "text/html; charset=utf-8";
            await Response.WriteAsync(@"<!DOCTYPE html>
            <html>
                <head>
                    <title>CRM-System</title>
                    <meta charset=utf-8/>
                    <style>
                        div {
                            border: 1px solid blue; color:blue; 
                            font-size: 1.5em; text-align: center;
                            width: auto; margin: 20px; padding: 10px;
                        }       
                    </style>
                </head>
                <body>
                    <div>Заявка успешно отправлена</div>
                </body>");
        }
    }
}