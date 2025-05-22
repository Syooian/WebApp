using Microsoft.AspNetCore.Mvc;
using Test2.Models;

namespace Test2.Controllers
{
    public class LoginController : Controller
    {
        /// <summary>
        /// 登入頁面
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// 登入表單回傳
        /// </summary>
        /// <param name="LoginData"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(Login LoginData)
        {
            //帳密檢查
            if (LoginData.UserName != "admin" || LoginData.Password != "123456")
            {
                ViewData["LoginError"] = "帳號或密碼錯誤";

                return View();
            }

            //重新導向至HomeController的Index
            return RedirectToAction("Index", "Home");
        }
    }
}
