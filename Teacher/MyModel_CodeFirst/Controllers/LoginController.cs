using Microsoft.AspNetCore.Mvc;
using MyModel_CodeFirst.Models;

namespace MyModel_CodeFirst.Controllers
{
    public class LoginController : Controller
    {
        private readonly GuestBookContext _context;

        public LoginController(GuestBookContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            var user = _context.Login.FirstOrDefault(u => u.Account == login.Account && u.Password == login.Password);
            if (user != null)
            { 

            }

            ViewData["Error"] = "帳號或密碼錯誤，請重新輸入";
            return View(login);
        }
    }
}
