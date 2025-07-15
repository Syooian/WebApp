using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
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
        public async Task<IActionResult> Login(Login login) // 修改方法為非同步，並將回傳型別改為 Task<IActionResult>
        {
            var user = _context.Login.FirstOrDefault(u => u.Account == login.Account && u.Password == login.Password);
            if (user != null)
            {
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Account),
                        new Claim(ClaimTypes.Role, "Manager"), // 假設所有登入的使用者都是管理者
                    };

                var claimsIdentity = new ClaimsIdentity(claims, "ManagerLogin");

                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                //await HttpContext.SignInAsync("ManagerLogin", claimsPrincipal); //把資料寫入 Cookie 進行登入狀態管理

                return RedirectToAction("Index", "BooksManage"); // 登入成功後導向到 BooksManage 的 Index 頁面
            }

            ViewData["Error"] = "帳號或密碼錯誤，請重新輸入";
            return View(login);
        }
    }
}
