using DemoMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoMVC.Controllers;

    public class Bai1Controller : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Message = "Nhan du lieu";
            return View();
        }
        [HttpPost]
        public IActionResult Index(Bai1 p)
        {
            int age = DateTime.Now.Year - p.Age;
            ViewBag.Message = $"Họ tên: {p.FullName}, Tuổi: {age}";
            return View();
        }

    }
