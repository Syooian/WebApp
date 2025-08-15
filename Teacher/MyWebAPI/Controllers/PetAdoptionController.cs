using System.Collections;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
        private readonly ThirdPartyService _thirdPartyService;

        public PetAdoptionController(ThirdPartyService thirdPartyService)
        {
            //_httpClient = new HttpClient();
            _thirdPartyService = thirdPartyService;
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
        public async Task<IEnumerable<PetAdoptionData>> Get(int pageSize = 30, int page=1)
        {
            //分頁功能-每N筆為一頁,若使用者沒有傳入pageSize參數，則預設為30筆資料一頁
            int skip = (page - 1) * pageSize; //計算要跳過的資料筆數           


            string url = $"https://data.moa.gov.tw/Service/OpenData/TransService.aspx?UnitId=QcbUEzN6E6DL&$top={pageSize}&$skip={skip}";

            var collection =await _thirdPartyService.Get<PetAdoptionData>(url);
            return collection;

        }


        //9.1.9 利用第三方API所給的使用說明文件，另外撰寫至少兩個不同的查詢功能以利測試

        //可用參數當條件查詢動物資料的功能

        [HttpGet("HasUrlParameter")]
        public async Task<IEnumerable<PetAdoptionData>> Get(string urlParameterName, string urlParameterValue, int pageSize = 30, int page = 1)
        {
            int skip = (page - 1) * pageSize;

            string url = $"https://data.moa.gov.tw/Service/OpenData/TransService.aspx?UnitId=QcbUEzN6E6DL&$top={pageSize}&$skip={skip}&{urlParameterName}={urlParameterValue}";
            var collection = await _thirdPartyService.Get<PetAdoptionData>(url);

            return collection;

        }

        //可用多個參數當條件查詢動物資料的功能
        [HttpGet("HaveUrlParameters")]
        public async Task<IEnumerable<PetAdoptionData>> Get(string urlParameters, int top = 200, int pageSize = 30, int page = 1)
        {
            int skip = (page - 1) * pageSize;

            string url = $"https://data.moa.gov.tw/Service/OpenData/TransService.aspx?UnitId=QcbUEzN6E6DL&$top={pageSize}&$skip={skip}{urlParameters}";
            var collection = await _thirdPartyService.Get<PetAdoptionData>(url);

            return collection;

        }

        //可以用動物種類查詢動物資料的功能
        //[HttpGet("AnimalKind")]
        //public async Task<IEnumerable<PetAdoptionData>> Get(string animalKind,int top= 200)
        //{
        //    string url = $"https://data.moa.gov.tw/Service/OpenData/TransService.aspx?UnitId=QcbUEzN6E6DL&$top={top}&animal_kind={animalKind}";
        //    var collection = await _thirdPartyService.Get<PetAdoptionData>(url);

        //    return collection;


        //}
    }
}
