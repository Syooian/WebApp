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


/////////////////////////////////////////////////////////////////////////////


//3     製作具CRUD 的 Restful API(Web API)

//3.1   建立Category資料的 API Ccontroller 
//3.1.1 在Controllers資料夾上按右鍵→加入→控制器
//3.1.2 左側選單點選「API」→ 中間主選單選擇「使用Entity Framework執行動作的API控制器」→按下「加入」鈕
//3.1.3 在對話方塊中設定如下
//      模型類別: Category(MyStoreAPI.Models)
//      資料內容類別: GoodStoreContext(MyStoreAPI.Models)
//      控制器名稱使用預設即可(CategoriesController)
//      按下「新增」鈕
//3.1.4 修改API介接路由為「api[controller]」


//3.2   建立Product資料的 API Ccontroller 
//3.2.1 在Controllers資料夾上按右鍵→加入→控制器
//3.2.2 左側選單點選「API」→ 中間主選單選擇「使用Entity Framework執行動作的API控制器」→按下「加入」鈕
//3.2.3 在對話方塊中設定如下
//      模型類別: Product(MyStoreAPI.Models)
//      資料內容類別: GoodStoreContext(MyStoreAPI.Models)
//      控制器名稱使用預設即可(ProductsController)
//      按下「新增」鈕
//3.2.4 修改API介接路由為「api[controller]」
//※使用Swagger Tool分別對上面兩個 API進行操作測試※



//3.3   資料接收來源
//3.3.1 以Swagger測試查看目前CategoriesController及ProductsController的資料來源
//3.3.2 將CategoriesController及ProductsController中的Action參數改變資料接收來源並以Swagger測試及交叉比較
//※資料接收來源的觀念很重要，必須配合實作的測試，透過Swagger觀查變化，就能知道使用時機※

//※補充說明※
//[FromBody]：HTTP 請求的主體內容(Body)綁定到Action的參數上，通常用於取得JSON、XML或其他格式的文字資料。
//[FromForm]：將來自HTTP請求主體的表單資料綁定到Action的參數上，常用於接收來自表單form提交的資料，例如 application/x-www-form-urlencoded 或 multipart/form-data。
//[FromQuery]：將URL中的參數綁定到Action的參數上，當你希望從 URL 的取得資料時使用。
//[FromHeader]：將HTTP請求中的標頭值綁定到Action的參數上，適合從Request Header中取得資料，例如Authentication Token、Client端資訊等。
//[FromRoute]：將URL路由中的參數綁定到Action的參數上，當URL的某部分是動態的，需要取得這些路由參數時使用。


/////////////////////////////////////////////////////////////////////////////


/////////////////////////////////////////////////////////////////////////////


//4     使用Get取得資料

//4.1   取得資料清單(ProductsController裡的第一個Get Action)
//4.1.1 先使用Swagger測試及觀查目前Product的資料取得狀況
//4.1.2 使用Include()同時取得關聯資料
//4.1.3 使用Where()改變查詢的條件並測試
//4.1.4 使用OrderBy()相關排序方法改變資料排序並測試
//4.1.5 使用Select()抓取特定的欄位並測試
//4.1.6 使用Swagger測試及觀查目前Product的資料取得狀況，並進行上述相關測試
//※這裡有一些重要的觀念必須解釋及透過實作來加強印象，尤其資料表之間的關聯特性將會影響實作的方式※
//※發生循環參照時可使用JsonIgnore來解決※


//4.2   使用DTO(Data Transfer Object)資料傳輸物件
//4.2.1 建立DTOs資料夾(這個步驟可視需求而定)來放置相關檔案
//4.2.2 建立ProductDTO類別
//4.2.3 改寫ProductsController裡的Get Action
//4.2.4 使用Swagger測試
//※想要抓取特定欄位最典型的方法就是使用DTO來傳輸※



//4.3   取得特定資料(ProductsController裡的第二個Get Action)
//4.3.1 先使用Swagger測試及觀查目前Product的資料取得狀況(理解參數及介接口)
//4.3.2 使用Include()同時取得關聯資料並使用ProductDTO來傳遞資料
//4.3.3 使用Swagger測試
//※發生循環參照時可使用JsonIgnore來解決※