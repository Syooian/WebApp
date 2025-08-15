using System.Drawing.Printing;
using Microsoft.AspNetCore.Mvc;
using MyWebAPI.Models;
using MyWebAPI.Services;

namespace MyWebAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ThirdPartyService _thirdPartyService;

        public HomeController(ThirdPartyService thirdPartyService)
        {
 
            _thirdPartyService = thirdPartyService;
        }

        public async Task<IActionResult> Index(int pageSize = 30, int page = 1)
        {
            //分頁功能-每N筆為一頁,若使用者沒有傳入pageSize參數，則預設為30筆資料一頁
            int skip = (page - 1) * pageSize; //計算要跳過的資料筆數           


            string url = $"https://data.moa.gov.tw/Service/OpenData/TransService.aspx?UnitId=QcbUEzN6E6DL&$top={pageSize}&$skip={skip}";

            var collection = await _thirdPartyService.Get<PetAdoptionData>(url);

            return View(collection.ToList());
        }
    }
}
