using Microsoft.AspNetCore.Mvc;
using MyModel_CodeFirst.Models;

namespace MyModel_CodeFirst.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            return View();
        }
    }
}
