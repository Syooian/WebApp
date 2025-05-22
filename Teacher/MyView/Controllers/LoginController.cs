using Microsoft.AspNetCore.Mvc;
using MyView.Models;

namespace MyView.Controllers
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
            //1.抓表單送來的帳密
            //2.檢查帳密是否正確

            //3.如果正確就導向後台頁面
            //4.如果錯誤就導回登入頁面,並顯示錯誤訊息

            //假設帳號為admin,密碼為12345678

            if ((login.UserName=="admin" && login.Password=="12345678"))
            {
                //導向後台頁面
                //
                return RedirectToAction("Index", "Home");
            }


            ViewData["Error"] = "帳密錯誤!!";
            return View();
        }
    }
}
