using Microsoft.AspNetCore.Mvc;
using NorthwindStore.Models;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

namespace NorthwindStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region 取圖片
        /// <summary>
        /// 
        /// </summary>
        /// <param name="OlePicture"></param>
        /// <returns></returns>
        public static byte[] RemoveOleHeader(byte[] OlePicture)
        {
            const int OleHeaderLength = 78;//OLE header 通常長度為 78 bytes

            if (OlePicture == null || OlePicture.Length <= OleHeaderLength)
            {
                return OlePicture;
            }

            return OlePicture.Skip(OleHeaderLength).ToArray();
        }
        #endregion
    }
}
