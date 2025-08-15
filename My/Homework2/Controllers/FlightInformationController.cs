using Homework2.Model;
using Homework2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Homework2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FlightInformationController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        readonly FlightInformationServices Services;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Services"></param>
        public FlightInformationController(FlightInformationServices Services)
        {
            this.Services = Services;
        }

        /// <summary>
        /// 取得所有機場資料
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="Page"></param>
        /// <returns></returns>
        [Route("GetAirports")]
        [HttpGet]
        public async Task<IEnumerable<Airport>> GetAirports(int PageSize = 30, int Page = 1)
        {
            int Skip = (Page - 1) * PageSize;

            var Result = await Services.GetAirports(Skip, PageSize);
            if (Result == null)
                return new List<Airport>();

            return Result;
        }

        /// <summary>
        /// 取得所有航空公司資料
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="Page"></param>
        /// <returns></returns>
        [Route("GetAirlines")]
        [HttpGet]
        public async Task<IEnumerable<Airline>> GetAirlines(int PageSize = 30, int Page = 1)
        {
            int Skip = (Page - 1) * PageSize;

            var Result = await Services.GetAirlines(Skip, PageSize);
            if (Result == null)
                return new List<Airline>();

            return Result;
        }
    }
}
