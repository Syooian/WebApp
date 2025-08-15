using Homework2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Homework2.Controllers
{
    public class FlightInformationController : Controller
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

        public IActionResult Index()
        {
            return View();
        }
    }
}
