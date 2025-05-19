using Microsoft.AspNetCore.Mvc;
using MyController.Models;

namespace MyController.Controllers
{
    public class SimpleBindController : Controller
    {
        //假設我們有一個商品資料表,有 商品編號、商品名稱、商品價格
        //1.我們要先有一個商品新增的表單介面(將介面實作於View)
        //2.所以要先有一個相對應的Action
        //3.假設我們的View名稱為Create.cshtml,則需有Create() Action
        public IActionResult Create()
        {

            return View();
        }


        //表單submit後,會將資料傳到Controller的指定Action
        [HttpPost]//這裡表示是由Post方法呼叫Action
        public IActionResult Create(string ProductNo, string ProductName, string ProductPrice) 
        {

            ViewData["ProductNo"] = ProductNo;
            ViewData["ProductName"] = ProductName;
            ViewData["ProductPrice"] = ProductPrice;

            return View();
        }


        public IActionResult Create2()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create2(Product product)
        {

            ViewData["ProductNo"] = product.ProductNo;
            ViewData["ProductName"] = product.ProductName;
            ViewData["ProductPrice"] = product.ProductPrice;

            return View();
        }

    }
}
