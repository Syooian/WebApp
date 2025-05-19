using Microsoft.AspNetCore.Mvc;
using MyController.Models;

namespace MyController.Controllers
{
    public class ComplexBindController : Controller
    {
        //假設我們有一個會員資料表,有 會員編號、會員姓名、地址、電話、性別
        //1.撰寫會員資料表的資料模型(複雜繫結法必須要有模型Model)
        //2.我們要先有一個加入會員的表單介面(將介面實作於View)
        //3.所以要先有一個相對應的Action
        //4.假設我們的View名稱為Create.cshtml,則需有Create() Action

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Member member)
        {

            ViewData["MemberID"] = member.MemberID;
            ViewData["MemberName"] = member.MemberName;
            ViewData["Address"] = member.Address;
            ViewData["Phone"] = member.Phone;
            ViewData["Gender"] = member.Gender;

            return View();
        }
    }
}

