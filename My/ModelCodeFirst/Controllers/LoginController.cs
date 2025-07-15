using Microsoft.AspNetCore.Mvc;
using ModelCodeFirst.Models;

namespace ModelCodeFirst.Controllers
{
    public class LoginController : Controller
    {
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
            return View();
        }
    }
}
