using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Hello()
        {
            return View();
            
        }
        public IActionResult Goodbye()
        {
            var messages = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                messages.Add($"Goodbye {i + 1}");
            }

            ViewData["Messages"] = messages;
            return View();
        }
        [HttpGet]
        public IActionResult Calculate()
        {
            return View(new CalculatorModel());
        }

        [HttpPost]
        public IActionResult Calculate(CalculatorModel model)
        {
            
            switch (model.Operation)
            {
                case "+":
                    model.Result = model.A + model.B;
                    break;
                case "-":
                    model.Result = model.A - model.B;
                    break;
                case "*":
                    model.Result = model.A * model.B;
                    break;
                case "/":
                    model.Result = model.B != 0 ? model.A / model.B : 0;
                    break;
            }
            ViewBag.ShowResult = true;
            return View(model);
        }
    }
}