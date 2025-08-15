using System.Collections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebAPI.Models;
using Newtonsoft.Json;

namespace MyWebAPI.Controllers
{
    [Route("api[controller]")]
    [ApiController]
    public class PetAdoptionController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public PetAdoptionController()
        {
            _httpClient = new HttpClient();
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
        public async Task<IEnumerable<PetAdoptionData>> Get()
        {
            string url = "https://data.moa.gov.tw/Service/OpenData/TransService.aspx?UnitId=QcbUEzN6E6DL&$top=200"; //這是資料來源


            //HttpClient client = new HttpClient();

            var resp = await _httpClient.GetStringAsync(url);  //取得API的回應的Json字串

            //將Json字串轉換為PetAdoptionData物件IEnumerable
            var collection = JsonConvert.DeserializeObject<IEnumerable<PetAdoptionData>>(resp);


            return collection;


        }


        //9.1.9 利用第三方API所給的使用說明文件，另外撰寫至少兩個不同的查詢功能以利測試

        //可以用縣市代碼查詢動物資料的功能
        [HttpGet("AnimalAreaPkid")]
        public async Task<IEnumerable<PetAdoptionData>> Get(int animalAreaPkid)
        {
            string url = $"https://data.moa.gov.tw/Service/OpenData/TransService.aspx?UnitId=QcbUEzN6E6DL&$top=200&animal_area_pkid={animalAreaPkid}"; //這是資料來源


            //HttpClient client = new HttpClient();

            var resp = await _httpClient.GetStringAsync(url);  //取得API的回應的Json字串

            //將Json字串轉換為PetAdoptionData物件IEnumerable
            var collection = JsonConvert.DeserializeObject<IEnumerable<PetAdoptionData>>(resp);


            return collection;


        }

        //可以用動物種類查詢動物資料的功能
        [HttpGet("AnimalKind")]
        public async Task<IEnumerable<PetAdoptionData>> Get(string animalKind)
        {
            string url = $"https://data.moa.gov.tw/Service/OpenData/TransService.aspx?UnitId=QcbUEzN6E6DL&$top=200&animal_kind={animalKind}"; //這是資料來源


            //HttpClient client = new HttpClient();

            var resp = await _httpClient.GetStringAsync(url);  //取得API的回應的Json字串

            //將Json字串轉換為PetAdoptionData物件IEnumerable
            var collection = JsonConvert.DeserializeObject<IEnumerable<PetAdoptionData>>(resp);


            return collection;


        }
    }
}
