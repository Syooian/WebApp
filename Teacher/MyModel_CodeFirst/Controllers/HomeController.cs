using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyModel_CodeFirst.Models;

namespace MyModel_CodeFirst.Controllers
{
    public class HomeController : Controller
    {
        private readonly GuestBookContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, GuestBookContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _context.Book.Where(b=>b.Photo!=null).OrderByDescending(s => s.CreatedDate).Take(5).ToListAsync();


            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


//MyModel_CodeFirst專案進行步驟

//1. 使用Code First建立Model及資料庫

//1.1   在Models資料夾裡建立Book及ReBook兩個類別做為模型
//1.1.1 在Models資料夾上按右鍵→加入→類別，檔名取名為Book.cs，按下「新增」鈕
//1.1.2 設計Book類別的各屬性，包括名稱、資料類型及其相關的驗證規則及顯示名稱(Display)
//1.1.3 在Models資料夾上按右鍵→加入→類別，檔名取名為ReBook.cs，按下「新增」鈕
//1.1.4 設計ReBook類別的各屬性，包括名稱、資料類型及其相關的驗證規則及顯示名稱(Display)
//1.1.5 撰寫兩個類別間的關聯屬性做為未來資料表之間的關聯
//1.1.6 建立MeataData類別，把Book及ReBook類別中自己所添加的程式碼移植至MetaData類別中
//1.1.7 使用Partial Class的特性，將MeataData類別標註於對應的Book及ReBook類別上(原始的Book及ReBook類別須在class前加上partial)
//※利用MeataData類別可以運用Partial Class的特性，將原本描述在原class中的程式碼分離出來，達到較好的程式架構，使原來的程式碼保持原始狀態※
//※這個實作技巧在DBFirst中尤為重要，因為只要重新執行DBFirst，將會把原來的模型內容覆蓋掉，而寫在模型類別中的規則就必須重新撰寫※



//1.2   建立DbContext類別
//      ※安裝下列兩個套件※
//      (1)Microsoft.EntityFrameworkCore.SqlServer
//      (2)Microsoft.EntityFrameworkCore.Tools
//      ※與DB First安裝的套件一樣※
//1.2.1 在Models資料夾上按右鍵→加入→類別，檔名取名為GuestBookContext.cs，按下「新增」鈕
//1.2.2 撰寫GuestBookContext類別的內容
//      (1)須繼承DbContext類別
//      (2)撰寫依賴注入用的建構子
//      (3)描述資料庫裡面的資料表
//1.2.3 在appsettings.json中撰寫資料庫連線字串
//1.2.4 在Program.cs內以依賴注入的寫法註冊讀取連線字串的服務(food panda、Uber Eats)
//      ※注意程式的位置必須要在var builder = WebApplication.CreateBuilder(args);這句之後且在var app = builder.Build();之前
//1.2.5 在套件管理器主控台(檢視 > 其他視窗 > 套件管理器主控台)下指令
//      ※※※注意注意※※※ 在執行指令前請先確定專案是否正確選擇
//      ※※※注意注意※※※ 在執行指令前請先確定專案是否正確選擇
//      ※※※注意注意※※※ 在執行指令前請先確定專案是否正確選擇
//      (1)Add-Migration InitialCreate
//      (2)Update-database
//      ※第(1)項的「InitialCreate﹞是自訂的名稱，若執行成功會看到「Build succeeded.」※
//      ※另外會看到一個Migrations的資料夾及其檔案被建立在專案中，裡面紀錄著Migration的歷程※
//      ※第(1)項指令執行成功才能執行第(2)項指令※
//      (3)至SSMS中查看是否有成功建立資料庫及資料表(目前資料表內沒有資料)

//※※※目前建置出來的資料庫並沒有符合我們在模型中所撰寫的規則※※※
//※※※MetadataType 只適用於 UI 驗證，與資料庫有關的設定值仍需在原模型類別檔或 DbContext 中撰寫。※※※
//在Code First中，因原模型類別檔不會被覆蓋，所以可以直接在原模型類別檔中撰寫，不需要使用MetadataType。
//不過為了保持原模型類別檔與DB First的相容性，仍建議使用MetadataType來撰寫驗證規則給UI使用。
//因此我們必須在DbContext中使用Fluent API來撰寫與資料庫端有關的程式，才能確保在資料庫建立時會使用此規則。

//1.2.6 在DbContext中使用Fluent API在GuestBookContext覆寫 OnModelCreating 方法
//1.2.7 將資料庫刪除，並將專案中Migrations資料夾及內含檔案整個刪除。
//1.2.8 重新執行Migration建置資料庫


//※Fluent API 是 Entity Framework Core 提供的一種「以程式碼方式」設定資料模型屬性與資料表結構的方法。※
//※它通常在你的 DbContext 類別中，覆寫 OnModelCreating 方法來使用。※


//1.3   創建Initializer物件建立初始(種子)資料(Seed Data)
//      ※※※我們可以在創建資料庫時就創建幾筆初始的資料在裡面以供開發時測試之用，但這個動作不一定要做，視需求而定※※※
//      ※※※做此步驟前，請先將資料庫刪除，並將專案中Migrations資料夾及內含檔案整個刪除※※※

//1.3.1 準備種子照片(SeedPhotos資料夾)
//1.3.2 在Models資料夾上按右鍵→加入→類別，檔名取名為SeedData.cs，按下「新增」鈕
//1.3.3 撰寫SeedData類別的內容
//      (1)撰寫靜態方法 Initialize(IServiceProvider serviceProvider)
//      (2)撰寫Book及ReBook資料表內的初始資料程式
//      (3)撰寫上傳圖片的程式
//      (4)加上 using() 及 判斷資料庫是否有資料的程式
//1.3.4 在Program.cs撰寫啟用Initializer的程式(要寫在var app = builder.Build();之後)
//      ※這個Initializer的作用是建立一些初始資料在資料庫中以利測試，所以不一定要有Initializer※
//      ※注意:初始資料的照片放在BookPhotos資料夾中※
//1.3.5 建置專案，確定專案完全建置成功
//1.3.6 再次於套件管理器主控台(檢視 > 其他視窗 > 套件管理器主控台)下指令
//      (1)Add-Migration InitialCreate
//      (2)Update-database
//1.3.7 至SSMS中查看是否有成功建立資料庫及資料表(目前資料表內沒有資料)
//1.3.8 在瀏覽器上執行網站首頁以建立初始資料(若沒有執行過網站，初始資料不會被建立)
//1.3.9 再次至SSMS中查看資料表內是否有資料


// 2.製作留言板前台功能

//2.1   製作自動生成的Book資料表CRUD
//2.1.1 在Controllers資料夾上按右鍵→加入→控制器
//2.1.2 選擇「使用EntityFramework執行檢視的MVC控制器」→按下「加入」鈕
//2.1.3 在對話方塊中設定如下
//      模型類別: Book(MyModel_CodeFirst.Models)
//      資料內容類別: GuestBookContext(MyModel_CodeFirst.Models)
//      勾選 產生檢視
//      勾選 參考指令碼程式庫
//      勾選 使用版面配置頁
//      控制器名稱改為PostBooksController
//      按下「新增」鈕
//2.1.4 修改PostBooksController，移除Edit、Delete Action
//2.1.5 刪除Edit、Delete View檔案
//2.1.6 修改Index Action的寫法


//2.2   顯示功能
//2.2.1 修改適合前台呈現的Index View
//2.2.2 將PostBooksController中Details Action改名為Display(View也要改名字)
//2.2.3 在Index View中加入Display Action的超鏈結
//2.2.4 修改Display View 排版樣式，排版可以個人喜好呈現
//      ※排版可以個人喜好呈現※


// 2.3   使用「ViewComponent」技巧實作「將回覆留言內容顯示於Display View」
//      ※此單元將要介紹ViewComponent的使用方式※
//2.3.1 在專案中新增ViewComponents資料夾(專案上按右鍵→加入→新增資料夾)以放置所有的ViewComponent元件檔
//2.3.2 在ViewComponents資料夾中建立VCReBooks ViewComponent(右鍵→加入→類別→輸入檔名→新增)
//2.3.3 VCReBooks class繼承ViewComponent(注意using Microsoft.AspNetCore.Mvc;)
//2.3.4 撰寫InvokeAsync()方法取得回覆留言資料
//2.3.5 在/Views/Shared裡建立Components資料夾，並在Components資料夾中建立VCReBooks資料夾
//2.3.6 在/Views/Shared/Components/VCReBooks裡建立檢視(右鍵→加入→檢視→選擇「Razor檢視」→按下「加入」鈕)
//2.3.7 在對話方塊中設定如下
//      檢視名稱: Default
//      範本:Empty(沒有模型)
//      不勾選 建立成局部檢視
//      不勾選 使用版面配置頁
//   ※注意：資料夾及View的名稱不是自訂的，而是有預設的名稱，規定如下：※
//   /Views/Shared/Components/{ComponentName}/Default.cshtml
//   /Views/{ControllerName}/Components/{ComponentName}/Default.cshtml
//2.3.8 在Default View上方加入@model IEnumerable<MyModel_CodeFirst.Models.ReBook>
//2.3.9 依喜好編輯Default View排版方式
//2.3.10 編寫Display View，加入VCReBooks ViewComponent
//2.3.11 測試


//2.4   留言功能
//2.4.1 修改Create View，修改Photo為上傳檔案的元件(type="file")
//2.4.2 修改Create View，將<form>增加 enctype="multipart/form-data" 屬性
//2.4.3 加入前端效果，使照片可先預覽
//2.4.4 刪除CreatedDate欄位
//2.4.5 修改Description欄位的標籤為textarea
//2.4.6 修改Post Create Action，加上處理上傳照片的功能
//2.4.7 測試留言功能
//2.4.8 在Index View中加入未上傳照片的留言之顯示方式
//2.4.9 在Display View中加入未上傳照片的留言之顯示方式
//2.4.10 在Index View中加入處理「有換行的留言」顯示方式
//2.4.11 在Display View中加入處理「有換行的留言」顯示方式
//2.4.12 在VCReBook View Component的Default View中加入沒有回覆留言即不顯示的判斷