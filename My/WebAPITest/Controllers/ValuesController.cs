using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPITest.Controllers
{
    [Route("api/[controller]")]//路由界接位址
    /**
     * [Route("api/[controller]")]：localhost:xxxx/api/Values
     * [Route("[controller]"：localhost:xxxx/Values
     */
    [ApiController]
    public class ValuesController : ControllerBase
    {
        string[] ReturnArray = { "アマガミ", "涼宮ハルヒ", "響けユーフォニアム" };

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return ReturnArray;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return ReturnArray[id];
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            //[FromBody]：從Body裡送資料
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
