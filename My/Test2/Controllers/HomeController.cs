using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Test2.Models;

namespace Test2.Controllers
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
            string[] id = { "A01", "A02", "A03", "A04", "A05", "A06", "A07" };
            string[] name = { "瑞豐夜市", "新堀江商圈", "六合夜市", "青年夜市", "花園夜市", "大東夜市", "武聖夜市" };
            string[] address = { "813高雄市左營區裕誠路", "800高雄市新興區玉衡里", "800台灣高雄市新興區六合二路",
                "80652高雄市前鎮區凱旋四路758號", "台南市北區海安路三段533號", "台南市東區林森路一段276號",
                "台南市中西區武聖路69巷42號" };

            List<NightMarket> Data = new List<NightMarket>();

            for (int a = 0; a < id.Length; a++)
            {
                Data.Add(new NightMarket(id[a], name[a], address[a]));
            }

            return View(Data);
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
    }
}
