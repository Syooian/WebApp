using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Test1.Controllers
{
    public class SimpleBindController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 表單Submit後回傳至此Action
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(string ProductNo, string ProductName, int ProductPrice)
        {
            //變數名稱需與html內的name參數名稱相同才會自動對應接收值，否則須另外處理

            Debug.WriteLine($"商品編號 : {ProductNo}, 商品名稱 : {ProductName}, 商品價格 : {ProductPrice}");

            ViewData["ProductNo"] = ProductNo;
            ViewData["ProductName"] = ProductName;
            ViewData["ProductPrice"] = ProductPrice;

            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
