using System.Collections;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebAPI.Models;
using MyWebAPI.Services;
using Newtonsoft.Json;

namespace MyWebAPI.Controllers
{
    [Route("api[controller]")]
    [ApiController]
    public class PetAdoptionController : ControllerBase
    {
        //9.2.6 將ThirdPartyService注入PetAdoptionController，並將原來注入的HttpClient相關程式碼註解
        //private readonly HttpClient _httpClient;
        private readonly PetAdoptionService _petAdoptionService;

        public PetAdoptionController(PetAdoptionService petAdoptionService)
        {
            //_httpClient = new HttpClient();
            _petAdoptionService = petAdoptionService;
        }

        //9.1.5 撰寫Get()方法，使用HttpClient物件取得第三方API的資料
        //[HttpGet]
        //public async Task<IEnumerable<PetAdoptionData>> Get()
        //{
        //    string url = "https://data.moa.gov.tw/Service/OpenData/TransService.aspx?UnitId=QcbUEzN6E6DL&$top=200"; //這是資料來源


        //    HttpClient client = new HttpClient();

        //    var resp = await client.GetStringAsync(url);  //取得API的回應的Json字串

        //    //將Json字串轉換為PetAdoptionData物件IEnumerable
        //    var collection = JsonConvert.DeserializeObject<IEnumerable<PetAdoptionData>>(resp);


        //    return collection;


        //}

        //9.2.1 將PetAdoptionController中的HttpClient物件寫成DI方式
        [HttpGet]
        public async Task<IEnumerable<PetAdoptionData>> Get(int top=200)
        {
            
 
            var collection =await _petAdoptionService.Get(null, top);
            return collection;

        }


        //9.1.9 利用第三方API所給的使用說明文件，另外撰寫至少兩個不同的查詢功能以利測試

        //可以用縣市代碼查詢動物資料的功能
        [HttpGet("AnimalAreaPkid")]
        public async Task<IEnumerable<PetAdoptionData>> Get(int animalAreaPkid, int top = 200)
        {
            var collection = await _petAdoptionService.Get($"&animal_area_pkid={animalAreaPkid}", top);

            return collection;


        }

        //可以用動物種類查詢動物資料的功能
        [HttpGet("AnimalKind")]
        public async Task<IEnumerable<PetAdoptionData>> Get(string animalKind,int top= 200)
        {

            var collection = await _petAdoptionService.Get($"&animal_kind={animalKind}", top);

            return collection;


        }
    }
}
