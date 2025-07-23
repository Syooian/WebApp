using Homework1.Data;
using Homework1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Homework1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        readonly DBContext Context;

        public HomeController(ILogger<HomeController> logger, DBContext Context)
        {
            _logger = logger;
            this.Context = Context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
