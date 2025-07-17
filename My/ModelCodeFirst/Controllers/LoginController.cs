using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ModelCodeFirst.Models;
using System.Security.Claims;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Login(Login Login)
        {
            var User = Context.Login.FirstOrDefault(U => U.Account == Login.Account && U.Password == Login.Password);
            if (User != null)
            {
                var Claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,User.Account),
                    new Claim(ClaimTypes.Role,"Admin"),//角色(Admin, User, Guest...etc)，系統簡單的網站可加可不加
                };

                var Identity = new ClaimsIdentity(Claims, "AdminLogin");//
                var Principal = new ClaimsPrincipal(Identity);//通行時管理存取狀態&存活時間

                await HttpContext.SignInAsync("AdminLogin", Principal);//登入

                return RedirectToAction("Index", "BooksManage");//登入成功後導向首頁
            }

            ViewData["Error"] = "帳號或密碼錯誤，請重新輸入！";

            return View(Login);
        }
    }
}
