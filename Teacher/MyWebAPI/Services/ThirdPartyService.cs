using MyWebAPI.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace MyWebAPI.Services
{
    public class ThirdPartyService
    {
        private readonly HttpClient _httpClient;

        public ThirdPartyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        //public async Task<IEnumerable<PetAdoptionData>> Get(string? urlParameter,int top=200)
        //{
        //    string url = $"https://data.moa.gov.tw/Service/OpenData/TransService.aspx?UnitId=QcbUEzN6E6DL&$top={top}{urlParameter}";

        //    var resp = await _httpClient.GetStringAsync(url);
        //    var collection = JsonConvert.DeserializeObject<IEnumerable<PetAdoptionData>>(resp);


        //    return collection;


        //}

        //9.2.4 撰寫ThirdPartyService內容，包念HttpClient注入及取得資料的Get的API方法
        public async Task<IEnumerable<T>> Get<T>(string url)
        {

            var resp = await _httpClient.GetStringAsync(url);
            var collection = JsonConvert.DeserializeObject<IEnumerable<T>>(resp);

            return collection;

        }
    }
   

   
}
