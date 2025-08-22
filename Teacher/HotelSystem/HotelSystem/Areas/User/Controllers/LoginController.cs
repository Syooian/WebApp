using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using HotelSystem.Access.Data;
using HotelSystem.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace HotelSystem.Areas.User.Controllers
{
    [Area("User")]
    public class LoginController : Controller
    {
        private readonly HotelSysDBContext2 _context;

        public LoginController(HotelSysDBContext2 context)
        {

            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
 
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(MemberAccount memberAccount)
        {
           
            if (memberAccount == null || string.IsNullOrEmpty(memberAccount.Account) || string.IsNullOrEmpty(memberAccount.Password))
            {
                ViewData["Error"] = "請輸入帳號和密碼";
                return View();
            }

            var user = await _context.MemberAccount.Include(u => u.Member)                                        //密碼必須先經過雜湊處理，再與資料庫中的密碼進行比對
                .FirstOrDefaultAsync(u => u.Account == memberAccount.Account && u.Password == ComputeSha256Hash(memberAccount.Password)); //12345678


            if (user != null)
            {
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Actor, user.Account),
                        new Claim(ClaimTypes.Role, "Member"),
                         new Claim(ClaimTypes.Sid, user.MemberID),
                          new Claim(ClaimTypes.Name, user.Member.Name)

                    };

                var claimsIdentity = new ClaimsIdentity(claims, "MemberLogin");

                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync("MemberLogin", claimsPrincipal); //把資料寫入 Cookie 進行登入狀態管理



                return RedirectToAction("Index", "Members"); // 登入成功後導向到 BooksManage 的 Index 頁面
            }

            ViewData["Error"] = "帳號或密碼錯誤，請重新輸入";


            return View(memberAccount);

        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync("MemberLogin");// 清除登入狀態(清除Cookie的MemberLogin紀錄)
            return RedirectToAction("Index", "Rooms"); // 登出後導向到 Login 頁面


        }

        // 新增一個靜態方法來進行 SHA256 雜湊
        private static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // 計算雜湊值
                byte[] bytes = sha256Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(rawData));

                // 將 byte 陣列轉換成十六進位字串
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
