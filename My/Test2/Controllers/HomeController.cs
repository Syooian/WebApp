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

            Debug.WriteLine("HomeController");

            /*
             * _Layout.cshtml
             * 所有view共享同一Layout，且不可單獨存在，需依附別的script
             * 
             * _partialView.cshtml
             * 部分View，需依附其他的View才能顯示
             * */
        }

        /// <summary>
        /// Guest
        /// </summary>
        /// <returns></returns>
        public IActionResult IndexRWD()
        {
            return View(NightMarketData());
        }

        /// <summary>
        /// 夜市主資料
        /// </summary>
        List<NightMarket> NightMarketData()
        {
            string[] id = { "A01", "A02", "A03", "A04", "A05", "A06", "A07" };
            string[] name = { "瑞豐夜市", "新堀江商圈", "六合夜市", "青年夜市", "花園夜市", "大東夜市", "武聖夜市" };
            string[] address = { "813高雄市左營區裕誠路", "800高雄市新興區玉衡里", "800台灣高雄市新興區六合二路",
                "80652高雄市前鎮區凱旋四路758號", "台南市北區海安路三段533號", "台南市東區林森路一段276號",
                "台南市中西區武聖路69巷42號" };

            var NightMarketData = new List<NightMarket>();

            for (int a = 0; a < id.Length; a++)
            {
                NightMarketData.Add(new NightMarket(id[a], name[a], address[a]));
            }

            return NightMarketData;
        }

        public IActionResult Index()
        {
            Debug.WriteLine("Index");

            return View(NightMarketData());
        }

        /// <summary>
        /// 新增資料
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public IActionResult Details(string ID)
        {
            // Home/Details?ID=A01
            // Home/Details/A01

            var List = NightMarketData();

            //Linq寫法
            //var Result = (from N in List where N.ID == ID select N).FirstOrDefault();

            //一般For迴圈
            //NightMarket Result = null;
            //for (int a = 0; a < List.Count; a++)
            //{
            //    if (List[a].ID == ID)
            //    {
            //        Result = List[a];
            //        break;
            //    }
            //}

            //Lambda寫法1
            //var Result = List.Where(List => List.ID == ID).FirstOrDefault();

            //Lambda寫法2 (List.Find)
            var Result = List.Find(N => N.ID == ID);

            return View(Result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public IActionResult IndexList(string ID)
        {
            // Home/IndexList?ID=A01
            // Home/IndexList/A01

            var List = NightMarketData();

            VMNightMarket vmn = new VMNightMarket()
            {
                NightMarkets = List,
                NightMarket = List.Find(N => N.ID == ID)
            };

            return View(vmn);
        }
    }
}
