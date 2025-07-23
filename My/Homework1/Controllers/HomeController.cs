using Homework1.Data;
using Homework1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> Index()
        {
            var Result = await Context.MainTexts.Include(R => R.Replies).OrderByDescending(T => T.CreatedDate).ToListAsync();

            return View(Result);
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
