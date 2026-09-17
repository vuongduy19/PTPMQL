using Microsoft.AspNetCore.Mvc;
using DemoMVC.Models;

namespace DemoMVC.Controllers
{
    public class Bai2Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Bai2 c)
        {
            double result = 0;
            switch (c.Operation)
            {
                case "Add": result = c.Number1 + c.Number2; break;
                case "Sub": result = c.Number1 - c.Number2; break;
                case "Mul": result = c.Number1 * c.Number2; break;
                case "Div": 
                    result = c.Number2 != 0 ? c.Number1 / c.Number2 : double.NaN; 
                    break;
            }
            ViewBag.Result = $"Kết quả = {result}";
            return View();
        }
    }
}