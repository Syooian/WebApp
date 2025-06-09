using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using MyModel_DBFirst.Models;

namespace MyModel_DBFirst.Controllers
{
    public class HomeController : Controller
    {
       

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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


//MyModel_DBFirst專案進行步驟

//1.使用DB First建立 Model

//1.1 在SSMS中執行dbStudents.sql程式碼，建立範例資料庫dbStudents，內含一個tStudent資料表

//1.2 建立專案與資料庫的連線
//1.2.1 使用NuGet(專案名稱上按右鍵→管理NuGet套件)安裝下列套件
//      (1) Microsoft.EntityFrameworkCore.SqlServer
//      (2) Microsoft.EntityFrameworkCore.Tools

//1.2.2 到SSMS設定登入SQL Server的使用者(必須測試連線成功)

//1.2.3 到套件管理器主控台(檢視 > 其他視窗 > 套件管理器主控台)下指令
//      Scaffold-DbContext "Data Source=伺服器位址;Database=資料庫名稱;TrustServerCertificate=True;User ID=帳號;Password=密碼" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -NoOnConfiguring -UseDatabaseNames -NoPluralize -Force
//      若成功的話，會看到Build succeeded.字眼，並在Models資料夾裡看到dbStudentsContext.cs(描述資料庫)及tStudent.cs(描述資料表)

//1.2.4 在dbStudentsContext.cs裡撰寫連線到資料庫的程式
//      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//              => optionsBuilder.UseSqlServer("Data Source=伺服器位址;Database=資料庫名稱;TrustServerCertificate=True;User ID=帳號;Password=密碼");

//1.2.5 在dbStudentsContext.cs裡撰寫一個空的建構子
//      public dbStudentsContext()
//      {
//      }
///////////////////////////////////////////////////////
///

//2.  製作自動生成的tStudent資料表的CRUD功能

//2.1 使用Scaffold方法讓Visual Studio自動建立出tStudent資料表的的CRUD功能
//    包含tStudentController及Index.cshtml、Create.cshtml、Edit.cshtml、Delete.cshtml、Details.cshtml等五個View的全部程式碼

//2.1.1 在Controllers資料夾上按右鍵→加入→控制器
//2.1.2 選擇「使用EntityFramework執行檢視的MVC控制器」→按下「加入」鈕
//2.1.3 在對話方塊中設定如下
//      模型類別: tStudent(MyModel_DBFirst.Models)
//      資料內容類別: dbStudentsContext(MyModel_DBFirst.Models)
//      勾選 產生檢視
//      勾選 參考指令碼程式庫
//      勾選 使用版面配置頁
//      控制器名稱使用預設即可(tStudentsController)
//      按下「新增」鈕
//2.1.4 此時Visual Studio會進行Scaffolding動作，將產出一個tStudentsController
//      (會包含所有相關的Action)及五個View(Index.cshtml、Create.cshtml、Edit.cshtml、Delete.cshtml、Details.cshtml)的全部程式碼

//      ※備註說明※
//      Index.cshtml(List範本)
//      Create.cshtml(Create範本)
//      Edit.cshtml(Edit範本)
//      Delete.cshtml(Delete範本)
//      Details.cshtml(Details範本)


//2.2   修改tStudentsController內容
//2.2.1 撰寫建立DbContext物件的程式
//      dbStudentsContext _context=new dbStudentsContext();
//2.2.2 將原先既有的程式碼(如下)註解掉
//      private readonly dbStudentsContext _context;
//      public tStudentsController(dbStudentsContext context)
//      {
//          _context = context;
//      }
//※自動生成的Controller寫法為依賴注入(Dependency Injection, DI)，目前我們尚未學到，因此先用一般new物件的寫法※


//2.3   執行Index View進行CRUD功能測試

//※補充說明※
//Visual Studio自動建立出的tStudentController，預設會使用「依賴注入(Dependency Injection)」的寫法
//不過一開始我們先不使用依賴注入的寫法，因此我們需修改如 1.2.5 及 2.2 等步驟的程式碼後才能正常執行