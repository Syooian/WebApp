using Microsoft.AspNetCore.Mvc;
using ModelCodeFirst.Models;

namespace ModelCodeFirst.Controllers
{
    public class LoginController : Controller
    {
        readonly GuestBookContext Context;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public LoginController(GuestBookContext context)
        {
            Context = context;
        }

        /// <summary>
        /// 登入
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Login"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(Login Login)
        {
            var User = Context.Login.FirstOrDefault(U => U.Account == Login.Account && U.Password == Login.Password);
            if (User != null)
            {

            }

            ViewData["Error"] = "帳號或密碼錯誤，請重新輸入！";

            return View(Login);
        }
    }
}
