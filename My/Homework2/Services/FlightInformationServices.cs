using Homework2.Model;
using Newtonsoft;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Homework2.Services
{
    public class FlightInformationServices
    {
        /// <summary>
        /// 
        /// </summary>
        HttpClient Client;
        /// <summary>
        /// API金鑰
        /// </summary>
        readonly string APIAccessToken;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="Configuration"></param>
        public FlightInformationServices(HttpClient Client, IConfiguration Configuration)
        {
            this.Client = Client;
            APIAccessToken = Configuration["APIAccessToken"];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Skip">跳過前幾筆</param>
        /// <param name="Top">取前幾筆</param>
        /// <returns></returns>
        public async Task<IEnumerable<Airport>> GetAirports(int Skip = 0, int Top = 30)
        {
            Debug.WriteLine($"https://tdx.transportdata.tw/api/basic/v2/Air/Airport?%24top={Top}&%24skip={Skip}&%24format=JSON");
            var Resp = await Client.GetStringAsync($"https://tdx.transportdata.tw/api/basic/v2/Air/Airport?%24top={Top}&%24skip={Skip}&%24format=JSON");

            //https://tdx.transportdata.tw/api/basic/v2/Air/Airport?%24top=30&%24format=JSON
            //https://tdx.transportdata.tw/api/basic/v2/Air/Airport?%24top=30&%24skip=100&%24format=JSON

            var Collection = JsonConvert.DeserializeObject<IEnumerable<Airport>>(Resp);

            return Collection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Skip">跳過前幾筆</param>
        /// <param name="Top">取前幾筆</param>
        /// <returns></returns>
        public async Task<IEnumerable<Airline>> GetAirlines(int Skip = 0, int Top = 30)
        {
            var Resp = await Client.GetStringAsync($"https://tdx.transportdata.tw/api/basic/v2/Air/Airline?%24top={Top}&%24skip={Skip}&%24format=JSON");

            //https://tdx.transportdata.tw/api/basic/v2/Air/Airline?%24top=30&%24format=JSON
            //https://tdx.transportdata.tw/api/basic/v2/Air/Airline?%24top=30&%24skip=0&%24format=JSON

            var Collection = JsonConvert.DeserializeObject<IEnumerable<Airline>>(Resp);

            return Collection;
        }
    }
}
