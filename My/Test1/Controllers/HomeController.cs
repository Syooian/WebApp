using ConsoleApp1;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Test1.Models;

namespace Test1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["MyTime"] = DateTime.Now;

            ViewData["ToGoogle"] = "<a href='http://www.google.com.tw'>Google</a>";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult CheckID(string ID)
        {
            ViewData["CheckIDResult"] = CheckIDCS.CheckID(ID);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
