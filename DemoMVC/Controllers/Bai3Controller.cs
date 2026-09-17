using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DemoMVC.Models;

namespace DemoMVC.Controllers
{
    public class Bai3Controller : Controller
    {
        [HttpGet]
        public IActionResult Bai3()
        {
            return View("bmi");
        }

        [HttpPost]
        public IActionResult Bmi(Bai3 model)
        {
            double bmi = 0;
            string status = "";

            double heightInMeters = model.Height > 3 ? model.Height / 100 : model.Height;

            if (model.Height > 0)
            {
                bmi = model.Weight / (heightInMeters * heightInMeters);

                if (bmi < 18.5)
                    status = "Gầy";
                else if (bmi < 24.9)
                    status = "Bình thường";
                else if (bmi < 29.9)
                    status = "Thừa cân";
                else
                    status = "Béo phì";
            }

            ViewBag.BMI = bmi.ToString("0.00");
            ViewBag.Status = status;

            return View("bmi", model);
        }
    }
}