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
            var Result = await Services.GetAirports(GetSkip(Page, PageSize), PageSize);
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
            var Result = await Services.GetAirlines(GetSkip(Page, PageSize), PageSize);
            if (Result == null)
                return new List<Airline>();

            return Result;
        }

        /// <summary>
        /// 取得所有航班資料
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="Page"></param>
        /// <returns></returns>
        [HttpGet("GetFlights")]
        public async Task<IEnumerable<Flight>> GetFlights(int PageSize = 30, int Page = 1)
        {
            var Result = await Services.GetFlights(GetSkip(Page, PageSize), PageSize);
            if (Result == null)
                return new List<Flight>();

            return Result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        int GetSkip(int Page, int PageSize)
        {
            return (Page - 1) * PageSize;
        }
    }
}
