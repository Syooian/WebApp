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


//4.4   建立Product資料查詢功能
//4.4.1 將資料轉換的程式寫成函數並再次改寫Get Action(※這種寫法架構才會好※)
//4.4.2 加入產品類別搜尋
//4.4.3 加入產品名稱關鍵字搜尋
//4.4.4 加入價格區間搜區
//4.4.5 加入產品敘述關鍵字搜尋
//4.4.6 使用Swagger測試(輸入的條件越多越嚴苛)
//4.4.7 利用Request URL在瀏覽器上執行測試
//※這個部份在做時會因Linq的寫法不同造成資料處理完的型態有不同的結果※
//※需依照Linq撰寫的方式及資料的同步或非同步取得，依其所需改變寫法※
//4.4.8 修改先將資料載入內存的寫法


//4.5   同時取得Category及Product一對多的關聯資料
//4.5.1 先使用Swagger測試及觀查目前Category的資料取得狀況
//4.5.2 建立CategoryDTO類別
//4.5.3 改寫CategoriesController裡的第一個Get Action
//4.5.4 使用Include()同時取得關聯資料並以CategoryDTO傳遞
//4.5.5 使用Swagger測試
//4.5.6 改寫CategoriesController裡的第二個Get Action
//4.5.7 使用Include()同時取得關聯資料並以CategoryDTO傳遞
//4.5.8 使用Swagger測試
//4.5.9 在CategoryDTO裡加入統計該類別有幾種商品的屬性
//4.5.10 在ProductDTO裡也加入一些統計資料的屬性
//4.5.11 使用Swagger測試


//4.6   使用SQL語法進行查詢
//4.6.1 新增一個Get Action GetProductFromSQL()並設定介接口為[HttpGet("fromSQL")]
//4.6.2 用SQL語法撰寫與先前一樣的功能並使用DTO傳遞結果
//4.6.3 製作關鍵字查詢
//4.6.4 使用Swagger測試(這裡會發生錯誤，因為使用了合併查詢)
//4.6.5 修改GoodStoreContext，增加ProductDTO的DbSet屬性
//4.6.6 將_context.Product.FromSqlRaw(sql).ToListAsync();改為_context.ProductDTO.FromSqlRaw(sql).ToListAsync();
//4.6.7 使用Swagger測試(這裡會發生ProductDTO沒有設定Primary Key的例外)
//4.6.8 修改GoodStoreContext的OnModelCreating()，標示ProductDTO為HasNoKey
//4.6.9 使用Swagger測試
//※使用SQL語法進行查詢是SQL老手的習慣，雖然EF Core已經使用一段時間，但很多開發人員仍鍾情於SQL※
//※不過使用SQL時需注意SQL Injection的問題，而我們使用SqlParameter來避免SQL Injection※
//※使用參數化查詢是防止 SQL Injection 的有效方式，使用SqlParameter避免SQ字串接寫法，直接避免SQL Injection風險※



//4.7   關於DbContext修改的優化做法
//4.7.1 複製GoodStoreContext.cs並更名為GoodStoreContextG2.cs
//4.7.2 修改類別、建構子名稱及繼承父類別
//4.7.3 只留下DTO的DbSet其他的DbSet全數刪除
//4.7.4 OnModelCreating方法中只留下ProductDTO的Entity設定其他刪除
//4.7.5 加入base.OnModelCreating(modelBuilder);來繼承父類別所的方法
//4.7.6 將GoodStoreContext.cs中與ProductDTO有關的設置刪除
//4.7.7 在Program裡註冊GoodStoreContext2的Service(※注意※原本註冊的GoodStoreContext不可刪掉)
//4.7.8 修改ProductsController上方所注入的GoodStoreContext為GoodStoreContext2
//4.7.9 使用Swagger測試
//※如果我們只是直接去改了原本的Context，在開發的過程中如果發生必須重新執行DB First的動作時，Context內容將被重置※
//※因此請善加利用物件導向的繼承寫法保持程式碼的彈性及再用性※
//4.7.10 在Program裡全域啟用 ReferenceHandler.Preserve


//4.8   使用資料庫裡的預存程序
//4.8.1 在GoodStore資料庫中建立預存程序，程式碼如下(這個預存程序可以讓我們使用類別ID查詢到產品資料)
//------SQL語法------
//use GoodStore
//go
//create proc getProductWithCateName
//	@cateID char(2)='A1'
//as
//begin
//	select p.ProductID, p.ProductName, p.Price, p.Description, p.CateID, c.CateName
//    from Product as p inner join Category as c on p.CateID=c.CateID where p.CateID=@cateID
//end
//------SQL語法------
//4.8.2 在ProductsController中建立一個新的Get Action
//4.8.3 設置介接口為[HttpGet("fromProc/{id}")]，Action名稱可自訂，並使用ProductDTO來傳遞資料
//4.8.4 使用預存程序進行查詢(參數的傳遞請使用SqlParameter)
//4.8.5 使用Swagger測試


/////////////////////////////////////////////////////////////////////////////////////////////////////////

//5     使用Post新增資料

//※較常用到的三種資料來源方式[FromBody][FromForm][FromQuery]※
//※若資料來源都是單純字串或數值等，預設為[FromBody]※

//5.1   基本新增資料方式
//5.1.1 先使用Swagger測試及觀查目前Product及Category的Post(注意其接收格式為JSON)
//5.1.2 使用Swagger對此二個資料表做資料新增測試(此時可能會有required的錯誤)
//5.1.3 修改Category.cs的Product屬性為非必填
//5.1.4 修改Product.cs的Category屬性為非必填
//5.1.5 使用Swagger再進行新增測試(應可成功新增寫入資料庫，但並無檔案之上傳)
//※由於傳遞的是JSON格式資料，只要能對應到所有屬性，通過模型的驗證即可成功※//
//※若是以JSON傳遞資料方式，則前端表單在傳遞時需先轉為JSON格式再拋給API※//
//※不過因Product的前端表單中有上傳檔案的功能，因此後面必須再做修改※//



//5.2   新增Product資料加入上傳照片功能
//5.2.1 將ProductsController的Post Action標示為[FromForm]，使其能直接由前端表單接收資料
//5.2.2 使用Swagger測試是否能正常新增(目前應會有錯誤,無法正常新增)
//5.2.3 建立一個ProductPostDTO給Post利用DTO傳遞資料
//5.2.4 建立一個新的Post Action，介接口設定為[HttpPost("PostWithPhoto")]，並加入上傳檔案的動作
//(這裡我們不要把原來的Post刪掉，而是新做一個以利測試)
//5.2.5 將上傳檔案寫成一個獨立的方法
//5.2.6 使用Swagger測試
//※如果Bind的資料模型類別中具有上傳檔案的物件(如IFormFile)，即使不標示資料來源為[FromForm]，它仍能自己判斷匹配為[FromForm]※//


//5.3   資料驗證
//5.3.1 在ProductPostDTO.cs加入需要的內建驗證器(Validator)
//5.3.2 使用Swagger測試
//※在一般的情況下我們只會在接收資料(Post、Put、Delete)時進行驗證，讀取資料則不會※
//5.3.3 在ProductPostDTO.cs加入自訂驗證器(使用ValidationAttribute物件)
//5.3.4 在需要使用此驗證器的屬性上加入標籤(這裡範例為ProductName屬性)
//5.3.5 使用Swagger測試
//5.3.6 建立CategoryPostDTO類別
//5.3.7 在CategoryPostDTO.cs加入需要的內建驗證器(Validator)
//5.3.8 在CategoryPostDTO.cs加入自訂驗證器(使用ValidationAttribute物件)
//5.3.9 在需要使用此驗證器的屬性上加入標籤
//5.3.10 修改CategoriesController的Post方法，使其傳遞CategoryPostDTO
//5.3.11 修改Post Action 內的寫法
//5.3.12 使用Swagger測試


//小結
//※程式撰寫至此，我們可以發現DTO在WebAPI的建置中是相當重要的資料傳輸物件※
//※除非你的API非常單純，否則您無法避免使用DTO物件※
//※因此在API設計的時候，我們盡量不去動到原來的Model物件，資料的傳輸皆用DTO來取代※
//※若DbContext物件的設計需求，我們則使用繼承的方式來使程式碼保持彈性※


/////////////////////////////////////////////////////////////////////////////

//6     使用Put修改資料

//6.1   基本修改資料方式
//6.1.1 先使用Swagger測試及觀查目前Product及Category的Put(注意其接收格式為JSON)
//6.1.2 使用Swagger對此二個資料表做資料修改測試(這邊主要是觀察一下它們的資料呈現)
//6.1.3 新增CategoryPutDTO類別
//6.1.4 改寫CategoriesController中Put Action內容
//6.1.5 使用Swagger測試
//6.1.6 新增ProductPutDTO類別
//6.1.7 改寫ProductsController中Put Action內容
//6.1.8 使用Swagger測試


/////////////////////////////////////////////////////////////////////////////
//7     使用Delete刪除資料

//7.1   基本刪除資料方式
//7.1.1 改寫ProductsController中Delete Action內容，加入刪除照片的功能
//7.1.2 將刪除照片功能另建立FileDelete()方法
//7.1.3 使用Swagger測試
//7.1.4 使用Swagger測試刪除Category資料(這裡會發生資料表關聯的完整性限制)
//7.1.5 建立可刪除多筆資料的Delete Action(批次刪除)，介接口設為[HttpDelete("ByCatID")]，方法名稱可自訂，傳入的參為為商品類別ID
//7.1.6 使用Swagger測試
//7.1.7 再次使用Swagger測試刪除Category資料
//※一般要刪除父資料表的資料前，需先刪除與之關聯的子資料表所有資料，以確保資料不會被批次誤刪※


/////////////////////////////////////////////////////////////////////////////

//8     程式碼重構-使用依賴注入(Dependency Injection-DI)

//8.0   自訂資料驗證類別程式碼重構(未用到DI)
//8.0.1 新增一個「ValidationAttributes」資料夾，並在資料夾中建立MyValidator.cs檔案
//8.0.2 將原本寫在ProductPostDTO及CategoryPostDTO內的自訂驗證類別(ValidationAttributes)貼進MyValidator.cs檔案中
//8.0.3 將原先的ValidationAttributes註解或刪除
//8.5.4 在用到自訂驗證器的DTO中using static的 MyValidator類別
//8.0.5 使用Swagger測試


//8.1   實作Service及DI
//8.1.1 創建一個Services資料夾
//8.1.2 在資料夾中建立SomeService.cs類別並實作內容
//8.1.3 建立API控制器 SomeController
//8.1.4 在SomeController裡注入SomeService服務(這裡就是DI的寫法，不使用 new 關鍵字)
//8.1.5 撰寫兩個Get Action
//8.1.6 在Program.cs裡註冊SomeService服務
//8.1.7 使用Swagger測試









