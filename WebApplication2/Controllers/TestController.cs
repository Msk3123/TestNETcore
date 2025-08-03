using Microsoft.AspNetCore.Mvc;

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

    }
}