using ASP.NET_Sample.Models;
using Homework2.Model;
using Newtonsoft;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO.Compression;
using System.Net.Http.Headers;

namespace Homework2.Services
{
    public class FlightInformationServices
    {
        /// <summary>
        /// 
        /// </summary>
        readonly IConfiguration Configuration;
        /// <summary>
        /// 
        /// </summary>
        readonly IHttpClientFactory ClientFactory;
        /// <summary>
        /// 
        /// </summary>
        readonly string tokenUri = $"https://tdx.transportdata.tw";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Configuration"></param>
        /// <param name="ClientFactory"></param>
        public FlightInformationServices(IConfiguration Configuration, IHttpClientFactory ClientFactory)
        {
            this.ClientFactory = ClientFactory;
            this.Configuration = Configuration.GetSection("APIAccess");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Skip">跳過前幾筆</param>
        /// <param name="Top">取前幾筆</param>
        /// <returns></returns>
        public async Task<IEnumerable<Airport>> GetAirports(int Skip = 0, int Top = 30)
        {
            var AccessToken = GetToken(tokenUri).Result;

            //Debug.WriteLine($"https://tdx.transportdata.tw/api/basic/v2/Air/Airport?%24top={Top}&%24skip={Skip}&%24format=JSON");
            var Resp = Get(null, $"https://tdx.transportdata.tw/api/basic/v2/Air/Airport?%24top={Top}&%24skip={Skip}&%24format=JSON", AccessToken.access_token).Result;

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
            var AccessToken = GetToken(tokenUri).Result;

            //var Resp = await Client.GetStringAsync($"https://tdx.transportdata.tw/api/basic/v2/Air/Airline?%24top={Top}&%24skip={Skip}&%24format=JSON");
            var Resp = Get(null, $"https://tdx.transportdata.tw/api/basic/v2/Air/Airline?%24top={Top}&%24skip={Skip}&%24format=JSON", AccessToken.access_token).Result;

            //https://tdx.transportdata.tw/api/basic/v2/Air/Airline?%24top=30&%24format=JSON
            //https://tdx.transportdata.tw/api/basic/v2/Air/Airline?%24top=30&%24skip=0&%24format=JSON

            var Collection = JsonConvert.DeserializeObject<IEnumerable<Airline>>(Resp);

            return Collection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> GetParameters()
        {
            return new Dictionary<string, string>()
            {
                { $"$select","StationName" },
                { $"$filter",""},
                { $"$orderby",""},
                { $"$top","30"},
                { $"$skip",""},
                { $"health",""},
                { $"$format","JSON"},
            };
        }

        #region API權限存取
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="requestUri"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        async Task<string> Get(Dictionary<string, string> parameters, string requestUri, string token)
        {
            var client = ClientFactory.CreateClient();

            if (!string.IsNullOrWhiteSpace(token))
            {
                client.DefaultRequestHeaders.Add("authorization", $"Bearer {token}");
                client.DefaultRequestHeaders.Add("Accept-Encoding", "br,gzip");
            }
            //client.DefaultRequestHeaders.Add("Content-Type", "application/json; charset=utf-8");

            if (parameters != null && parameters.Any())
            {
                var strParam = string.Join("&", parameters.Where(d => !string.IsNullOrWhiteSpace(d.Value)).Select(o => o.Key + "=" + o.Value));
                requestUri = string.Concat(requestUri, '?', strParam);
            }
            client.BaseAddress = new Uri(requestUri);

            var response = await client.GetAsync(requestUri).ConfigureAwait(false);

            var responseContent = await DecompressResponse(response);

            return responseContent;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="RequestURI"></param>
        /// <returns></returns>
        async Task<AccessToken> GetToken(string RequestURI)
        {
            string BaseURI = $"https://tdx.transportdata.tw/auth/realms/TDXConnect/protocol/openid-connect/token";

            var Parameters = new Dictionary<string, string>()
            {
                { "grant_type", "client_credentials"},
                { "client_id", Configuration["ClientID"] },
                { "client_secret", Configuration["ClientSecret"]}
            };

            var FormData = new FormUrlEncodedContent(Parameters);

            var client = ClientFactory.CreateClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            client.DefaultRequestHeaders.Add("Accept-Encoding", "br,gzip");
            var Response = await client.PostAsync(BaseURI, FormData);

            var ResponseContent = await DecompressResponse(Response);

            return JsonConvert.DeserializeObject<AccessToken>(ResponseContent);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        async Task<string> DecompressResponse(HttpResponseMessage response)
        {
            if (response.Content.Headers.ContentEncoding.Contains("br"))
            {
                using (var stream = new BrotliStream(await response.Content.ReadAsStreamAsync(), CompressionMode.Decompress))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        return await reader.ReadToEndAsync();
                    }
                }
            }
            else if (response.Content.Headers.ContentEncoding.Contains("gzip"))
            {
                using (var stream = new GZipStream(await response.Content.ReadAsStreamAsync(), CompressionMode.Decompress))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        return await reader.ReadToEndAsync();
                    }
                }
            }
            else
            {
                return await response.Content.ReadAsStringAsync();
            }
        }
        #endregion
    }
}
