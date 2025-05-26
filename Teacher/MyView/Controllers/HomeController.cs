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

        private List<NightMarket> GetData()
        {
            string[] id = { "A01", "A02", "A03", "A04", "A05", "A06", "A07" };
            string[] name = { "瑞豐夜市", "新堀江商圈", "六合夜市", "青年夜市", "花園夜市", "大東夜市", "武聖夜市" };

            string[] address = { "813高雄市左營區裕誠路", "800高雄市新興區玉衡里", "800台灣高雄市新興區六合二路",
                "80652高雄市前鎮區凱旋四路758號", "台南市北區海安路三段533號", "台南市東區林森路一段276號",
                "台南市中西區武聖路69巷42號" };


            List<NightMarket> list = new List<NightMarket>();


            for (int i = 0; i < id.Length; i++)
            {
                NightMarket nm = new NightMarket();
                nm.Id = id[i];
                nm.Name = name[i];
                nm.Address = address[i];
                list.Add(nm);
            }

            return list;
        }



        public IActionResult IndexRWD()
        {
            return View(GetData());

        }


        public IActionResult IndexList(string id)
        {
            var list = GetData();

            VMNightMarket vmN = new VMNightMarket()
            {
                //左側導覽列
                //取得所有夜市資料的編號與名稱
                NightMarkets = list,
                //右側顯示資料內容主畫面
                //取得某一筆夜市資料的詳細內容
                NightMarket = list.Where(list => list.Id == id).FirstOrDefault()
            };

            //左側導覽列
            //取得所有夜市資料的編號與名稱
            //ViewData["nm"] = list;

            //右側顯示資料內容主畫面
            //取得某一筆夜市資料的詳細內容
            //Lambda寫法
            //var result = list.Where(list => list.Id == id).FirstOrDefault();

            return View(vmN);

        }


        public IActionResult Index()
        {

            return View(GetData());
        }
        public IActionResult Details(string id)
        {
            var list = GetData();

            //select *
            //from list
            //where id = 'A05'

            //Linq寫法
            //var result = (from n in list
            //              where n.Id == id
            //              select n).FirstOrDefault();

          
            //Lambda寫法
            var result = list.Where(list => list.Id == id).FirstOrDefault();


            return View(result);
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
