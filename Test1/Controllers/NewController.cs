using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Test1.Controllers
{
    public class NewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        /*
         * MVC關係圖
         * M -> C
         * C -> M
         * C -> V
         * V        ---->      Client -> C
         *       Response
         */

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Hello()
        {
            //於網址上輸入Controller名稱+ActionName(Method名稱)即可於網頁上顯示此字串
            //http://localhost:5192/New/Hello

            return "Hello World";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public string Hello2(string Name)
        {
            //於網址上輸入Controller名稱+ActionName(Method名稱)，並帶入參數即可於網頁上顯示此字串
            //http://localhost:5192/New/Hello2?Name=AAA

            return "Hello " + Name;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        public void Hello3(string Name)
        {
            Response.ContentType = "text/html; charset=utf-8";
            Response.WriteAsync($"Hello <span style='color:red'>{Name}</span>");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public IActionResult ShowPhoto(int ID)
        {
            ViewData["ShowPhoto"] = "<img src='/images/" + ID + ".jpg'>";

            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult ShowPhotos()
        {
            string str = "";
            for (int a = 1; a <= 8; a++)
            {
                str += "<img src='/images/" + a + ".jpg' width='200'>";
            }

            ViewData["ShowPhotos"] = str;

            return View();
        }
    }
}
