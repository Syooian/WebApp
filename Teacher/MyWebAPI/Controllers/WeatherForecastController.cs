using Microsoft.AspNetCore.Mvc;

namespace MyWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}




//1     製作我的第一個Web API(Restful API)

//1.1   建立CRUD API Ccontroller 
//1.1.1 在Controllers資料夾上按右鍵→加入→控制器
//1.1.2 左側選單點選「API」→ 中間主選單選擇「執行讀取/寫入動作的API控制器」→按下「加入」鈕
//1.1.3 檔名使用預設的ValuesController.cs 即可，按下「新增」鈕
//      ※我們會得到一個已經撰寫好基本CRUD架構的API Ccontroller※
//      ※該Ccontroller的撰寫風格符合Rest，使用Get、Get/{id}、Post、Put、Delete 進行各項動作※


//1.2   安裝Swagger Tool(如果有需要的話)
//1.2.1 使用NuGet(專案名稱上按右鍵→管理NuGet套件)安裝Swashbuckle.AspNetCore套件
//1.2.2 在Program.cs中註冊及啟用Swagger
//1.2.3 安裝完後請執行本專案讓伺服器執行
//1.2.4 在網址列輸入http://localhost:xxxx/swagger/index.html (其中xxxx是您的port)
//1.2.5 測試及瞭解Swagger
//      ※Swagger是由一家叫SmartBear的公司所發行，屬於無償使用的OpenAPI套件，以使用於API開發時的測試※
//      ※以往開發多使用Postman這個軟體進行API測試，目前Swagger成為主流※
//      ※若建立專案時為WebAPI專案並勾選Open API，Swagger將直接安裝在專案上※  


//1.3   使用Swagger Tool來進行ValuesController API的操作測試
//1.3.1 修改Get Action的內容並測試
//1.3.2 進行增加Action、修改介接口等測試
//1.3.3 修改ValuesController的路由介接位址並測試 ([Route("api/[controller]")])
//      ※Web API因為沒有UI，利用瀏覽器只能就Get的動作進行測試，無法對Post、Put及Delete的動作進行測試※ 
//      ※還有一個強大的API軟體叫Postman，以前還沒有Swagger時大都是用Postman進行測試※
//      ※因此這裡的操作目的是熟悉Swagger的用法，以利Web API在開發時能使用它來進行測試※



/////////////////////////////////////////////////////////////////////////////
///

//2     範例專案開發準備

//2.1   利用素材建利範例專案環境
//2.1.1 建立GoodStore範例資料庫
//2.1.2 將ProductPhotos資料夾及內含之檔案放至專案中的wwwroot下
//2.1.3 在Program.cs中加入app.UseStaticFiles(); (因為我們開的是 純WebAPI專案)
//2.1.4 在瀏覽器中輸入「http://localhost:XXXX/ProductPhotos/A3001.jpg」(XXXX為您的port)測試是否能看到照片


//2.2   建立專案與資料庫的連線
//2.2.1 使用DB First建立 Model
//2.2.2 使用NuGet(專案名稱上按右鍵→管理NuGet套件)安裝下列套件
//      (1) Microsoft.EntityFrameworkCore.SqlServer
//      (2) Microsoft.EntityFrameworkCore.Tools 
//2.2.3 到套件管理器主控台(檢視 > 其他視窗 > 套件管理器主控台)下指令
//      Scaffold-DbContext "Data Source=伺服器位址;Database=GoodStore;TrustServerCertificate=True;User ID=帳號;Password=密碼" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -NoOnConfiguring  -UseDatabaseNames -NoPluralize -Force
//      若成功的話，會看到Build succeeded.字眼，並在Models資料夾裡看到GoodStoreContext.cs、Category.cs、Product.cs等資料庫相關類別檔
//2.2.4 在appsettings.json檔中撰寫連線字串(ConnectionString)
//2.2.5 在Program.cs註冊DbContext物件(GoodStoreContext.cs)並指定appsettings.json中的連線字串程式碼(這段必須寫在var builder這行後面)