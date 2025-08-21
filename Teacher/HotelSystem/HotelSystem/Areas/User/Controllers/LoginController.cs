using System.Security.Claims;
using HotelSystem.Access.Data;
using HotelSystem.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace HotelSystem.Areas.User.Controllers
{
    public class LoginController : Controller
    {
        private readonly HotelSysDBContext2 _context;

        public LoginController(HotelSysDBContext2 context)
        {

            _context = context;
        }
        

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(MemberAccount memberAccount)
        {

            var user = await _context.MemberAccount.FirstOrDefaultAsync(u => u.Account == memberAccount.Account && u.Password == memberAccount.Password);
            
            
            if (user != null)
            {
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Actor, user.Account),
                        new Claim(ClaimTypes.Role, "Member")
                    };

                var claimsIdentity = new ClaimsIdentity(claims, "MemberLogin");

                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync("MemberLogin", claimsPrincipal); //把資料寫入 Cookie 進行登入狀態管理



                return RedirectToAction("Index", "Members"); // 登入成功後導向到 BooksManage 的 Index 頁面
            }

            ViewData["Error"] = "帳號或密碼錯誤，請重新輸入";
           
            
            return View(memberAccount);



         
        }
    }
}
