using Microsoft.AspNetCore.Mvc;
using WebAPITest.Services;

namespace WebAPITest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestProductController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        readonly TestProductService Service = new TestProductService();
        public TestProductController(TestProductService Service)
        {
            this.Service = Service;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<TestProductData> GetProducts()
        {
            return Service.GetProducts();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet("{ID}")]
        public ActionResult<TestProductData> GetProduct(int ID)
        {
            try
            {
                return Service.GetProduct(ID);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
