using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyView.Models;

namespace MyView.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult IndexRWD()
        {
            //用陣列來存放白板上面的資料
            string[] id = { "A01", "A02", "A03", "A04", "A05", "A06", "A07" };
            string[] name = { "瑞豐夜市", "新堀江商圈", "六合夜市", "青年夜市", "花園夜市", "大東夜市", "武聖夜市" };

            string[] address = { "813高雄市左營區裕誠路", "800高雄市新興區玉衡里", "800台灣高雄市新興區六合二路",
                "80652高雄市前鎮區凱旋四路758號", "台南市北區海安路三段533號", "台南市東區林森路一段276號",
                "台南市中西區武聖路69巷42號" };


            //NightMarket nm = new NightMarket();
            //nm.Id = id[0];
            //nm.Name = name[0];
            //nm.Address = address[0];

            //NightMarket nm2 = new NightMarket();
            //nm.Id = id[1];
            //nm.Name = name[1];
            //nm.Address = address[1];

            //NightMarket nm3 = new NightMarket();
            //nm.Id = id[2];
            //nm.Name = name[2];
            //nm.Address = address[2];


            List<NightMarket> list = new List<NightMarket>();


            for (int i = 0; i < id.Length; i++)
            {
                NightMarket nm = new NightMarket();
                nm.Id = id[i];
                nm.Name = name[i];
                nm.Address = address[i];
                list.Add(nm);
            }



            return View(list);

        }


        public IActionResult Index()
        {
            //用陣列來存放白板上面的資料
            string[] id = { "A01", "A02", "A03", "A04", "A05", "A06", "A07" };
            string[] name = { "瑞豐夜市", "新堀江商圈", "六合夜市", "青年夜市", "花園夜市", "大東夜市", "武聖夜市" };

            string[] address = { "813高雄市左營區裕誠路", "800高雄市新興區玉衡里", "800台灣高雄市新興區六合二路",
                "80652高雄市前鎮區凱旋四路758號", "台南市北區海安路三段533號", "台南市東區林森路一段276號",
                "台南市中西區武聖路69巷42號" };


            //NightMarket nm = new NightMarket();
            //nm.Id = id[0];
            //nm.Name = name[0];
            //nm.Address = address[0];

            //NightMarket nm2 = new NightMarket();
            //nm.Id = id[1];
            //nm.Name = name[1];
            //nm.Address = address[1];

            //NightMarket nm3 = new NightMarket();
            //nm.Id = id[2];
            //nm.Name = name[2];
            //nm.Address = address[2];


            List<NightMarket> list = new List<NightMarket>();


            for (int i = 0; i < id.Length; i++)
            {
                NightMarket nm = new NightMarket();
                nm.Id = id[i];
                nm.Name = name[i];
                nm.Address = address[i];
                list.Add(nm);
            }



            return View(list);
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Razor()
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
