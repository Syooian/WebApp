using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebAPI.Services;

namespace MyWebAPI.Controllers
{
    [Route("api[controller]")]
    [ApiController]
    public class SomeController : ControllerBase
    {
        //8.1.4 在SomeController裡注入SomeService服務(這裡就是DI的寫法，不使用 new 關鍵字)
        readonly SomeService _someService;

        public SomeController(SomeService someService)
        {
            _someService = someService;
        }

        //SomeService someService = new SomeService();

        //8.1.5 撰寫兩個Get Action
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _someService.getAllStudents();
        }

        [HttpGet("id")]
        public ActionResult<string> Get(string id)
        {
            return _someService.gelStudent(id);
        }

    }
}
