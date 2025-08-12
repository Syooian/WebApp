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
        //9.1.5 撰寫Get()方法，使用HttpClient物件取得第三方API的資料
        [HttpGet]
        public async Task<IEnumerable<PetAdoptionData>> Get()
        {
            string url = "https://data.moa.gov.tw/Service/OpenData/TransService.aspx?UnitId=QcbUEzN6E6DL&$top=200"; //這是資料來源


            HttpClient client = new HttpClient();

            var resp = await client.GetStringAsync(url);  //取得API的回應的Json字串

            //將Json字串轉換為PetAdoptionData物件IEnumerable
            var collection = JsonConvert.DeserializeObject<IEnumerable<PetAdoptionData>>(resp);


            return collection;

          
        }
    }
}
