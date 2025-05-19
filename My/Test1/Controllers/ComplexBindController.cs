using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Test1.Models;

namespace Test1.Controllers
{
    public class ComplexBindController : Controller
    {
        /*
         * 複雜繫結法需有模型
         */

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Member"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(Member Member)
        {
            Debug.WriteLine($"ID : {Member.ID}, Name : {Member.Name}, Address : {Member.Address}, Phone : {Member.Phone}");

            ViewData["ID"] = Member.ID;
            ViewData["Name"] = Member.Name;
            ViewData["Address"] = Member.Address;
            ViewData["Phone"] = Member.Phone;

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult CreateProduct()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateProduct(Product Product)
        {
            Debug.WriteLine($"商品編號 : {Product.ProductNo}, 商品名稱 : {Product.ProductName}, 商品價格 : {Product.ProductPrice}");

            ViewData["ProductNo"] = Product.ProductNo;
            ViewData["ProductName"] = Product.ProductName;
            ViewData["ProductPrice"] = Product.ProductPrice;

            return View();
        }
    }
}
